using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
	Animator anim;

	float restartTimer;

	void Awake ()
	{
		anim = GetComponent <Animator> ();
	}

	void Update ()
	{
		if (PlayerHealthManager.instance.isDead) {
			anim.SetTrigger ("GameOver");

			restartTimer += Time.deltaTime;

			if (restartTimer >= 3.0f) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
}
