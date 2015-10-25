/**
 * Team Titan
 *
 * Meng-Hsin Tung, PoHsien Wang
 *
 * Jump action is based on a script found by Zizheng Wu
 **/

using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	public float jumpHeight = 5.0f;
	public float groundCheckDistance = 0.3f;
	public bool freeMovementEnabled = false;

	public Camera playerCamera;

	Animator anim;
	Rigidbody rig;
	CapsuleCollider col;
	
	float colHeight;
	Vector3 colCenter, desiredColCenter;

	bool isGrounded;
	float origGroundCheckDistance;
//	Vector3 groundNormal;

	static int groundLayer = LayerMask.NameToLayer ("GameBoard");
	
	void Awake ()
	{
		anim = GetComponent <Animator> ();
		rig = GetComponent <Rigidbody> ();
		col = GetComponent<CapsuleCollider> ();
		colHeight = col.height;
		desiredColCenter = colCenter = col.center;

		origGroundCheckDistance = groundCheckDistance;
	}

	void FixedUpdate ()
	{
		Debug.DrawRay (transform.position, rig.velocity);

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		bool isCrouching = Input.GetButton ("Crouch");

		if (freeMovementEnabled) {
			anim.applyRootMotion = false;
			return;
		}

		CheckGroundStatus ();

		HandleRotation (h, v);
		HandleMovement (h, v);
		HandleJumping (h, v);
		HandleCrouch (isCrouching);

		UpdateColliderHeight ();
	}

	void HandleRotation (float h, float v)
	{
		if (h == 0 && v == 0) {
			return;
		}

		Quaternion desiredRotation = Quaternion.LookRotation (Vector3.ProjectOnPlane (playerCamera.transform.forward, transform.up));

		transform.rotation = desiredRotation;
	}

	void HandleMovement (float h, float v)
	{
		anim.SetFloat ("VelocityZ", v);
		anim.SetFloat ("VelocityX", h);
	}

	void HandleJumping (float h, float v)
	{
		// Handle jump state
		if (isGrounded && anim.GetCurrentAnimatorStateInfo (0).IsName ("Locomotion") && Input.GetButton ("Jump")) {
			isGrounded = false;
			anim.applyRootMotion = false;
			groundCheckDistance = 0.01f;

			// let jump force count wasd
			Vector3 jumpForce = Vector3.up * jumpHeight + transform.forward * v + transform.right * h;
			rig.AddForce (jumpForce, ForceMode.VelocityChange);
		} else if (!isGrounded) {
			groundCheckDistance = (rig.velocity.y < 0) ? origGroundCheckDistance : 0.01f;
		}

		// Setup Animator variables
		anim.SetBool ("IsGrounded", isGrounded);
		if (!isGrounded) {
			anim.SetFloat ("Jump", rig.velocity.y);
		}
	}

	void HandleCrouch (bool isCrouching)
	{
		anim.SetBool ("IsCrouching", isCrouching);
	}

	void UpdateColliderHeight ()
	{
		col.height = colHeight * anim.GetFloat ("ColHeight");

		desiredColCenter.y = colCenter.y * anim.GetFloat ("ColPosOffsetY");
		col.center = desiredColCenter;
	}

	void CheckGroundStatus ()
	{
		RaycastHit hitInfo;

		Debug.DrawLine (new Vector3 (col.bounds.center.x, col.bounds.min.y + col.radius, col.bounds.center.z),
		                new Vector3 (col.bounds.center.x, col.bounds.max.y + col.radius, col.bounds.center.z),
		                Color.green);

		if (Physics.CapsuleCast (new Vector3 (col.bounds.center.x, col.bounds.min.y + col.radius + 0.1f, col.bounds.center.z),
		                         new Vector3 (col.bounds.center.x, col.bounds.max.y + col.radius + 0.1f, col.bounds.center.z),
		                         col.radius, Vector3.down, out hitInfo, groundCheckDistance)) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}

		anim.applyRootMotion = isGrounded;
	}
}
