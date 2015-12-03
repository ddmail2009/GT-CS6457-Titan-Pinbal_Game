using UnityEngine;
using System.Collections;

public class BallPassDetector : MonoBehaviour
{
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Ball") {
			BallManager.instance.BallFired ();
		}
	}
}
