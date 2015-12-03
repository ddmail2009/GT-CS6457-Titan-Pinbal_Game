/**
 * Team Titan
 * 
 * PoHsien Wang
 */

using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour
{
	[Tooltip("The object tag which is allowed to into the portal")]
	public string
		objectTag = "Ball";
	[Tooltip("The object tag which is allowed to open the portal")]
	public string
		objectTagToOpen = "Ball";
	[Tooltip("The direction which is allowed to enter the portal")]
	public Vector3
		enterLocalDirection = Vector3.forward;
	[Tooltip("The least angle to portal surface to enter the portal")]
	public float
		enterAngle = 15;
	[Tooltip("The linked portal gameobject")]
	public GameObject[]
		linkPortals = null;
	[Tooltip("How many duplicate should be generated?")]
	public int
		DuplicateNumberMin = 1, DuplicateNumberMax = 5;


	[Tooltip("Should it open on start of the game?")]
	public bool
		openOnStart = true;
	[Tooltip("A delay time to open portal after start")]
	public float
		openOnStartDelay = 1.0f;
	[Tooltip("Should it close after every balls in it exit")]
	public bool
		closeOnLaunch = false;
	[Tooltip("How many times could the object enter the portal before it close? (0 for inf)")]
	public int
		closeOnEnter = 2;
	[Tooltip("Indicator group, open if all are On")]
	public PortalIndicatorControl[]
		indicators;

	[Tooltip("Delay for output")]
	public float
		outDelay = 5f;
	[Tooltip("Speed for output")]
	public float
		outSpeed = 20f;

	private Vector3 enterWorldDirection;
	private ArrayList buffer = new ArrayList ();
	private PortalSoundManager sound;
	private int curEnter = 0;

	void Start ()
	{
		enterWorldDirection = transform.TransformDirection (enterLocalDirection);
		sound = GetComponent<PortalSoundManager> ();

		if (openOnStart) {
			Invoke ("openPortal", openOnStartDelay);
		}
	}

	void OnDrawGizmosSelected ()
	{
		Vector3 td = transform.TransformDirection (enterLocalDirection);
		//if (linkPortals.Length > 0)
		//DebugExtension.DrawCone (transform.position, td, Color.red, 60);
	}

	bool isValidAngle (float angle)
	{
		return angle >= 90 + enterAngle || angle <= 90 - enterAngle;
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag.Equals (objectTag)) {

			Vector3 vin = col.attachedRigidbody.GetRelativePointVelocity (transform.position);
			float angle = Vector3.Angle (vin, enterWorldDirection);

			if (isValidAngle (angle)) {
				if (isOpen ()) {
					GameObject portal = null;
					int foundPortal = 0;

					int thisDuplicateNumber = Random.Range (DuplicateNumberMin, DuplicateNumberMax);

					for (int i=0; i<linkPortals.Length; i++) {
						if ((portal = linkPortals [Random.Range (0, linkPortals.Length)]) != null && foundPortal < thisDuplicateNumber) {
							GameObject obj = col.gameObject;
							if (foundPortal != 0)
								obj = Instantiate (col.gameObject);
							portalEnter (obj, portal);
							foundPortal += 1;
						}
					}
				} else if (col.tag.Equals (objectTagToOpen)) {
					// Debug.Log ("Open");
					for (int i=0; i<indicators.Length; i++) {
						if (indicators [i].isOn () == false) {
							indicators [i].setOn ();
							if (i != indicators.Length - 1)
								return;
							else {
								openPortal ();
							}
						}
					}
				}
			} 
		} 
	}

	void portalEnter (GameObject obj, GameObject portal)
	{
		curEnter += 1;
		obj.SetActive (false);
		sound.play ("Enter");

		PortalController[] controllers = portal.GetComponents<PortalController> ();
		for (int i=0; i<controllers.Length; i++) {
			if (controllers [i].objectTag.Equals (objectTag)) {
				controllers [i].addtoBuffer (obj);
			}
		}
		if (closeOnEnter != 0 && curEnter >= closeOnEnter) {
			closePortal ();
		}
	}
	
	
	void addtoBuffer (GameObject ball)
	{
		buffer.Add (ball);
		sound.play ("Idle");

		// out degree -60/60 with out portal forward direction
		Vector3 linkPortalsF = transform.TransformDirection (Vector3.forward);
		Vector3 linkPortalsH = transform.TransformDirection (Vector3.right);
		float forwardSpeed = Random.Range (0.5f, 1f) * outSpeed;
		float horizontalSpeed = Mathf.Sqrt (outSpeed * outSpeed - forwardSpeed * forwardSpeed) * Random.Range (-1, 2);

		ball.GetComponent<Rigidbody> ().velocity = linkPortalsF * forwardSpeed + linkPortalsH * horizontalSpeed;
		ball.transform.position = transform.position + linkPortalsF;

		if (!isOpen () && !(outDelay < 0.1 && closeOnLaunch))
			openPortal ();
		Invoke ("Fire", outDelay);
	}
	
	void Fire ()
	{
		GameObject obj = (GameObject)buffer [0];
		buffer.RemoveAt (0);
		obj.SetActive (true);

		sound.play ("Exit");
		if (buffer.Count == 0 && closeOnLaunch) {
			sound.stop ("Idle");
			// print ("closePortal");
			closePortal ();
		}
	}
	
	public bool isOpen ()
	{
		return GetComponent<EllipsoidParticleEmitter> ().emit;
	}

	public void openPortal ()
	{
		GetComponent<EllipsoidParticleEmitter> ().emit = true;

		sound.play ("Open");
		sound.play ("Idle");
	}

	public void closePortal ()
	{
		GetComponent<EllipsoidParticleEmitter> ().emit = false;

		sound.stop ("Idle");
		sound.play ("Close");
		curEnter = 0;
		for (int i=0; i<indicators.Length; i++) {
			indicators [i].setOff ();
		}
	}
}
