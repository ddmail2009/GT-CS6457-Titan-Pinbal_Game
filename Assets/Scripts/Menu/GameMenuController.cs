using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMenuController : MonoBehaviour
{
	bool isOpened = false;
	GameObject canvas;
	float savedTimeScale;

	ThirdPersonCameraControllerBeta playerCamera;

	public void LoadLevel (string levelName)
	{
		Application.LoadLevel (levelName);
	}

	public void ExitGame ()
	{
		Application.Quit ();
	}

	void Awake ()
	{
		playerCamera = GameObject.FindWithTag ("ThirdPersonCamera").GetComponent<ThirdPersonCameraControllerBeta> ();

		canvas = transform.GetChild (0).gameObject;
		savedTimeScale = Time.timeScale;
	}

	void Update ()
	{
		if (Input.GetButtonUp ("Esc")) {
			isOpened = !isOpened;

			canvas.gameObject.SetActive (isOpened);
			Cursor.visible = isOpened;
			playerCamera.enabled = !isOpened;

			if (isOpened) {
				savedTimeScale = Time.timeScale;
				Time.timeScale = 0;
			} else {
				Time.timeScale = savedTimeScale;
			}
		}
	}
}
