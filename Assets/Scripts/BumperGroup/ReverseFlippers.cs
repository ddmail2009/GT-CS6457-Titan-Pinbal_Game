using UnityEngine;
using System.Collections;

public class ReverseFlippers : Effect {
	public float effectTime = 20f;
	public FlipperController leftFlipper;
	public FlipperController rightFlipper;

	private float effectTimer = 0f;
	private float fadeOutTime;
	private AudioSource aud;

	// Use this for initialization
	void Awake () {
		aud = GetComponent<AudioSource> ();
	}

	void OnEnable() {
		effectTimer = effectTime;
		fadeOutTime = effectTime / 5;

		swapFlipperButton ();

		aud.volume = 1f;
		aud.Play ();
	}

	void swapFlipperButton() {
		string tmp;
		tmp = leftFlipper.buttonName;
		leftFlipper.buttonName = rightFlipper.buttonName;
		rightFlipper.buttonName = tmp;
	}

	// Update is called once per frame
	void Update () {
		if (effectTimer <= 0) {
			swapFlipperButton ();
			enabled = false;
			manager.onEffectEnd ();
		} else {
			effectTimer -= Time.deltaTime;
		}
		if (effectTimer <= fadeOutTime) {
			aud.volume = Mathf.Lerp(1, 0, (fadeOutTime - effectTimer)/fadeOutTime);
		}
	}
}
