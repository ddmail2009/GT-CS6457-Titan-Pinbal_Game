using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour
{
	static public BallManager instance;
	public GameObject[] ballTemplates;
	public int numOfBalls = 0;
	public Transform ballSpawnPoint;
	public float minSpawnInterval = 3f;

	bool readyForSpawning = true;
	int ballTemplateIdx = 0;

	void Awake ()
	{
		instance = this;
	}

	void Update ()
	{
		if (readyForSpawning && Input.GetButtonUp ("SpawnBall")) {
			GameObject newBall = Instantiate (ballTemplates [ballTemplateIdx], ballSpawnPoint.position + transform.up * 0.5f, new Quaternion (15, 0, 0, 0)) as GameObject;
			newBall.transform.parent = transform;
			numOfBalls += 1;

			readyForSpawning = false;
			Invoke ("GetReadyForSpawning", minSpawnInterval);
		}

		// FIXME: testing code. remove this after milestone 3
		if (readyForSpawning && Input.GetButtonUp ("AITest")) {
			GameObject newBall = Instantiate (ballTemplates [ballTemplateIdx], GameObject.Find ("AISpawningPoint").transform.position + transform.up * 0.5f, new Quaternion (15, 0, 0, 0)) as GameObject;
			newBall.transform.parent = transform;
			numOfBalls += 1;

			newBall.GetComponent<Rigidbody> ().AddForce (transform.forward * 25.0f, ForceMode.VelocityChange);

			readyForSpawning = false;
			Invoke ("GetReadyForSpawning", minSpawnInterval);
		}

		// FIXME: same as above...
		if (Input.GetKeyUp ("1")) {
			Debug.Log ("Set BallTemplate as MultiBall 1");
			ballTemplateIdx = 0;
		} else if (Input.GetKeyUp ("2")) {
			Debug.Log ("Set BallTemplate as MultiBall 2");
			ballTemplateIdx = 1;
		} else if (Input.GetKeyUp ("3")) {
			Debug.Log ("Set BallTemplate as MultiBall 3");
			ballTemplateIdx = 2;
		}
	}

	void GetReadyForSpawning ()
	{
		readyForSpawning = true;
	}
}
