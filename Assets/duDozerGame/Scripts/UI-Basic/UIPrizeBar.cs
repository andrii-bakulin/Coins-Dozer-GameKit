//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIPrizeBar : ExtMonoBehaviour
{
	public UnityEngine.UI.Image activePrizeIcon;
	public UIProgressBar progressBar;

	void Start()
	{
		UpdateStates();
	}

	void UpdateStates()
	{
		if (playerManager.ActivePrizeObjectId == kObjectId.Undefined)
		{
			if (activePrizeIcon != null)
				activePrizeIcon.enabled = false;

			if (progressBar != null)
				progressBar.SetProgress(0.0f);

			Invoke("UpdateStates", 0.02f);
		}
		else
		{
			if (activePrizeIcon != null)
			{
				activePrizeIcon.enabled = true;
				activePrizeIcon.sprite = resourcesManager.GetPrizeSprite(playerManager.ActivePrizeObjectId);
			}

			if (progressBar != null)
				progressBar.SetProgress((float)playerManager.ActivePrizeProgress / (float)playerManager.ActivePrizeProgressLimit);

			Invoke("UpdateStates", 0.2f);
		}
	}
}
