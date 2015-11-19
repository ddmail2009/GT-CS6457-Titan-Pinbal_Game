/**
 * Team Titan
 *
 * Meng-Hsin Tung
 **/

using UnityEngine;
using System.Collections;

public class BallGravity : MonoBehaviour
{
	public float increasedGravity = 35.2f;

	Rigidbody rig;

	void Awake ()
	{
		rig = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		rig.AddForce (-Vector3.forward * increasedGravity, ForceMode.Acceleration);
	}
}
