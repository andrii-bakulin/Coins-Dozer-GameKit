//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UICoinsBar : ExtMonoBehaviour
{
	public UnityEngine.UI.Text timerLabel;
	public UnityEngine.UI.Text coinsLabel;
	public UIProgressBar progressBar;

	void Start()
	{
		UpdateStates();
	}

	void UpdateStates()
	{
		bool isShowMaxLabel = false;

		// if TOP second showing
		if (playerManager.CoinsRegenSecondsLeft >= (balanceManager.CoinsRegenTimeout - 1.0f))
		{
			if (playerManager.CoinsAmount >= balanceManager.CoinsRegenMaxAmount)
			{
				isShowMaxLabel = true;
			}
		}

		if (isShowMaxLabel)
		{
			if (timerLabel != null)
				timerLabel.text = "REGEN.";

			if (coinsLabel != null)
				coinsLabel.text = "MAX";
		}
		else
		{
			int secLeft = Mathf.CeilToInt(playerManager.CoinsRegenSecondsLeft);

			if (timerLabel != null)
				timerLabel.text = HlpConvert.SecondsToFriendlyTime(secLeft);

			if (coinsLabel != null)
				coinsLabel.text = playerManager.CoinsAmount.ToString();
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		if (progressBar != null)
			progressBar.SetProgress(1.0f - playerManager.CoinsRegenSecondsLeft / balanceManager.CoinsRegenTimeout);

		Invoke("UpdateStates", playerManager.Level == 0 ? 0.01f : 0.2f);
	}
}
