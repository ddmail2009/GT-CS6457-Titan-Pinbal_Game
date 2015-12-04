/**
 * Team Titan
 * 
 * Meng-Hsin Tung, Xiaoyu Chen
 */

using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
	public float animationDelay = 3f;
	public float restartDelay = 6f;

	Animator anim;
	bool triggered = false;

	void Awake ()
	{
		anim = GetComponent <Animator> ();
	}

	void Update ()
	{
		if (!triggered && VersusManager.instance.isEnded) {
			triggered = true;
			Invoke ("PlayAnimation", animationDelay);
			Invoke ("RestartLevel", restartDelay);
		}
	}

	void PlayAnimation ()
	{
		if (VersusManager.instance.winner == VersusManager.Winner.ATTACKER) {
			anim.SetTrigger ("AttackerWin");
		} else if (VersusManager.instance.winner == VersusManager.Winner.DODGER) {
			anim.SetTrigger ("DodgerWin");
		}
	}

	void RestartLevel ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
