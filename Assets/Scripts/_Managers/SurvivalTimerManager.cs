using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SurvivalTimerManager : MonoBehaviour
{
	Text survivalTimerText;
	float timer = 0.0f;
	float lastTimer = 0.0f;

	void Awake ()
	{
		survivalTimerText = GetComponent <Text> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (!PlayerHealthManager.instance.isDead && BallManager.instance.numOfBalls > 0) {
			timer += Time.deltaTime;

			if (timer - lastTimer >= 0.1f) {
				survivalTimerText.text = "Time: " + timer.ToString ("F1");
				lastTimer = timer;
			}
		}
	}
}
