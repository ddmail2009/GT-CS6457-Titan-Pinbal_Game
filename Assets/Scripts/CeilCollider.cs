/**
 * Team Titan
 *
 * Meng-Hsin Tung
 **/

using UnityEngine;
using System.Collections;

public class CeilCollider : MonoBehaviour
{
	public Collider playerCollider;
	// FIXME: Find a better way to deal with this
	void Awake ()
	{
		Physics.IgnoreCollision (playerCollider, GetComponent <Collider> ());
	}
}
