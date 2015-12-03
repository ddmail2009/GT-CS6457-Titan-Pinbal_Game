/**
 * Team Titan
 * 
 * Tzu-Wei Huang
 */

using UnityEngine;
using System.Collections;

public class ReverseCharacterLeftRight : Effect
{
	public float effectTime = 20f;
	public ThirdPersonCameraControllerBeta cameraController;
	public CharacterMovement characterMovement;

	private float effectTimer = 0f;
	private float fadeOutTime;
	private AudioSource aud;


	// Use this for initialization
	void Awake ()
	{
		aud = GetComponent<AudioSource> ();
	}

	void OnEnable ()
	{
		effectTimer = effectTime;
		fadeOutTime = effectTime / 5;
		characterMovement.reverseLeftRight = true;
		cameraController.reverseLeftRight = true;
		aud.volume = 1f;
		aud.Play ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (effectTimer <= 0) {
			characterMovement.reverseLeftRight = false;
			cameraController.reverseLeftRight = false;
			enabled = false;
			manager.onEffectEnd ();
		} else {
			effectTimer -= Time.deltaTime;
		}
		if (effectTimer <= fadeOutTime) {
			aud.volume = Mathf.Lerp (1, 0, (fadeOutTime - effectTimer) / fadeOutTime);
		}
	}
}
