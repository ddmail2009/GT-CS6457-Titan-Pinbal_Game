/**
 * Team Titan
 * 
 * Meng-Hsin Tung
 */

using UnityEngine;
using System.Collections;

public class PowerPelletManager : MonoBehaviour
{
	public static PowerPelletManager instance;
	public AudioClip clip;
	public bool isEnabled = false;

	float _timer = 0f, _duration = 0f;
	ParticleSystem effect;
	
	public void Enable (float duration)
	{
		AudioSource.PlayClipAtPoint (clip, transform.position, 1.0f);
		_duration += duration;

		isEnabled = true;
		effect.Play ();

		VersusManager.instance.isLocked = true;
	}

	void Disable ()
	{
		_duration = 0f;
		_timer = 0f;
		isEnabled = false;
		effect.Stop ();

		VersusManager.instance.isLocked = false;
	}

	void Awake ()
	{
		instance = this;
		effect = transform.Find ("PowerPelletEffect").GetComponent<ParticleSystem> ();
	}
	
	void Update ()
	{
		if (isEnabled) {
			_timer += Time.deltaTime;

			if (_timer > _duration) {
				Disable ();
			}
		}
	}
}
