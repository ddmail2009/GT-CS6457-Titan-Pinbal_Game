using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour
{
	public Transform orbitCenter;
	public float orbitDistance;
	public float orbitSpeed;

	void LateUpdate ()
	{
		Vector3 towardOrbitCenter = orbitCenter.position - transform.position;
		towardOrbitCenter += -transform.right * orbitSpeed;

		towardOrbitCenter = towardOrbitCenter.normalized * orbitDistance;

		transform.position = orbitCenter.position - towardOrbitCenter;
		transform.LookAt (orbitCenter.position);
	}
}
