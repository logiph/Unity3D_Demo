using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble = 10.0f;

	// Use this for initialization
	void Start () {
	
		// Random.insideUnitSphere 返回一个单位长度的球体内的一个随机点
		// angularVelocity 角速度
		GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitSphere * tumble;

	}
	
	// Update is called once per frame
	void Update () {
	
	}






}
