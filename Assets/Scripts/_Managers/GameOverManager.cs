﻿using UnityEngine;
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
		if (!triggered && PlayerHealthManager.instance.isDead) {
			triggered = true;
			Invoke ("PlayAnimation", animationDelay);
			Invoke ("RestartLevel", restartDelay);
		}
	}

	void PlayAnimation ()
	{
		anim.SetTrigger ("GameOver");
	}

	void RestartLevel ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
