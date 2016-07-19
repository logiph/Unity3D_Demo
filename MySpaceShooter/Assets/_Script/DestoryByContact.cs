using UnityEngine;
using System.Collections;

public class DestoryByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;

	// 添加记分
	public int scoreValue = 10;
	private GameController gameController;

	// Use this for initialization
	void Start () {

		GameObject go = GameObject.FindWithTag ("GameController");

		if (go != null) {
			gameController = go.GetComponent<GameController> ();
		} else {
			Debug.Log ("can't find GameController ");
		}

		if (gameController == null) {
			Debug.Log ("can't find GameController.cs");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
	
		// 防止启动的时候就发生了小行星和边框的碰撞
		if (other.tag == "Boundary") {
			return;
		}

		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "Player") {		
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
		}

		gameController.AddScore (scoreValue);

		Destroy (other.gameObject);
		Destroy (gameObject);
	}



}
