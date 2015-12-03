/**
 * Team Titan
 * 
 * PoHsien Wang
 */

using UnityEngine;
using System.Collections;

public class ShockWaveforce : MonoBehaviour
{
	public float maxSize = 5f;
	public float velocityForce = 20f;
	public float radiusCurveDamp = 5f;
	public float destroyTime = 3f;

	ParticleSystem ps;
	void Start ()
	{
		ps = GetComponent<ParticleSystem> ();
		Invoke ("Play", ps.startLifetime);
	}

	IEnumerator applyForce (float waitSecond, float radius)
	{
		yield return new WaitForSeconds (waitSecond);

		Vector3 position = transform.TransformPoint (new Vector3 (0, 0, 0));
		Debug.DrawLine (position, position + transform.forward * radius);
		Debug.DrawLine (position, position - transform.forward * radius);
		Debug.DrawLine (position, position - transform.right * radius);
		Debug.DrawLine (position, position + transform.right * radius);
		
		Collider[] colliders = Physics.OverlapSphere (position, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody> ();
			
			if (rb != null && rb.gameObject != this.gameObject && rb.gameObject.tag != "Player" && rb.GetComponent<Rigidbody> () != null) {
				rb.AddExplosionForce (velocityForce, position, radius, 0.0F, ForceMode.VelocityChange);
			}
		}
	}
	void Play ()
	{
		ps.Play ();

		for (float i=0f; i<ps.startLifetime; i+=0.002f) {
			StartCoroutine (applyForce (i, maxSize - Mathf.Pow (maxSize, 1 - radiusCurveDamp * i / ps.startLifetime)));
		}
		Invoke ("selfDestroy", destroyTime);
	}

	void selfDestroy ()
	{
		Destroy (this.gameObject);
	}
}
