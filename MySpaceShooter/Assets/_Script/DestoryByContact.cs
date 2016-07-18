using UnityEngine;
using System.Collections;

public class DestoryByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;


	// Use this for initialization
	void Start () {
	
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

		Destroy (other.gameObject);
		Destroy (gameObject);
	}



}
