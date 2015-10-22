/**
 * Team Titan
 *
 * Meng-Hsin Tung, PoHsien Wang
 **/

using UnityEngine;
using System.Collections;

public class CharacterFootstep : MonoBehaviour
{
	public string[] tags;
	public AudioClip[] clips;

	AudioSource aud;
	Animator anim;

	int desiredClip = 0, clip = 0;

	static int groundLayer = LayerMask.NameToLayer ("GameBoard");

	void Awake ()
	{
		aud = GetComponent <AudioSource> ();
		anim = GetComponent <Animator> ();
	}

	void Update ()
	{
		float h = anim.GetFloat ("VelocityX"), v = anim.GetFloat ("VelocityZ");
		bool isGrounded = anim.GetBool ("IsGrounded");
		bool isLocomotionState = anim.GetCurrentAnimatorStateInfo (0).IsName ("Locomotion");

		HandleFootstepSound (isGrounded && isLocomotionState && (h != 0 || v != 0));
		HandleSoundPitch (h, v);
	}

	void HandleFootstepSound (bool shouldBePlaying)
	{
		if (aud.isPlaying) {
			if (!shouldBePlaying) {
				aud.Stop ();
			} else if (clip != desiredClip) {
				aud.Stop ();

				aud.clip = clips [desiredClip];
				aud.Play ();
			}
		} else if (shouldBePlaying) {
			aud.clip = clips [desiredClip];
			aud.Play ();
		}

		clip = desiredClip;
	}

	void HandleSoundPitch (float h, float v)
	{
		float theta = Mathf.Atan2 (v, h);
		if (theta > 0) {
			aud.pitch = 1f;
		} else {
			float x = 0f + Mathf.Cos (theta);
			float y = 0.5f + Mathf.Sin (theta);
			aud.pitch = Mathf.Sqrt (x * x + y * y);
		}
	}

	void OnCollisionStay (Collision col)
	{
		if (col.gameObject.layer == groundLayer) {
			desiredClip = 0;

			for (var i = 1; i < tags.Length; ++i) {
				if (col.gameObject.tag == tags [i]) {
					desiredClip = i;
					break;
				}
			}
		}
	}

	void OnCollisionExit (Collision col)
	{
		if (col.gameObject.layer == groundLayer) {
			desiredClip = 0;
		}
	}

}
