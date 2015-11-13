using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
	public void LoadLevel (string levelName)
	{
		Application.LoadLevel (levelName);
	}
}
