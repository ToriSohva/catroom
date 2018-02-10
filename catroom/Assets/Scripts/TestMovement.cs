using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

	Rigidbody2D rb;
	Animator animator;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position += Vector3.ClampMagnitude (Vector3.up * Input.GetAxis ("Vertical") + Vector3.right * Input.GetAxis ("Horizontal"), 1) * 10 * Time.deltaTime;
	}

	void FixedUpdate() {
		var delta = Vector3.ClampMagnitude (Vector3.up * Input.GetAxis ("Vertical") + Vector3.right * Input.GetAxis ("Horizontal"), 1) * 10 * Time.fixedDeltaTime;
		rb.MovePosition(transform.position + delta);

		animator.SetFloat ("Velocity", delta.magnitude / Time.fixedDeltaTime);
	}
}
