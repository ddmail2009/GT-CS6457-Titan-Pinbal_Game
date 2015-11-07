/**
 * Titan
 * 
 * PoHsien Wang
 * 
 * Protal Sound Manager Script
 * Note: Portal must have at least two AudioSources(one for environmental Sound, one for sound effect)
 * @param IdleSound sound when no balls in buffer
 * @param BallWaiting sound when balls in buffer
 * @param BallLaunching sound when launching balls
 * @param BallEntering sound when balls enter portal
 * @param backgroundVolume volume of idle & waiting sound
 * @param effectVolume volume of launching & entering sound
 * 
 * public method
 * @method setIdle(bool) switch to idle sound or not
 * @method ballLaunch() play BallLaunching
 * @method ballEnter() play BallEntering
 * @method ballBuffer() play BallWaiting
 */

using UnityEngine;

public class PortalSoundManager : MonoBehaviour
{
	public AudioClip IdleSound;
	public AudioClip BallWaiting;

	public AudioClip BallLaunching;
	public AudioClip BallEntering;
	public float backgroundVolume = 0.2f;
	public float effectVolume = 1f;


	private AudioSource[]sources;
	void Start ()
	{
		sources = GetComponents<AudioSource> ();
		backgroundSoundSet (IdleSound);
	}

	void backgroundSoundSet (AudioClip clip)
	{
		sources [0].Stop ();
		sources [0].clip = clip;
		sources [0].loop = true;
		sources [0].volume = backgroundVolume;
		sources [0].Play ();
	}

	public void setIdle (bool idle)
	{
		if (idle)
			backgroundSoundSet (IdleSound);
	}

	public void ballLaunch ()
	{
		sources [1].PlayOneShot (BallLaunching);
	}

	public void ballBuffer ()
	{
		backgroundSoundSet (BallWaiting);
	}

	public void ballEnter ()
	{
		sources [1].PlayOneShot (BallEntering);
	}
}
