/**
 * Titan
 *
 * Xiaoyu Chen
 */

using UnityEngine;
using System.Collections;

public class CharacterFootsteps : MonoBehaviour
{
	public AudioClip stepClip;

	AudioSource aud;

	public void Awake ()
	{
		aud = GetComponent<AudioSource> ();
	}

	public void LeftFootstep ()
	{
		aud.PlayOneShot (stepClip);
	}

	public void RightFootstep ()
	{
		aud.PlayOneShot (stepClip);
	}
}
