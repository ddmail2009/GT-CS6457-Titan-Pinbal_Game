﻿/**
 * Team Titan
 *
 * PoHsien Wang, Meng-Hsin Tung
 **/

using UnityEngine;
using System.Collections;

public class FlipperController : MonoBehaviour
{
	public bool isFiring = false;
	public float force = 100.0f;
	public string buttonName = "Fire1";
	public Vector3 offset;

	Rigidbody flipperRigidbody;
	HingeJoint flipperHingeJoint;
	bool holdingAtMax;
	Quaternion rotationAtMax;

	AudioSource aud;
	bool flipperUpAudioPlayed = false;

	void Awake ()
	{
		flipperRigidbody = GetComponent <Rigidbody> ();
		flipperHingeJoint = GetComponent <HingeJoint> ();
		aud = GetComponent<AudioSource> ();
	}

	void FixedUpdate ()
	{
		if (Input.GetButton (buttonName)) {
			isFiring = true;

			if (!flipperUpAudioPlayed) {
				aud.Play ();
				flipperUpAudioPlayed = true;
			}

			if (Mathf.Abs (flipperHingeJoint.angle - flipperHingeJoint.limits.max) < 2.0f) {
				flipperRigidbody.isKinematic = true;
			}

			flipperRigidbody.AddForceAtPosition (transform.forward * force, transform.position + offset, ForceMode.Acceleration);

		} else {
			isFiring = false;
			flipperUpAudioPlayed = false;

			flipperRigidbody.isKinematic = false;
			flipperRigidbody.AddForceAtPosition (transform.forward * -force, transform.position + offset, ForceMode.Acceleration);

		}
	}
}
