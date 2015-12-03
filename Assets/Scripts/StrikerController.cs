/**
 * Titan
 * 
 * Xiaoyu Chen
 */

using UnityEngine;
using System.Collections;

public class StrikerController : MonoBehaviour
{
	public string buttonName = "shoot";
	public string targetTag = "Ball";
	
	public float maxForce = 20000f;
	public float shrinkRateInSecond = 1f;
	public float maxShrinkRate = 0.75f;

	public float maxStrikeDistance = 4.5f;

	float shrinkRate = 0f;
	Vector3 oriScale;

	ParticleSystem strikeEffect;

	AudioSource aud;

	void Awake ()
	{
		oriScale = transform.localScale;
		aud = GetComponent<AudioSource> ();
		strikeEffect = GetComponentInChildren<ParticleSystem> ();
	}
	
	void Update ()
	{
		if (Input.GetButton (buttonName)) {
			if (shrinkRate < maxShrinkRate) {
				shrinkRate += shrinkRateInSecond * Time.deltaTime;
				ShrinkStriker ();
			}
		} else {
			if (shrinkRate > 0f) {
				RecoverStriker ();
				FireBall ();
				shrinkRate = 0f;
			}
		}
	}

	void FireBall ()
	{
		RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit, maxStrikeDistance)) {
			if (hit.collider.tag == targetTag) {
				float force = shrinkRate / maxShrinkRate * maxForce;
				hit.collider.attachedRigidbody.AddForce (transform.forward * force, ForceMode.Impulse);
				strikeEffect.Play ();
			}
		}
	}

	void ShrinkStriker ()
	{
		transform.localScale = oriScale - new Vector3 (0, 0, oriScale.z * shrinkRate);
	}
	
	void RecoverStriker ()
	{
		transform.localScale = oriScale;
		aud.Play ();
	}
}
