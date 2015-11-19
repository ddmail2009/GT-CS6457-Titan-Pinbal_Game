/**
 * Team Titan
 * 
 * Meng-Hsin Tung
 */

using UnityEngine;
using System.Collections;

public class PowerPelletItem : PlayerItem
{
	public float duration = 10.0f;

	public override void Use ()
	{
		PowerPelletManager mgr = GameObject.FindGameObjectWithTag ("Player").GetComponent <PowerPelletManager> ();

		mgr.Enable (duration);
	}
}
