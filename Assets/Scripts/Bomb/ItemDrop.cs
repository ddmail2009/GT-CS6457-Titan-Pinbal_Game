using UnityEngine;
using System.Collections;

public class ItemDrop : MonoBehaviour
{
	public GameObject[] items;
	public float[] probabilities;

	public GameObject GetItem ()
	{
		float totalProbability = 0.0f;

		foreach (float p in probabilities) {
			totalProbability += p;
		}

		float r = Random.Range (0, totalProbability);
		for (var i = 0; i < items.Length; ++i) {
			if (r <= probabilities [i]) {
				return items [i];
			}

			r -= probabilities [i];
		}

		return items [items.Length - 1];
	}
}
