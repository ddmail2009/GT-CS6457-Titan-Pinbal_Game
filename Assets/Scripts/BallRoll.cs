using UnityEngine;
using System.Collections;

public class BallRoll : MonoBehaviour
{
	public float volumeFactor = 0.02f;

	AudioSource aud;
	Rigidbody ballBody;

	void Awake ()
	{
		aud = GetComponent<AudioSource> ();
		ballBody = GetComponent<Rigidbody> ();
	}
	
	void Update ()
	{
		float ballSpeed = ballBody.velocity.magnitude;
		//aud.pitch = ballSpeed * 0.1f;
		aud.volume = ballSpeed * volumeFactor;
	}
}
