using UnityEngine;
using System.Collections;

public class VersusManager : MonoBehaviour
{
	public static VersusManager instance;

	public float decaySpeed = 0;
	public float minValue = 0, maxValue = 100f, startValue = 50f;
	public float currValue;

	public bool isStarting = false, isEnded = false;

	public void ChangeValueBy (float diff)
	{
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
		if (isStarting && !isEnded) {
			currValue -= decaySpeed * Time.deltaTime;
		}
	}
}
