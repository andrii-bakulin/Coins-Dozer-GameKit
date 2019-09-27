//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UILevelUpPopup : ExtMonoBehaviour
{
	public UnityEngine.UI.Text curLevelTitle;

	public UnityEngine.UI.Image giftPrizeIcon;
	public UnityEngine.UI.Image giftChipIcon;

	public UnityEngine.UI.Text curRegenOffline;
	public UnityEngine.UI.Text curRegenLimit;

	void Start()
	{
		curLevelTitle.text = "Level " + playerManager.Level.ToString();

		giftPrizeIcon.sprite = resourcesManager.GetPrizeSprite(playerManager.LevelUpGiftPrizeObjectId);
		giftChipIcon.sprite = resourcesManager.GetChipSprite(playerManager.LevelUpGiftChipObjectId);

		curRegenOffline.text = HlpConvert.SecondsToFriendlyLongTime(balanceManager.OfflineRegenTimeForOneCoin);
		curRegenLimit.text = balanceManager.CoinsRegenMaxAmount.ToString();
	}

	public void ClickOnCollect()
	{
		playerManager.DropPrizesOnLevelUp();

		GetComponent<UIPopup>().Close();
	}
}
