/**
 * Team Titan
 * 
 * Meng-Hsin Tung
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerItemManager : MonoBehaviour
{
	public AudioClip clip;
	public Image icon;

	PlayerItem _currItem;

	public void AddItem (PlayerItem item)
	{
		AudioSource.PlayClipAtPoint (clip, transform.position);

		icon.sprite = item.sprite;
		icon.color = Color.white;

		item.GetComponent <ItemLifeTimeManager> ().CancelInvoke ();
		_currItem = item;
	}
	
	void Update ()
	{
		if (Input.GetButton ("Use") && _currItem) {
			icon.color = new Color (1f, 1f, 1f, 0f);
			icon.sprite = null;
			
			_currItem.Use ();
			_currItem = null;
		}
	}
}
