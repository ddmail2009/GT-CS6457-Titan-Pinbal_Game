using UnityEngine;
using System.Collections;

public class BallIgnore : MonoBehaviour
{
	void Awake ()
	{
		Physics.IgnoreCollision (GetComponent<Collider> (), GameObject.Find ("PlayerWall").GetComponent<Collider> ());
	}
}
