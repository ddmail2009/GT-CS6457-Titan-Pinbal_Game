/**
 * Team Titan
 * 
 * Meng-Hsin Tung
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class PlayerItem : MonoBehaviour
{
	public Sprite sprite;
	public abstract void Use ();

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player") {
			PlayerItemManager mgr = col.gameObject.GetComponent<PlayerItemManager> ();

			mgr.AddItem (this);
			gameObject.SetActive (false);
		}
	}
}
