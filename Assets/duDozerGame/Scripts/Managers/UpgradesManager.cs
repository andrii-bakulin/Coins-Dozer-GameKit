//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UpgradesManager : ExtMonoBehaviour
{
	// @notice: if change order then require to check "1st items of groups" in UIUpgradesPopup.cs
	public static kUpgradeId[] kAllUpgradeIds = {
		kUpgradeId.ChipBasicCoin,
		kUpgradeId.ChipGoldenCoin,
		kUpgradeId.ChipGem,
		kUpgradeId.ChipXP,
		kUpgradeId.ChipGift,
		kUpgradeId.ChipPowerUp,

		kUpgradeId.PowerUpCashfall,
		kUpgradeId.PowerUpMegaDozer,
		kUpgradeId.PowerUpQuake,
		kUpgradeId.PowerUpShield,

		kUpgradeId.SpecImproveChipsAttrs,
		kUpgradeId.SpecImprovePrizeAttrs,
		kUpgradeId.SpecRegenSpeed,
		kUpgradeId.SpecRegenMax,
		kUpgradeId.SpecOfflineRegenSpeed,
	};

	public UpgradeRules GetUpgradeRules(kUpgradeId upgradeId)
	{
		var upgradeRules = new UpgradeRules();

		// @marker: PrizesList
		switch (upgradeId)
		{
			case kUpgradeId.ChipBasicCoin:
				upgradeRules.AddPrize(kObjectId.Prize0);
				upgradeRules.AddPrize(kObjectId.Prize1);
				upgradeRules.AddPrize(kObjectId.Prize2);
				break;

			case kUpgradeId.ChipGoldenCoin:
				upgradeRules.AddPrize(kObjectId.Prize1);
				upgradeRules.AddPrize(kObjectId.Prize3);
				upgradeRules.AddPrize(kObjectId.Prize5);
				break;

			case kUpgradeId.ChipGem:
				upgradeRules.AddPrize(kObjectId.Prize2);
				upgradeRules.AddPrize(kObjectId.Prize4);
				upgradeRules.AddPrize(kObjectId.Prize5);
				break;

			case kUpgradeId.ChipXP:
				upgradeRules.AddPrize(kObjectId.Prize0);
				upgradeRules.AddPrize(kObjectId.Prize4);
				upgradeRules.AddPrize(kObjectId.Prize6);
				break;

			case kUpgradeId.ChipGift:
				upgradeRules.AddPrize(kObjectId.Prize4);
				upgradeRules.AddPrize(kObjectId.Prize6);
				upgradeRules.AddPrize(kObjectId.Prize8);
				break;

			case kUpgradeId.ChipPowerUp:
				upgradeRules.AddPrize(kObjectId.Prize3);
				upgradeRules.AddPrize(kObjectId.Prize7);
				upgradeRules.AddPrize(kObjectId.Prize9);
				break;


			case kUpgradeId.PowerUpCashfall:
				upgradeRules.AddPrize(kObjectId.Prize0);
				upgradeRules.AddPrize(kObjectId.Prize3);
				upgradeRules.AddPrize(kObjectId.Prize9,2);
				break;

			case kUpgradeId.PowerUpMegaDozer:
				upgradeRules.AddPrize(kObjectId.Prize1);
				upgradeRules.AddPrize(kObjectId.Prize4);
				upgradeRules.AddPrize(kObjectId.Prize7,2);
				break;

			case kUpgradeId.PowerUpQuake:
				upgradeRules.AddPrize(kObjectId.Prize2);
				upgradeRules.AddPrize(kObjectId.Prize5);
				upgradeRules.AddPrize(kObjectId.Prize9,2);
				break;

			case kUpgradeId.PowerUpShield:
				upgradeRules.AddPrize(kObjectId.Prize3);
				upgradeRules.AddPrize(kObjectId.Prize6);
				upgradeRules.AddPrize(kObjectId.Prize8,2);
				break;


			case kUpgradeId.SpecImproveChipsAttrs:
				upgradeRules.AddPrize(kObjectId.Prize0);
				upgradeRules.AddPrize(kObjectId.Prize3);
				upgradeRules.AddPrize(kObjectId.Prize6);
				break;

			case kUpgradeId.SpecImprovePrizeAttrs:
				upgradeRules.AddPrize(kObjectId.Prize1);
				upgradeRules.AddPrize(kObjectId.Prize2);
				upgradeRules.AddPrize(kObjectId.Prize5);
				break;

			case kUpgradeId.SpecRegenSpeed:
				upgradeRules.AddPrize(kObjectId.Prize0);
				upgradeRules.AddPrize(kObjectId.Prize2,2);
				upgradeRules.AddPrize(kObjectId.Prize4,2);
				break;

			case kUpgradeId.SpecRegenMax:
				upgradeRules.AddPrize(kObjectId.Prize1,2);
				upgradeRules.AddPrize(kObjectId.Prize2);
				upgradeRules.AddPrize(kObjectId.Prize5,2);
				break;

			case kUpgradeId.SpecOfflineRegenSpeed:
				upgradeRules.AddPrize(kObjectId.Prize0);
				upgradeRules.AddPrize(kObjectId.Prize3);
				upgradeRules.AddPrize(kObjectId.Prize7,2);
				break;
		}

		return upgradeRules;
	}
}
