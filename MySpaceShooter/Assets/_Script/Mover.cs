using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed = 5.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {

		float moveHorizontal = 0;
		float moveVertical = 2;

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = movement * speed;
			
	}


}
