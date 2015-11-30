/**
 * Team Titan
 *
 * Xiaoyu Chen
 **/

using UnityEngine;
using System.Collections;

public class BallAttack : MonoBehaviour
{
	public int attackDamage = 40;

	bool isDetecting = true;

	void OnCollisionEnter (Collision col)
	{
		if (isDetecting && col.gameObject.tag == "Player") {
			VersusManager.instance.ChangeValueBy (-attackDamage);
			isDetecting = false;

			Invoke ("ResumeCollision", 3.0f);
		}
	}

	void ResumeCollision ()
	{
		isDetecting = true;
	}
}
