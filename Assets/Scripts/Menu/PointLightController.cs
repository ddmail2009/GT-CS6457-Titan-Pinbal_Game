/**
 * Team Titan
 * 
 * Meng-Hsin Tung
 */

using UnityEngine;
using System.Collections;

public class PointLightController : MonoBehaviour
{
	public float speed = 0.01f;
	public float maxRange = 20.0f, minRange = 10.0f;

	bool isIncreasing = false;
	Light light;
		
	void Awake ()
	{
		light = GetComponent <Light> ();
	}

	void Update ()
	{
		light.range += (isIncreasing) ? speed : -speed;

		if ((isIncreasing && light.range > maxRange) || (!isIncreasing && light.range < minRange)) {
			isIncreasing = !isIncreasing;

			if (!isIncreasing) {
				float r = Random.Range (0, 1f), g = Random.Range (0, 1f), b = Random.Range (0, 1f);
				float scale = 3.0f / (r + g + b);

				light.color = new Color (r * scale, g * scale, b * scale);
			}
		}
	}
}
