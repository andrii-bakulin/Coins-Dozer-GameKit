//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIUpgradeDetailsPopup : ExtMonoBehaviour
{
	const int kInstanteUpgradePrice = 10;

	//------------------------------------------------------------------------------------------------------------------

	public kUpgradeId upgradeId;

	public UnityEngine.UI.Text titleLabel;
	public UnityEngine.UI.Text descriptionLabel;

	public UnityEngine.UI.Text upgradeProgressLabel;
	public UIProgressBar upgradeProgressBar;

	public GameObject upgradeButton;

	public UnityEngine.UI.Image prizeSprite0;
	public UnityEngine.UI.Image prizeSprite1;
	public UnityEngine.UI.Image prizeSprite2;

	public UnityEngine.UI.Text prizeAmount0;
	public UnityEngine.UI.Text prizeAmount1;
	public UnityEngine.UI.Text prizeAmount2;

	public GameObject upgradeInfoHolder;
	public GameObject instanteButton;

	UpgradeRules upgradeRule;

	Color amountNotEnoughColor = (Color)new Color32(254, 121, 158, 255);
	Color amountEnoughColor = (Color)new Color32(30, 120, 60, 255);

	bool isAllowManualUpgrade;

	void Start()
	{
		upgradeId = popupManager.UpgradeDetails_upgradeId;

		titleLabel.text = Localize.GetUpgradeTitle(upgradeId);
		descriptionLabel.text = Localize.GetUpgradeDescription(upgradeId);

		upgradeRule = upgradesManager.GetUpgradeRules(upgradeId);

		prizeSprite0.sprite = resourcesManager.GetPrizeSprite(upgradeRule.prize0_objectId);
		prizeSprite1.sprite = resourcesManager.GetPrizeSprite(upgradeRule.prize1_objectId);
		prizeSprite2.sprite = resourcesManager.GetPrizeSprite(upgradeRule.prize2_objectId);

		UpgradeStates();
	}

	void UpgradeStates()
	{
		if (playerManager.GetUpgradeLevel(upgradeId) < 5)
		{
			int prize0_hasAmount = playerManager.GetPrizeAmount(upgradeRule.prize0_objectId);
			int prize1_hasAmount = playerManager.GetPrizeAmount(upgradeRule.prize1_objectId);
			int prize2_hasAmount = playerManager.GetPrizeAmount(upgradeRule.prize2_objectId);

			prizeAmount0.text = prize0_hasAmount.ToString() + " / " + upgradeRule.prize0_amount.ToString();
			prizeAmount1.text = prize1_hasAmount.ToString() + " / " + upgradeRule.prize1_amount.ToString();
			prizeAmount2.text = prize2_hasAmount.ToString() + " / " + upgradeRule.prize2_amount.ToString();

			// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

			isAllowManualUpgrade = true;

			if (prize0_hasAmount < upgradeRule.prize0_amount)
			{
				prizeAmount0.color = amountNotEnoughColor;
				isAllowManualUpgrade = false;
			}
			else
			{
				prizeAmount0.color = amountEnoughColor;
			}

			if (prize1_hasAmount < upgradeRule.prize1_amount)
			{
				prizeAmount1.color = amountNotEnoughColor;
				isAllowManualUpgrade = false;
			}
			else
			{
				prizeAmount1.color = amountEnoughColor;
			}

			if (prize2_hasAmount < upgradeRule.prize2_amount)
			{
				prizeAmount2.color = amountNotEnoughColor;
				isAllowManualUpgrade = false;
			}
			else
			{
				prizeAmount2.color = amountEnoughColor;
			}

			// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

			upgradeInfoHolder.SetActive(true);
			upgradeButton.SetActive(false);

			instanteButton.SetActive(true);
			instanteButton.GetComponent<AudioSource>().mute = playerManager.GemsAmount < kInstanteUpgradePrice;
		}
		else
		{
			isAllowManualUpgrade = false;

			upgradeInfoHolder.SetActive(false);
			instanteButton.SetActive(false);
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		float progressState = (float)playerManager.GetUpgradeLevel(upgradeId) / 5.0f;

		upgradeProgressLabel.text = ((int)(progressState * 100.0f)).ToString() + "%";
		upgradeProgressBar.SetProgress(progressState);
	}

	public void ClickOnUpgrade()
	{
		if (isAllowManualUpgrade == false)
			return;

		if (playerManager.IncUpgradeLevel(upgradeId) == false)
			return;

		playerManager.DecPrizeAmount(upgradeRule.prize0_objectId, upgradeRule.prize0_amount);
		playerManager.DecPrizeAmount(upgradeRule.prize1_objectId, upgradeRule.prize1_amount);
		playerManager.DecPrizeAmount(upgradeRule.prize2_objectId, upgradeRule.prize2_amount);

		UpgradeStates();
	}

	public void ClickOnInstanteUpgrade()
	{
		if (playerManager.GemsAmount < kInstanteUpgradePrice)
			return;

		if (playerManager.IncUpgradeLevel(upgradeId) == false)
			return;

		playerManager.DecGemsAmount(kInstanteUpgradePrice);

		UpgradeStates();
	}
}
