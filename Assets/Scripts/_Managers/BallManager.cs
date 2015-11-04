using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour
{
	static public BallManager instance;
	public GameObject ballTemplate;
	public int numOfBalls = 0;
	public Transform ballSpawnPoint;
	public float minSpawnInterval = 3f;

	bool readyForSpawning = true;

	void Awake ()
	{
		instance = this;
	}

	void FixedUpdate ()
	{
		if (readyForSpawning && Input.GetButtonUp ("SpawnBall")) {
			GameObject newBall = Instantiate (ballTemplate, ballSpawnPoint.position + transform.up * 0.5f, new Quaternion (15, 0, 0, 0)) as GameObject;
			newBall.transform.parent = transform;
			numOfBalls += 1;

			readyForSpawning = false;
			Invoke ("GetReadyForSpawning", minSpawnInterval);
		}

		// FIXME: testing code. remove this after milestone 3
		if (readyForSpawning && Input.GetButtonUp ("AITest")) {
			GameObject newBall = Instantiate (ballTemplate, GameObject.Find ("AISpawningPoint").transform.position + transform.up * 0.5f, new Quaternion (15, 0, 0, 0)) as GameObject;
			newBall.transform.parent = transform;
			numOfBalls += 1;

			newBall.GetComponent<Rigidbody> ().AddForce (transform.forward * 25.0f, ForceMode.VelocityChange);

			readyForSpawning = false;
			Invoke ("GetReadyForSpawning", minSpawnInterval);
		}
	}

	void GetReadyForSpawning ()
	{
		readyForSpawning = true;
	}
}
