/**
 * Team Titan
 * 
 * Meng-Hsin Tung
 */

using UnityEngine;
using System.Collections;

public class BombItem : PlayerItem
{
	public GameObject bombTemplate;

	public override void Use ()
	{
		Vector3 bombPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		bombPos += bombTemplate.transform.position;

		Instantiate (bombTemplate, bombPos, Quaternion.identity);
	}
}
