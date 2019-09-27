//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIProfilePopup : ExtMonoBehaviour
{
	public UnityEngine.UI.Text curLevelTitle;
	public UnityEngine.UI.Text nextLevelTitle;

	public UnityEngine.UI.Image giftPrizeIcon;
	public UnityEngine.UI.Image giftChipIcon;

	public UnityEngine.UI.Text curRegenOffline;
	public UnityEngine.UI.Text curRegenLimit;

	public UnityEngine.UI.Text nextRegenOffline;
	public UnityEngine.UI.Text nextRegenLimit;

	void Start ()
	{
		curLevelTitle.text = "Your Level " + playerManager.Level.ToString();
		nextLevelTitle.text = "On Level " + (playerManager.Level+1).ToString()+" You will get";

		giftPrizeIcon.sprite = resourcesManager.GetPrizeSprite(playerManager.LevelUpGiftPrizeObjectId);
		giftChipIcon.sprite = resourcesManager.GetChipSprite(playerManager.LevelUpGiftChipObjectId);

		curRegenOffline.text = HlpConvert.SecondsToFriendlyLongTime(balanceManager.OfflineRegenTimeForOneCoin);
		curRegenLimit.text = balanceManager.CoinsRegenMaxAmount.ToString();

		nextRegenOffline.text = HlpConvert.SecondsToFriendlyLongTime(balanceManager.OfflineRegenTimeForOneCoinNextLevel);
		nextRegenLimit.text = balanceManager.CoinsRegenMaxAmountNextLevel.ToString();
	}
}
