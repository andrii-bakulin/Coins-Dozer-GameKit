//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIProgressBar : ExtMonoBehaviour
{
	public RectTransform scrollbarColor;
	public RectTransform scrollbarLight;

	Vector3 mainPosition;
	float minWidth;
	float maxWidth;

	int showedProgress = -1;
	int toShowProgress = 0;

	void Start()
	{
		if (scrollbarColor == null)
		{
			Debug.LogWarning("UIProgressBar: Require to define scrollbarColor object");
			return;
		}

		if (scrollbarLight == null)
		{
			Debug.LogWarning("UIProgressBar: Require to define scrollbarColor object");
			return;
		}

		mainPosition = scrollbarColor.localPosition;

		minWidth = 22.0f;
		maxWidth = scrollbarColor.sizeDelta.x;
	}

	void Update()
	{
		if (showedProgress == toShowProgress)
			return; // no need to update!

		if (toShowProgress == 0)
		{
			scrollbarColor.gameObject.SetActive(false);
			scrollbarLight.gameObject.SetActive(false);
		}
		else
		{
			float newWidth = (maxWidth - minWidth) * (float)(toShowProgress / 100f) + minWidth;

			scrollbarColor.gameObject.SetActive(true);
			scrollbarColor.localPosition = mainPosition + new Vector3(-(maxWidth - newWidth) / 2.0f, 0.0f, 0.0f);
			scrollbarColor.sizeDelta = new Vector2(newWidth, scrollbarColor.sizeDelta.y);

			scrollbarLight.gameObject.SetActive(true);
			scrollbarLight.localPosition = scrollbarColor.localPosition;
			scrollbarLight.sizeDelta = scrollbarColor.sizeDelta;
		}

		showedProgress = toShowProgress;
	}

	/**
	 * float progress in range 0.0 to 1.0
	 */
	public void SetProgress(float progress)
	{
		toShowProgress = Mathf.RoundToInt(progress * 100f);
	}
}
