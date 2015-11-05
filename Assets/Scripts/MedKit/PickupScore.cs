/**
 * Team Titan
 *
 * Meng-Hsin Tung
 */

using UnityEngine;
using UnityEngine.UI;

public class PickupScore: MonoBehaviour
{
	public int scoreValue = 10;
	public float destroySpeed = 0f;
	public string targetTag = "Player";

	public AudioClip clip = null;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			ScoreManager.score += scoreValue;
			AudioSource.PlayClipAtPoint (clip, transform.position);
			Destroy (gameObject, destroySpeed);
		}
	}
}
