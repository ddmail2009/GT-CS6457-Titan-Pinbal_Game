﻿using UnityEngine;
using System.Collections;

public class DeadZoneTrigger : MonoBehaviour
{
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Ball") {
			Destroy (col.gameObject, 3.0f);
		}
	}
}
