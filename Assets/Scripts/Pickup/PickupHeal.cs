/**
 * Team Titan
 *
 * Xiaoyu Chen
 */

using UnityEngine;
using System.Collections;

public class PickupHeal : MonoBehaviour
{
	public int healValue = 10;
	public float destroySpeed = 0f;
	public string targetTag = "Player";
	public AudioClip clip = null;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == targetTag) {
			PlayerHealthManager.instance.Heal (healValue);

			AudioSource.PlayClipAtPoint (clip, transform.position);
			Destroy (gameObject, destroySpeed);
		}
	}
}
