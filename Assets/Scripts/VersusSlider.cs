using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VersusSlider : MonoBehaviour
{
	public RectTransform redFillArea, blueFillArea, handle;

	float totalWidth, totalHeight;
	float initRedWidth;

	void Awake ()
	{
		initRedWidth = redFillArea.rect.width;
		totalWidth = redFillArea.rect.width + blueFillArea.rect.width;
		totalHeight = redFillArea.rect.height;
	}
	
	void Draw ()
	{
		float redRatio = (VersusManager.instance.currValue - VersusManager.instance.minValue) / (VersusManager.instance.maxValue - VersusManager.instance.minValue);

		redFillArea.sizeDelta = new Vector2 (totalWidth * redRatio, totalHeight);
		blueFillArea.sizeDelta = new Vector2 (totalWidth * (1 - redRatio), totalHeight);

		handle.anchoredPosition = new Vector2 ((totalWidth * redRatio - initRedWidth) * 0.5f, 0);
	}

	void Update ()
	{
		Draw ();
	}
}
