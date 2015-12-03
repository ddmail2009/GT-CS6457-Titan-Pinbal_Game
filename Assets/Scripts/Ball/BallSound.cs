/**
 * Team Titan
 * 
 * Xiaoyu Chen
 */

using UnityEngine;
using System.Collections;

public class BallSound : MonoBehaviour
{
	public float volumeFactor = 0.02f;

	AudioSource hitAudio, rollAudio;
	Rigidbody ballBody;

	void Awake ()
	{
		AudioSource[] audioSources = GetComponents<AudioSource> ();

		hitAudio = audioSources [0];
		rollAudio = audioSources [1];

		ballBody = GetComponent<Rigidbody> ();
	}
	
	void Update ()
	{
		float ballSpeed = ballBody.velocity.magnitude;
		//aud.pitch = ballSpeed * 0.1f;
		rollAudio.volume = ballSpeed * volumeFactor;
	}

	void OnCollisionEnter (Collision col)
	{
		float ballSpeed = ballBody.velocity.magnitude;
		hitAudio.volume = ballSpeed * volumeFactor;
		hitAudio.Play ();
	}
}
