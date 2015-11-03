using UnityEngine;
using System.Collections;

public class StrikerController : MonoBehaviour
{
	public string buttonName = "shoot";
	public string targetTag = "Ball";
	
	public float maxForce = 20000f;
	public float shrinkRateInSecond = 1f;
	public float maxShrinkRate = 0.75f;

	float shrinkRate;
	Vector3 oriScale;

	void Awake ()
	{
		shrinkRate = 0f;
		oriScale = transform.localScale;
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

		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			if (hit.collider.tag == targetTag) {
				float force = shrinkRate / maxShrinkRate * maxForce;
				hit.collider.attachedRigidbody.AddForce (transform.forward * force, ForceMode.Impulse);
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
	}
}
