using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	private Vector3 spawnPosition = Vector3.zero;
	private Quaternion spawnRotation;

	// 控制小行星产生的数量
	public int hazardCount = 4;
	public float spawnWait = 0.5f;
	public float startWait = 1.0f;
	public float waveWait = 2.0f;

	// 记分功能
	public Text scoreText;
	private int score;



	// Use this for initialization
	void Start () {
		score = 0;
		UpdateScore ();
		StartCoroutine(SpawnWaves ());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator SpawnWaves() {

		yield return new WaitForSeconds (startWait);

		while (true) {
			for (int i = 0; i < hazardCount; ++i) {
				spawnPosition.x = Random.Range (-spawnValues.x, spawnValues.x);
				spawnPosition.z = spawnValues.z;
				spawnRotation = Quaternion.identity;

				Instantiate (hazard, spawnPosition, spawnRotation);

				yield return new WaitForSeconds (spawnWait);
			}
		}

	}

	public void AddScore(int newScoreValue) {
	
		score += newScoreValue;

		UpdateScore ();
	}

	void UpdateScore () {

		scoreText.text = "得分: " + score;
	}

}
