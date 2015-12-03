/**
 * Team Titan
 * 
 * Tzu-Wei Huang
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BumperManager : MonoBehaviour
{
	public Bumper[] bumpers;
	public Effect[] effects;
	public Sprite[] effectSprites;
	public int[] probabilities;
	public int threshold = 3;
	public Animator effectImageAnimator;
	public Image effectImage;

	private int targetIndex = -1;
	private int counter = 0;
	private int sumProb = 0;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < bumpers.Length; i++) {
			bumpers [i].index = i;
			bumpers [i].manager = this;
		}

		randomSelectBumper ();

		for (int i = 0; i < probabilities.Length; i++) {
			sumProb += probabilities [i];
		}

		/*
		for (int i = 0; i < 10; i++) {
			Debug.Log(randomEffectIndex());
		}
		*/
	}

	public void onEffectEnd ()
	{
		randomSelectBumper ();
	}

	int randomEffectIndex ()
	{
		int x = Random.Range (0, sumProb);
		int n = 0;
		for (int i = 0; i < probabilities.Length; i++) {
			if (x >= n && x < (n + probabilities [i])) {
				return i;
			}
			n += probabilities [i];
		}
		return -1;
	}

	void randomSelectBumper ()
	{
		if (targetIndex == -1) {
			targetIndex = Random.Range (0, bumpers.Length);
		} else {
			targetIndex = (targetIndex + Random.Range (1, bumpers.Length - 1)) % bumpers.Length;
		}
		bumpers [targetIndex].state = Bumper.BumperState.Targeted;

	}

	public void onBumperCollide (int index)
	{
		if (index == targetIndex) {
			bumpers [index].state = Bumper.BumperState.Idle;
			randomSelectBumper ();

			counter++;
			if (counter >= threshold) {
				counter = 0;
				for (int i = 0; i < bumpers.Length; i++) {
					bumpers [i].state = Bumper.BumperState.Idle;
					;
				}
				targetIndex = -1;
				int effectIndex = randomEffectIndex ();
				//Debug.Log(effectIndex);
				effectImage.sprite = effectSprites [effectIndex];
				effectImageAnimator.SetTrigger ("showImage");
				effects [effectIndex].BeginEffect (this);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
