//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIPowerUpsPopup : ExtMonoBehaviour
{
	public UnityEngine.UI.Text labelPowerUps;

	void Start()
	{
		UpdateStates();
	}

	void UpdateStates()
	{
		if (labelPowerUps != null)
			labelPowerUps.text = playerManager.PowerUpsAmount.ToString();

		Invoke("UpdateStates", 0.2f);
	}
}
