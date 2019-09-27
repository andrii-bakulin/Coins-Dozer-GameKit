//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIActivePowerUpButton : ExtMonoBehaviour
{
	public UIProgressBar progressBar;

	public GameObject iconCashfall;
	public GameObject iconMegaDozer;
	public GameObject iconQuake;
	public GameObject iconShield;

	void Update()
	{
		if (progressBar != null)
			progressBar.SetProgress(playerManager.PowerUpProgressComplete);

		if (iconCashfall != null)
			iconCashfall.SetActive(playerManager.PowerUpActiveType == kPowerUpType.Cashfall);

		if (iconMegaDozer != null)
			iconMegaDozer.SetActive(playerManager.PowerUpActiveType == kPowerUpType.MegaDozer);

		if (iconQuake != null)
			iconQuake.SetActive(playerManager.PowerUpActiveType == kPowerUpType.Quake);

		if (iconShield != null)
			iconShield.SetActive(playerManager.PowerUpActiveType == kPowerUpType.Shield);
	}
}
