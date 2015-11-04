/**
 * Titan
 * 
 * Meng-Hsin Tung
 * 
 * TODO: Use interface, subclass or reflection to remove anooying switch statement
 */

using UnityEngine;
using System.Collections;

public class MissileBallAI : MonoBehaviour
{
	public float normalGravity = 35.2f;
	public float tracingAdjustmentFrequency = 0.1f;
	public float tracingAdjustmentForce = 2.5f;
	public float tracingStopDistance = 5.0f;
	public float returningStopDistance = 5.0f;
	public int numOfPursuits = 3;
	
	public enum States
	{
		Init,
		Normal,
		Tracing,
		Returning
	}

	Rigidbody rig;
	NavMeshAgent agent;
	Vector3 groundNormal;
	States prevState = States.Init, currState = States.Init, nextState = States.Tracing;
	Transform selectedTarget, playerTransform;
	Rigidbody playerRig;
	float tracingTimer = 0.0f;
	float distToPlayer;
	
	Light stateLight;
	GameObject[] flippers;

	void Awake ()
	{
		flippers = GameObject.FindGameObjectsWithTag ("Flipper");

		agent = GetComponent <NavMeshAgent> ();
		rig = GetComponent <Rigidbody> ();
		stateLight = GetComponent <Light> ();

		groundNormal = GameObject.Find ("GameBoard").transform.up;
	}

	void Update ()
	{
		if (currState == nextState) {
			switch (currState) {
			case States.Normal:
				normal_update ();
				break;
			case States.Returning:
				returning_update ();
				break;
			case States.Tracing:
				tracing_update ();
				break;
			}
		} else {
			// call state_exit method
			switch (currState) {
			case States.Normal:
				normal_exit ();
				break;
			case States.Returning:
				returning_exit ();
				break;
			case States.Tracing:
				tracing_exit ();
				break;
			}

			// call state_enter method
			switch (nextState) {
			case States.Normal:
				normal_enter ();
				break;
			case States.Returning:
				returning_enter ();
				break;
			case States.Tracing:
				tracing_enter ();
				break;
			}

			// switch state
			Debug.Log ("State transition: " + currState.ToString () + " -> " + nextState.ToString ());
			prevState = currState;
			currState = nextState;

			// TODO: change color
		}
	}
	
	void normal_enter ()
	{
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;

		distToPlayer = Vector3.Distance (transform.position, playerTransform.position);
	}
	
	void normal_update ()
	{
		rig.AddForce (Vector3.down * normalGravity, ForceMode.Acceleration);

		// Conditions to switch to returning state.
		float currDistToPlayer = Vector3.Distance (playerTransform.position, transform.position);

		if (prevState == States.Tracing && distToPlayer > currDistToPlayer) {
			if (numOfPursuits > 0) {
				nextState = States.Returning;
			} else {
				prevState = States.Normal;
			}
		}

		distToPlayer = currDistToPlayer;
	}

	void normal_exit ()
	{
		playerTransform = null;
	}

	void tracing_enter ()
	{
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		playerRig = GameObject.FindGameObjectWithTag ("Player").GetComponent <Rigidbody> ();

		stateLight.color = new Color (1.0f, 0f, 0f);
		stateLight.enabled = true;
	}

	void tracing_update ()
	{

		if (Vector3.Distance (Vector3.ProjectOnPlane (transform.position, groundNormal),
							  Vector3.ProjectOnPlane (playerTransform.position, groundNormal)) < tracingStopDistance) {
			nextState = States.Normal;
		}

		if (tracingTimer < tracingAdjustmentFrequency) {
			tracingTimer += Time.deltaTime;
			return;
		} else {
			tracingTimer = 0.0f;
		}

		// Calculate AdjustmentForce
		// AdjustmentForce = DirectionAdjustment + SpeedAdjustment.
		Vector3 targetEstimatedPos = playerTransform.position + playerRig.velocity * tracingAdjustmentFrequency;
		Vector3 currVel = rig.velocity;
		Vector3 desiredVel = (targetEstimatedPos - transform.position) * currVel.magnitude - Physics.gravity * tracingAdjustmentFrequency;
		desiredVel = Vector3.ProjectOnPlane (desiredVel, groundNormal);
		Vector3 velDiff = desiredVel - currVel;

		if (velDiff.magnitude <= tracingAdjustmentForce) {
			velDiff += desiredVel * (tracingAdjustmentForce - velDiff.magnitude);
		} else {
			velDiff.Normalize ();
			velDiff *= tracingAdjustmentForce;
		}

		rig.AddForce (velDiff, ForceMode.VelocityChange);
	}

	void tracing_exit ()
	{
		playerTransform = null;
		playerRig = null;

		stateLight.enabled = false;
	}

	void returning_enter ()
	{
		float minDistance = float.MaxValue;

		foreach (GameObject obj in flippers) {
			Transform t = obj.transform;
			float targetDistance = Vector3.Distance (transform.position, t.position);

			if (targetDistance < minDistance) {
				selectedTarget = t;
				minDistance = targetDistance;
			}
		}

		Debug.Log (selectedTarget);

		agent.enabled = true;

		Vector3 rigVelocity = rig.velocity;
		rig.velocity = Vector3.zero;
		agent.velocity = rigVelocity;
	}

	void returning_update ()
	{
		agent.destination = selectedTarget.position;

		if (Vector3.Distance (transform.position, selectedTarget.position) < returningStopDistance) {
			nextState = States.Normal;
		}
	}

	void returning_exit ()
	{
		rig.AddForce (agent.velocity, ForceMode.VelocityChange);
		agent.enabled = false;

		selectedTarget = null;
	}
	
	void OnCollisionEnter (Collision col)
	{
		foreach (GameObject obj in flippers) {
			FlipperController f = obj.GetComponent<FlipperController> ();
			if (currState != States.Tracing && f.isFiring && col.gameObject == f.gameObject && numOfPursuits > 0) {
				nextState = States.Tracing;
				numOfPursuits -= 1;
			}
		}
	}
}
