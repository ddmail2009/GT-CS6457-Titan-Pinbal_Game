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

//			Rigidbody ballRig = newBall.GetComponent <Rigidbody> ();
//			Vector3 forceDirection = (-transform.right * Random.Range (0f, 1f) + transform.forward * Random.Range (0f, 1f)).normalized;
//			ballRig.AddForce (forceDirection * Random.Range (10f, 15f), ForceMode.VelocityChange);

			readyForSpawning = false;
			Invoke ("GetReadyForSpawning", minSpawnInterval);
		}
	}

	void GetReadyForSpawning ()
	{
		readyForSpawning = true;
	}
}
