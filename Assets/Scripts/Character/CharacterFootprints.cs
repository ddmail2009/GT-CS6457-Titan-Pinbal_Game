/**
 * Titan
 *
 * Xiaoyu Chen
 */

using UnityEngine;
using System.Collections;

public class CharacterFootprints : MonoBehaviour
{
	public bool isEnabled = false;
	public GameObject leftFootprint;
	public GameObject rightFootprint;

	public Transform leftFootLocation;
	public Transform rightFootLocation;

	public int maxFootprintNumber = 10;

	public float footprintOffset = 0.02f;

	int layerMask, printedNumber;

	public void Awake ()
	{
		layerMask = 1 << LayerMask.NameToLayer ("GameBoard");
		printedNumber = 0;
	}

	public void LeftFootprint ()
	{
		if (isEnabled) {
			Footprint (leftFootprint, leftFootLocation);
		}
	}

	public void RightFootprint ()
	{
		if (isEnabled) {
			Footprint (rightFootprint, rightFootLocation);
		}
	}

	public void Footprint (GameObject footprint, Transform footLocation)
	{
		RaycastHit hit;

		if (Physics.Raycast (footLocation.position, footLocation.forward, out hit, Mathf.Infinity, layerMask)) {
			if (printedNumber < maxFootprintNumber) {
				Instantiate (footprint, hit.point + hit.normal * footprintOffset, Quaternion.LookRotation (hit.normal, footLocation.up));
				printedNumber++;
			}
		} else {
			printedNumber = 0;
		}
	}
}
