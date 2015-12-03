using UnityEngine;
using System.Collections;

public class BallPassDetector : MonoBehaviour
{
	void OnTriggerEnter (Collider col)
	{
		BallManager.instance.BallFired ();
	}
}
