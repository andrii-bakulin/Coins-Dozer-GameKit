//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UILevelBar : ExtMonoBehaviour
{
	public UnityEngine.UI.Text levelLabel;
	public UIProgressBar progressBar;

	void Start()
	{
		UpdateStates();
	}

	void UpdateStates()
	{
		if (levelLabel != null)
			levelLabel.text = "LEVEL " + playerManager.Level.ToString();

		float expProgress = (float)playerManager.Experience / (float)playerManager.ExperienceLimit;

		if (progressBar != null)
			progressBar.SetProgress(expProgress);

		Invoke("UpdateStates", playerManager.Level == 0 ? 0.01f : 0.2f);
	}
}
