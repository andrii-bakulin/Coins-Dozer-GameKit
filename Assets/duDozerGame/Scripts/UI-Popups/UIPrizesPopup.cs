//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIPrizesPopup : ExtMonoBehaviour
{
	public UnityEngine.UI.Image activePrizeIcon;
	public UnityEngine.UI.Image changePrizeIcon1;
	public UnityEngine.UI.Image changePrizeIcon2;
	public UnityEngine.UI.Image changePrizeIcon3;

	void Start ()
	{
		UpdateStates();
	}

	void UpdateStates()
	{
		UpdateIcons();

		Invoke("UpdateStates", 0.5f);
	}

	void UpdateIcons()
	{
		activePrizeIcon.sprite = resourcesManager.GetPrizeSprite(playerManager.ActivePrizeObjectId);

		changePrizeIcon1.sprite = resourcesManager.GetPrizeSprite(playerManager.ChangePrizeObjectId1);
		changePrizeIcon2.sprite = resourcesManager.GetPrizeSprite(playerManager.ChangePrizeObjectId2);
		changePrizeIcon3.sprite = resourcesManager.GetPrizeSprite(playerManager.ChangePrizeObjectId3);
	}

	public void ChangePrize1()
	{
		ChangePrize(playerManager.ChangePrizeObjectId1, 1);
	}

	public void ChangePrize2()
	{
		ChangePrize(playerManager.ChangePrizeObjectId2, 2);
	}

	public void ChangePrize3()
	{
		ChangePrize(playerManager.ChangePrizeObjectId3, 3);
	}

	void ChangePrize(kObjectId newPrizeObjectId, int gemsPrice)
	{
		if (playerManager.DecGemsAmount(gemsPrice) == false)
			return;

		playerManager.ActivePrizeObjectId = newPrizeObjectId;

		UpdateIcons();
	}
}
