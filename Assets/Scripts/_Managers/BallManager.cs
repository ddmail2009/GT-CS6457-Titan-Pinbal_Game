/**
 * Team Titan
 * 
 * Xiaoyu Chen, Meng-Hsin Tung
 */

using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour
{
	static public BallManager instance;
	public GameObject ballTemplate;
	public int numOfBalls = 0;
	public Transform ballSpawnPoint;
	public float spawnInterval = 3f;

	AudioSource aud;

	void Awake ()
	{
		instance = this;
		aud = GetComponent<AudioSource> ();

		SpawnNewBall ();
	}

	public void BallFired ()
	{
		Invoke ("SpawnNewBall", spawnInterval);
	}

	void SpawnNewBall ()
	{
		aud.Play ();
		GameObject newBall = Instantiate (ballTemplate, ballSpawnPoint.position + transform.up * 0.5f, new Quaternion (15, 0, 0, 0)) as GameObject;
		newBall.transform.parent = transform;
		numOfBalls += 1;
	}
}
