using UnityEngine;
using System.Collections;

public class BallIgnore : MonoBehaviour
{
	void OnEnable ()
	{
		Physics.IgnoreCollision (GetComponent<Collider> (), GameObject.Find ("PlayerWall").GetComponent<Collider> ());
	}
}
