﻿/**
 * Team Titan
 * 
 * Meng-Hsin Tung
 */

using UnityEngine;
using System.Collections;

public class VersusManager : MonoBehaviour
{
	public static VersusManager instance;

	public float decaySpeed = 0;
	public float minValue = 0, maxValue = 100f, startValue = 50f;
	public float currValue;

	public bool isStarting = false, isEnded = false;
	public bool isLocked = false;

	public enum Winner
	{
		DODGER,
		ATTACKER
	}
	public Winner winner = Winner.DODGER;


	public GameObject ragdollTemplate;
	
	public void ChangeValueBy (float diff)
	{
		if (isEnded || isLocked && diff < 0) {
			return;
		}

		currValue += diff;
		if (currValue > maxValue) {
			currValue = maxValue;
		} else if (currValue < minValue) {
			currValue = minValue;
		}
	}


	void Awake ()
	{
		instance = this;

		currValue = startValue;
	}
	
	void Update ()
	{
		if (isEnded) {
			return;
		}

		if (currValue <= minValue) {
			isEnded = true;
			winner = Winner.DODGER;

			ReplacePlayerWithRagdoll ();
		} else if (currValue >= maxValue) {
			isEnded = true;
			winner = Winner.ATTACKER;
		} else if (isStarting) {
			currValue -= decaySpeed * Time.deltaTime;

			if (currValue < minValue) {
				currValue = minValue;
			} else if (currValue > maxValue) {
				currValue = maxValue;
			}
		}
	}

	void ReplacePlayerWithRagdoll ()
	{
		GameObject player = GameObject.FindWithTag ("Player");
		ThirdPersonCameraControllerBeta playerCamera = GameObject.FindWithTag ("ThirdPersonCamera").GetComponent<ThirdPersonCameraControllerBeta> ();
		
		GameObject ragdoll = Instantiate (ragdollTemplate, player.transform.position, player.transform.rotation) as GameObject;
		playerCamera.targetLookAt = ragdoll.transform.Find ("LookAt");

		Destroy (player);
	}
}

