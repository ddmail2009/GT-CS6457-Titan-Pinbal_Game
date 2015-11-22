using UnityEngine;
using System.Collections;

public class RockEffectControl : MonoBehaviour
{
	public string triggerButton = "RockEffect";

	Effect rockSpawner;

	void Awake ()
	{
		rockSpawner = GetComponent<Effect> ();
	}
	
	void Update ()
	{
		if (Input.GetButtonUp (triggerButton)) {
			rockSpawner.enabled = true;
		}
	}
}
