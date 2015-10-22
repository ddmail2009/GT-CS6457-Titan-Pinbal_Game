﻿using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour
{
	static public BallManager instance;
	public GameObject ballTemplate;
	public int numOfBalls = 0;
	public Transform ballSpawnPoint;

	void Awake ()
	{
		instance = this;
	}

	void FixedUpdate ()
	{
		if (Input.GetButtonUp ("FireBall")) {
			GameObject newBall = Instantiate (ballTemplate, ballSpawnPoint.position + transform.up * 0.5f, new Quaternion (15, 0, 0, 0)) as GameObject;
			newBall.transform.parent = transform;
			numOfBalls += 1;

			Rigidbody ballRig = newBall.GetComponent <Rigidbody> ();
			Vector3 forceDirection = (-transform.right * Random.Range (0f, 1f) + transform.forward * Random.Range (0f, 1f)).normalized;
			ballRig.AddForce (forceDirection * Random.Range (10f, 15f), ForceMode.VelocityChange);
		}
	}
}
