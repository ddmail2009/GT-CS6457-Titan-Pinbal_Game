/**
 * Titan
 * 
 * PoHsien Wang
**/

using UnityEngine;

public class PortalSoundManager : MonoBehaviour
{

	public string[] keys = {"Enter", "Idle", "Exit", "Open", "Close"};
	public AudioClip[] clips = {null, null, null, null, null};
	public bool[] isEnvSound = {false, true, false, false, false};

	public float backgroundVolume = 1f;
	public float effectVolume = 1f;

	private AudioSource[]sources;
	void Start ()
	{
		sources = GetComponents<AudioSource> ();
	}

	void backgroundSoundSet (AudioClip clip)
	{
		sources [0].Stop ();
		sources [0].clip = clip;
		sources [0].loop = true;
		sources [0].volume = backgroundVolume;
		sources [0].Play ();
	}

	public void play (string key)
	{
		AudioClip clip = null;
		bool isEnv = false;
		for (int i=0; i<keys.Length; i++) {
			if (key.Equals (keys [i])) {
				clip = clips [i];
				isEnv = isEnvSound [i];
			}
		}

		if (clip == null)
			return;
		if (!isEnv) {
			sources [1].PlayOneShot (clip);
		} else
			backgroundSoundSet (clip);
	}

	public void stop (string key)
	{
		AudioClip clip = null;
		bool isEnv = false;
		for (int i=0; i<keys.Length; i++) {
			if (key.Equals (keys [i])) {
				clip = clips [i];
				isEnv = isEnvSound [i];
			}
		}
		
		if (clip == null)
			return;
		if (!isEnv) {
			sources [1].Stop ();
		} else
			sources [0].Stop ();
	}
}
