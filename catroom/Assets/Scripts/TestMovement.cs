using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position += Vector3.ClampMagnitude (Vector3.up * Input.GetAxis ("Vertical") + Vector3.right * Input.GetAxis ("Horizontal"), 1) * 10 * Time.deltaTime;
	}

	void FixedUpdate() {
		rb.MovePosition(transform.position + Vector3.ClampMagnitude (Vector3.up * Input.GetAxis ("Vertical") + Vector3.right * Input.GetAxis ("Horizontal"), 1) * 10 * Time.fixedDeltaTime);
	}
}
