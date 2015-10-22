/**
 * Team Titan
 *
 * Meng-Hsin Tung
 **/

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speedDampTime = 0.1f;
	public float turnSmoothing = 15f;
	public float jumpHeight = 5.0f;
	public float jumpColliderReduce = 0.6f;
	public Camera playerCamera;

	Animator anim;
	Rigidbody playerRigidbody;
	CapsuleCollider col;

	int groundMask;
	float colHeight, colJumpHeight;

	bool isGrounded = false;
	bool isJumping = false;

	static int jumpingState = Animator.StringToHash ("Base Layer.Jumping");
	static int fallingState = Animator.StringToHash ("Base Layer.Falling");

	void Awake ()
	{
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
		col = GetComponent<CapsuleCollider> ();
		colHeight = col.height;
		colJumpHeight = colHeight * jumpColliderReduce;

		groundMask = LayerMask.NameToLayer ("GameBoard");
	}

	void FixedUpdate ()
	{
		Debug.DrawRay (transform.position, playerRigidbody.velocity);

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		HandleRotation ();
		HandleMovement (h, v);
		HandleJumping (h, v);
	}

	void HandleMovement (float h, float v)
	{
		anim.SetFloat ("Speed", v);
		anim.SetFloat ("Direction", h);
	}

	void HandleRotation ()
	{
		Quaternion desiredRotation = Quaternion.LookRotation (Vector3.ProjectOnPlane (playerCamera.transform.forward, transform.up));

		transform.rotation = desiredRotation;
	}

	void HandleJumping (float h, float v)
	{
		if (isGrounded && !isJumping && Input.GetButton ("Jump")) {
			isJumping = true;
			isGrounded = false;

			// workaround to make jump action work
			Vector3 upwardAdjustment = playerRigidbody.position;
			upwardAdjustment.y += 0.1f;
			playerRigidbody.MovePosition (upwardAdjustment);

			// tweak: let jump force count wasd
			Vector3 jumpForce = Vector3.up * jumpHeight + transform.forward * v;
			playerRigidbody.AddForce (jumpForce, ForceMode.VelocityChange);

			anim.SetTrigger ("Jump");
		} else if (!isGrounded && isJumping && Vector3.Angle (playerRigidbody.velocity, Vector3.down) < 90f) {
			isJumping = false;
		}

		var currentBaseState = anim.GetCurrentAnimatorStateInfo (0);
		if ((currentBaseState.fullPathHash == jumpingState || currentBaseState.fullPathHash == fallingState)
			&& !Physics.Raycast (transform.position + Vector3.up, Vector3.down, 1.2f, 1 << groundMask)) {
			Debug.DrawRay (transform.position + Vector3.up, Vector3.down, Color.red);
			col.height = colJumpHeight;
		} else {
			col.height = colHeight;
		}

		anim.applyRootMotion = isGrounded;

		anim.SetBool ("IsGrounded", isGrounded);
		anim.SetBool ("IsJumping", isJumping);
	}

	void OnCollisionStay (Collision col)
	{
		if (col.gameObject.layer == groundMask) {
			isGrounded = true;
		}
	}

	void OnCollisionExit (Collision col)
	{
		if (col.gameObject.layer == groundMask) {
			isGrounded = false;
		}
	}
}
