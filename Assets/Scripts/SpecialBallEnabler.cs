using UnityEngine;
using System.Collections;

public class SpecialBallEnabler : MonoBehaviour
{
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Ball") {
			if (Vector3.Angle (col.attachedRigidbody.velocity, transform.forward) < 90.0f) {
				return;
			}

			// Currently, we have only one kind of special balls.
			col.gameObject.GetComponent<BallGravity> ().enabled = false;
			col.gameObject.GetComponent<MissileBallAI> ().enabled = true;
		}
	}
}
