/**
 * Team Titan
 *
 * PoHsien Wang
 */

using UnityEngine;
using System.Collections;

public class MedKitRotation : MonoBehaviour
{
	public Vector3 rotateAxis = Vector3.forward;
	public float rotateSpeed = 1f;
	public bool suspended = false;
	public bool loop = true;
	public float startDelay = 0f; // start immediately

	int count = 0;

	void Update ()
	{
		if (suspended)
			return;

		if (Time.time > startDelay) {
			if (count >= 1 && loop == false)
				return;

			transform.Rotate (rotateAxis, rotateSpeed * Time.deltaTime);
		}
	}

	public void setSuspend (bool flag)
	{
		suspended = flag;
	}

	public int getLoopCount ()
	{
		return count;
	}
}
