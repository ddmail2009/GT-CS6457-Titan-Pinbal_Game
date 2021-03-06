﻿/**
 * Team Titan
 *
 * Xiaoyu Chen
 */

using UnityEngine;
using System.Collections;

public class MedKitHeal : MonoBehaviour
{
	public int healValue = 10;
	public float destroySpeed = 0f;
	public string targetTag = "Player";
	public AudioClip clip = null;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == targetTag) {
			VersusManager.instance.ChangeValueBy (healValue);

			AudioSource.PlayClipAtPoint (clip, transform.position);
			Destroy (gameObject, destroySpeed);
		}
	}
}
