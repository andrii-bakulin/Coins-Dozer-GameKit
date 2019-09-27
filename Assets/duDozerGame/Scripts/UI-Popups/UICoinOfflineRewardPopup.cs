//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UICoinOfflineRewardPopup : ExtMonoBehaviour
{
	public static string showUserMessage = "";

	public UnityEngine.UI.Text messageLabel;
	public UnityEngine.UI.Text regenOfflineLabel;
	public UnityEngine.UI.Text regenOnlineLabel;
	public UnityEngine.UI.Text regenLimitLabel;

	void Start ()
	{
		messageLabel.text = popupManager.CoinOfflineReport_userMessage;

		regenOfflineLabel.text = HlpConvert.SecondsToFriendlyLongTime(balanceManager.OfflineRegenTimeForOneCoin);
		regenOnlineLabel.text = balanceManager.CoinsRegenTimeout.ToString() + " sec";
		regenLimitLabel.text = balanceManager.CoinsRegenMaxAmount.ToString();
	}
}
