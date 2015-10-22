/**
 * Team Titan
 *
 * Meng-Hsin Tung
 **/

using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
	void Update ()
	{
		if (Input.GetKey ("1")) {
			Application.LoadLevel ("Level_House");
		} else if (Input.GetKey ("2")) {
			Application.LoadLevel ("Level_BlossomPark");
		} else if (Input.GetKey ("3")) {
			Application.LoadLevel ("Level_Icy");
		} else if (Input.GetKey ("4")) {
			Application.LoadLevel ("Level_Pinball");
		} else if (Input.GetKey ("5")) {
			Application.LoadLevel ("Level_EndOfWorld");
		}
	}
}
