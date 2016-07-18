using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed = 5.0f;
	public Boundary boundary;
	public float tilt = 4.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);


		Rigidbody rb = GetComponent<Rigidbody> ();

		if (rb != null) {

			rb.velocity = movement * speed;

			rb.position = new Vector3 (Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
				0,
				Mathf.Clamp(rb.position.y, boundary.zMin, boundary.zMax));


			rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x*-tilt);



		}		
	}

}
