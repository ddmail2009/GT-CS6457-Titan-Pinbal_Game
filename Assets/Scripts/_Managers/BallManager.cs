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

	AudioSource aud;

	void Awake ()
	{
		instance = this;
		aud = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (readyForSpawning && Input.GetButtonUp ("SpawnBall")) {
			SpawnNewBall ();
			readyForSpawning = false;
			Invoke ("GetReadyForSpawning", minSpawnInterval);
		}
	}

	void SpawnNewBall ()
	{
		aud.Play ();
		GameObject newBall = Instantiate (ballTemplate, ballSpawnPoint.position + transform.up * 0.5f, new Quaternion (15, 0, 0, 0)) as GameObject;
		newBall.transform.parent = transform;
		numOfBalls += 1;
	}

	void GetReadyForSpawning ()
	{
		readyForSpawning = true;
	}
}
