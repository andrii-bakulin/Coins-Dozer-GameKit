//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UICurrencyBar : ExtMonoBehaviour
{
	public UnityEngine.UI.Text labelGems;
	public UnityEngine.UI.Text labelPowerUps;

	void Start()
	{
		UpdateStates();
	}

	void UpdateStates()
	{
		if (labelGems != null)
			labelGems.text = playerManager.GemsAmount.ToString();

		if (labelPowerUps != null)
			labelPowerUps.text = playerManager.PowerUpsAmount.ToString();

		Invoke("UpdateStates", 0.2f);
	}
}
