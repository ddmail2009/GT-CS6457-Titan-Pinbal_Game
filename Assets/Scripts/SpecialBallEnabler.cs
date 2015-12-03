/**
 * Titan
 * 
 * Meng-Hsin Tung
 */

using UnityEngine;
using System.Collections;

public class SpecialBallEnabler : MonoBehaviour
{
	public float[] tracingStartDistances;
	public int[] numOfPursuits;
	public float[] probabilities;

	float totalProbability = 0;

	void Awake ()
	{
		foreach (float p in probabilities) {
			totalProbability += p;
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Ball") {
			if (Vector3.Angle (col.attachedRigidbody.velocity, transform.forward) < 90.0f) {
				return;
			}

			MissileBallAI mbai = col.gameObject.GetComponent<MissileBallAI> ();

			if (!mbai.enabled) {
				col.gameObject.GetComponent<BallGravity> ().enabled = false;

				int idx = getRandomIdx ();
				mbai.tracingStartDistance = tracingStartDistances [idx];
				mbai.numOfPursuits = numOfPursuits [idx];

				mbai.enabled = true;
			}
		}
	}

	int getRandomIdx ()
	{
		float p = Random.Range (0, totalProbability);

		for (var i = 0; i < probabilities.Length; ++i) {
			if (p <= probabilities [i]) {
				return i;
			}

			p -= probabilities [i];
		}

		return probabilities.Length - 1;
	}
}
