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
		Vector3 movingDirection = -ballBody.velocity;
		Quaternion newRotation = Quaternion.LookRotation (movingDirection);
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 8);
	}
}
