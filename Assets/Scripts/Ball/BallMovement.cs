/**
 * Team Titan
 * 
 * Xiaoyu Chen
 */

using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour
{
	Rigidbody ballBody;

	void Awake ()
	{
		ballBody = GetComponent<Rigidbody> ();
	}

	void Update ()
	{
		FaceMovingDirection ();
	}

	void FaceMovingDirection ()
	{
		Vector3 movingDirection = -ballBody.velocity;
		
		if (movingDirection != Vector3.zero) { // If ball speed is zero, do nothing
			Quaternion newRotation = Quaternion.LookRotation (movingDirection);
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 8);
		}
	}
}
