/**
 * Team Titan
 * 
 * Tzu-Wei Huang
 */

using UnityEngine;
using System.Collections;

public class RockSpawner : Effect
{
	public GameObject player;
	public GameObject ground;
	public GameObject rockTemplate;
	public float effectTime = 10f;
	public float endDelay = 5f;
	
	private float timer = 0f;
	private float effectTimer = 0f;
	private float fadeOutTime;
	private Bounds bounds;
	private AudioSource aud;
	
	// Use this for initialization
	
	void Awake ()
	{
		bounds = ground.GetComponent<Renderer> ().bounds;
		aud = GetComponent<AudioSource> ();
	}
	
	void OnEnable ()
	{
		effectTimer = effectTime;
		fadeOutTime = effectTime / 10;
		timer = 0;
		aud.volume = 1f;
		aud.Play ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (effectTimer <= -endDelay) {
			enabled = false;
			manager.onEffectEnd ();
		} else {
			effectTimer -= Time.deltaTime;
		}
		
		if (effectTimer <= fadeOutTime) {
			aud.volume = Mathf.Lerp (1, 0, (fadeOutTime - effectTimer) / (endDelay + fadeOutTime));
		}
		
		if (effectTimer > 0 && timer <= 0) {
			for (int i = 0; i < 10; i++) {
				RandomRock ();
			}
			timer = Random.Range (0.5f, 1f);
		} else {
			timer -= Time.deltaTime;
		}
	}
	
	void RandomRock ()
	{
		float y, x, z;
		y = Random.Range (30f, 35f);
		if (Random.value < 0.5f || !player) {
			x = Random.Range (bounds.center.x - bounds.extents.x, bounds.center.x + bounds.extents.x);
			z = Random.Range (bounds.center.z - bounds.extents.z, bounds.center.z + bounds.extents.z);
		} else {
			Rigidbody playerRigibody = player.GetComponent<Rigidbody> ();
			x = player.transform.position.x + playerRigibody.velocity.x;
			z = player.transform.position.z + playerRigibody.velocity.z;
		}
		GameObject rock = Instantiate (rockTemplate, new Vector3 (x, y, z), Quaternion.identity) as GameObject;
		RockControl rockControl = rock.GetComponent <RockControl> ();
		rockControl.size = Random.Range (1f, 2f);
	}
}