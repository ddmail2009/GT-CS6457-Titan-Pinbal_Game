/**
 * Team Titan
 *
 * PoHsien Wang
 **/

using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour
{
	public float force = 20.0f;

	void OnCollisionEnter (Collision col)
	{
		if (col.rigidbody && col.gameObject.tag == "Ball") {
			Vector3 forceVec = Vector3.ProjectOnPlane (col.transform.position - transform.position, transform.up).normalized * force;
			col.rigidbody.AddForce (forceVec, ForceMode.VelocityChange);
		}
	}
}
