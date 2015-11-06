using UnityEngine;
using System.Collections;

public class RockControl : MonoBehaviour
{
	public float size;

	private Rigidbody rockBody;
	private int groundLayerMask;
	private AudioSource aud;
	private bool played = false;
	private float timeToLive = 5;
	private float timer;

	// Use this for initialization
	void Start ()
	{
		transform.localScale = new Vector3 (Random.Range (0.015f, 0.03f), Random.Range (0.015f, 0.03f), Random.Range (0.015f, 0.03f)) * size;
		transform.localRotation = Random.rotation;
		rockBody = GetComponent <Rigidbody> ();
		rockBody.mass = size * 5.0f;
		groundLayerMask = LayerMask.NameToLayer ("GameBoard");
		aud = GetComponent <AudioSource> ();
		timer = timeToLive;
	}

	void Disappear ()
	{
		Destroy (gameObject);
	}

	void Update ()
	{
		if (timer <= 0) {
			Disappear ();
		} else {
			timer -= Time.deltaTime;
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.layer == groundLayerMask && !played) {
			aud.Play ();
			played = true;
		} else if (col.gameObject.tag == "Player") {
			PlayerHealthManager.instance.TakeDamage (10);
		}
	}
}
