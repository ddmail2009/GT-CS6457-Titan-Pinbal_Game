/**
 * Titan
 * 
 * PoHsien Wang
 * 
 * Protal Controller Script
 * @param objectTag objects with which tag are allowed to enter
 * @param enterLocalDirection only allow entering the portal in this direction(local representation)
 * @param linkPortals linkedPortals
 * 
 * @param outDelay how long should the ball stays in its buffer
 * @param outSpeed the speed of the ball when launching from portal
 */

using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour
{
	public string objectTag = "Ball";
	public Vector3 enterLocalDirection = Vector3.forward;
	public GameObject[] linkPortals = null;

	public float outDelay = 5f;
	public float outSpeed = 20f;


	private Vector3 enterWorldDirection;
	private ArrayList ballBuffer = new ArrayList ();
	void Start ()
	{
		enterWorldDirection = transform.TransformDirection (enterLocalDirection);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag.Equals (objectTag)) {
			Vector3 vin = col.attachedRigidbody.GetRelativePointVelocity (transform.position);
			float t = Vector3.Dot (vin, enterWorldDirection);
			if (!Mathf.Approximately (t, 0f) && t < 0) {
				GameObject portal = null;
				for (int i=0; i<linkPortals.Length; i++)
					if ((portal = linkPortals [Random.Range (0, linkPortals.Length)]) != null)
						break;
				if (portal != null) {
					col.gameObject.SetActive (false);
					GetComponent<PortalSoundManager> ().ballEnter ();

					portal.GetComponent<PortalController> ().addBalltoBuffer (col.gameObject);
				}
			}
		}
	}


	void addBalltoBuffer (GameObject ball)
	{
		ballBuffer.Add (ball);
		GetComponent<PortalSoundManager> ().ballBuffer ();

		// out degree -60/60 with out portal forward direction
		Vector3 linkPortalsF = transform.TransformDirection (Vector3.forward);
		Vector3 linkPortalsH = transform.TransformDirection (Vector3.right);
		float forwardSpeed = Random.Range (0.5f, 1f) * outSpeed;
		float horizontalSpeed = Mathf.Sqrt (outSpeed * outSpeed - forwardSpeed * forwardSpeed) * Random.Range (-1, 2);

		ball.GetComponent<Rigidbody> ().velocity = linkPortalsF * forwardSpeed + linkPortalsH * horizontalSpeed;
		ball.transform.position = transform.position + linkPortalsF;

		Invoke ("FireBall", outDelay);
	}

	// Fire ball through the portal
	void FireBall ()
	{
		GameObject ball = (GameObject)ballBuffer [0];
		ballBuffer.RemoveAt (0);
		ball.SetActive (true);
		GetComponent<PortalSoundManager> ().ballLaunch ();
		GetComponent<PortalSoundManager> ().setIdle (ballBuffer.Count == 0);
	}
}
