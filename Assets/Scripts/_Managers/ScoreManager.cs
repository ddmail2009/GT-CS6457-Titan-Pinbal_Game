using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public static int score = 0;

	Text scoreText;

	void Start ()
	{
		scoreText = GetComponent<Text> ();
		scoreText.text = "Score: " + score;
	}

	void Update ()
	{
		scoreText.text = "Score: " + score;
	}
}
