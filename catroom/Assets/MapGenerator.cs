using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public float roomWidth = 16;
	public float roomHeight = 10;
	public Grid grid;
	public Transform player;
	public Vector2 roomCullDistance;

	public List<Transform> roomTemplates;

	int columns;
	int rows;

	List<Transform> rooms = new List<Transform>();

	static MapGenerator _instance;
	public static MapGenerator instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<MapGenerator> ();
			}
			return _instance;
		}
	}

	void Start() {
		Generate (5, 6);
	}

	public void Generate(int columns, int rows) {
		columns = Mathf.Max (columns, 3);
		rows = Mathf.Max (rows, 4);
		columns = columns % 2 == 0 ? columns + 1 : columns;
		rows = rows % 2 == 0 ? rows : rows + 1;

		this.columns = columns;
		this.rows = rows;

		Debug.Log (new Vector2Int (columns, rows));

		Vector2 roomSize = new Vector2 (roomWidth, roomHeight);
		Vector2 topLeft = (Vector2)transform.position + (Vector2.left * Mathf.FloorToInt(columns / 2) * roomWidth) + (Vector2.up * (rows / 2 - 1) * roomHeight);
		Vector2 cursor = Vector2.zero;

		int centerColumn = columns / 2;
		int centerRow = rows / 2;

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				
				if (j != centerColumn || (i != centerRow && i != centerRow - 1)) {
					
					Vector2 position = topLeft + Vector2.Scale (cursor, roomSize);

					Transform template = roomTemplates[Random.Range(0, roomTemplates.Count)];
					var room = Instantiate (template, position, Quaternion.identity, grid.transform);

					rooms.Add (room);
				} else {
					Debug.Log ("skipping " + cursor.ToString () + "...");
				}

				cursor.x += 1;
				if (cursor.x >= columns)
					cursor.x = 0;
			}
			cursor.y -= 1;
		}
		CameraOverlord.instance.SearchCameras ();
		CullRooms ();
	}

	public void CullRooms() {
		for (int i = 0; i < rooms.Count; i++) {
			bool active = (Mathf.Abs(rooms [i].position.x - player.position.x) <= roomCullDistance.x && Mathf.Abs(rooms [i].position.y - player.position.y) <= roomCullDistance.y);
			rooms [i].gameObject.SetActive (active);
		}
	}
}
