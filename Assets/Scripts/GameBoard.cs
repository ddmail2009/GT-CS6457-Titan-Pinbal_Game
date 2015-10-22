/**
 * Team Titan
 *
 * Meng-Hsin Tung
 **/

using UnityEngine;
using System.Collections;

public class GameBoard : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("GameBoard"), LayerMask.NameToLayer ("GameBoard"));
	}
}
