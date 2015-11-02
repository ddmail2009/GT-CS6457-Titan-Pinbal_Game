/**
 * Team Titan
 *
 * PoHsien Wang
 **/

using UnityEngine;
using System.Collections;

public class FlipperController : MonoBehaviour
{
	public float force = 100.0f;
	public string buttonName = "Fire1";
	public Vector3 offset;

	Rigidbody flipperRigidbody;
	HingeJoint flipperHingeJoint;
	bool holdingAtMax;
	Quaternion rotationAtMax;

	void Awake ()
	{
		flipperRigidbody = GetComponent <Rigidbody> ();
		flipperHingeJoint = GetComponent <HingeJoint> ();
	}

	void FixedUpdate ()
	{
		Debug.Log (transform.up);
		if (Input.GetButton (buttonName)) {
			if (Mathf.Approximately (flipperHingeJoint.angle, flipperHingeJoint.limits.max)) {
				flipperRigidbody.isKinematic = true;
			}
			// TEMP: use -transform.up instead of transform.forward
			flipperRigidbody.AddForceAtPosition (-transform.up * force, transform.position + offset, ForceMode.Acceleration);
		} else {
			flipperRigidbody.isKinematic = false;
			// TEMP: use -transform.up instead of transform.forward
			flipperRigidbody.AddForceAtPosition (-transform.up * -force, transform.position + offset, ForceMode.Acceleration);
		}
	}
}
