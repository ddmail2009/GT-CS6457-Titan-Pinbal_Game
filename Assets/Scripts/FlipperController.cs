/**
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

	Animator anim;
	float damageTimer, damageDuration = 0;
	bool isDamaged = false;

	public void TakeDamage (float duration)
	{
		if (isDamaged) {
			damageDuration += duration;
		} else {
			damageTimer = 0;
			damageDuration = duration;

			isDamaged = true;

			anim.SetBool ("IsDamaged", true);
		}
	}

	void Awake ()
	{
		flipperRigidbody = GetComponent <Rigidbody> ();
		flipperHingeJoint = GetComponent <HingeJoint> ();
		aud = GetComponent <AudioSource> ();
		anim = GetComponent <Animator> ();
	}

	void Update ()
	{
		if (isDamaged) {
			damageTimer += Time.deltaTime;

			if (damageTimer > damageDuration) {
				isDamaged = false;
				anim.SetBool ("IsDamaged", false);
			}
		}
	}

	void FixedUpdate ()
	{
		if (!isDamaged && Input.GetButton (buttonName)) {
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
