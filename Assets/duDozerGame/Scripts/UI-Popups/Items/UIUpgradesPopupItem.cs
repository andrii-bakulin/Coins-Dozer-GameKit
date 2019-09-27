//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIUpgradesPopupItem : ExtMonoBehaviour
{
	public kUpgradeId upgradeId = kUpgradeId.Undefined;

	public UnityEngine.UI.Image backgroundImage;
	public UnityEngine.UI.Image iconImage;

	public UnityEngine.UI.Text titleLabel;
	public UnityEngine.UI.Text upgradeLabel;

	public UIProgressBar upgradeProgressBar;

	void Start()
	{
		titleLabel.text = Localize.GetUpgradeTitle(upgradeId);

		switch (upgradeId)
		{
			case kUpgradeId.ChipBasicCoin: iconImage.sprite = resourcesManager.IconChipBasicCoin; break;
			case kUpgradeId.ChipGoldenCoin: iconImage.sprite = resourcesManager.IconChipGoldenCoin; break;
			case kUpgradeId.ChipGem: iconImage.sprite = resourcesManager.IconChipGem; break;
			case kUpgradeId.ChipXP: iconImage.sprite = resourcesManager.IconChipXP; break;
			case kUpgradeId.ChipGift: iconImage.sprite = resourcesManager.IconChipGift; break;
			case kUpgradeId.ChipPowerUp: iconImage.sprite = resourcesManager.IconChipPowerUp; break;

			case kUpgradeId.PowerUpCashfall: iconImage.sprite = resourcesManager.IconPowerUpCashfall; break;
			case kUpgradeId.PowerUpMegaDozer: iconImage.sprite = resourcesManager.IconPowerUpMegaDozer; break;
			case kUpgradeId.PowerUpQuake: iconImage.sprite = resourcesManager.IconPowerUpQuake; break;
			case kUpgradeId.PowerUpShield: iconImage.sprite = resourcesManager.IconPowerUpShield; break;

			case kUpgradeId.SpecImproveChipsAttrs: iconImage.sprite = resourcesManager.IconSpecImproveChipsAttrs; break;
			case kUpgradeId.SpecImprovePrizeAttrs: iconImage.sprite = resourcesManager.IconSpecImprovePrizeAttrs; break;
			case kUpgradeId.SpecRegenSpeed: iconImage.sprite = resourcesManager.IconSpecRegenSpeed; break;
			case kUpgradeId.SpecRegenMax: iconImage.sprite = resourcesManager.IconSpecRegenMax; break;
			case kUpgradeId.SpecOfflineRegenSpeed: iconImage.sprite = resourcesManager.IconSpecOfflineRegenSpeed; break;
		}

		UpdateStates();
	}

	void UpdateStates()
	{
		if (upgradeId == kUpgradeId.Undefined)
			return;

		float progressState = (float)playerManager.GetUpgradeLevel(upgradeId) / 5.0f;

		upgradeLabel.text = ((int)(progressState * 100.0f)).ToString() + "%";
		upgradeProgressBar.SetProgress(progressState);

		Invoke("UpdateStates", 1.0f);
	}

	public void ShowDetails()
	{
		popupManager.OpenPopup_UpgradeDetails(upgradeId);
	}
}
