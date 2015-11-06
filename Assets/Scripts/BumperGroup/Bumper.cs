/**
 * Team Titan
 *
 * PoHsien Wang
 **/

using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour
{
	public enum BumperState{
		Idle,
		Targeted
	};

	public float force = 20.0f;
	public int index;
	public BumperManager manager;
	public BumperState state {
		set {
			switch (value) {
				case BumperState.Targeted:
					render.material.color = Color.red;
					break;
				default:
					render.material.color = defaultColor;
				break;
			}
		}
	}

	private Renderer render;
	private Color defaultColor;
	private AudioSource aud;

	void Awake () {
		render = GetComponent<Renderer> ();
		defaultColor = render.material.color;
		aud = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.rigidbody && col.gameObject.tag == "Ball") {
			Vector3 forceVec = Vector3.ProjectOnPlane (col.transform.position - transform.position, transform.up).normalized * force;
			col.rigidbody.AddForce (forceVec, ForceMode.VelocityChange);
			aud.Play();
			if (manager) {
				manager.onBumperCollide(index);
			}
		}
	}
}
