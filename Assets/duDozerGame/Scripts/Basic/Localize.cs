//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class Localize
{
	public static string GetUpgradeTitle(kUpgradeId upgradeId)
	{
		switch (upgradeId)
		{
			case kUpgradeId.ChipBasicCoin:
				return "Coin";
			case kUpgradeId.ChipGoldenCoin:
				return "Golden Coin";
			case kUpgradeId.ChipGem:
				return "Gem Chip";
			case kUpgradeId.ChipXP:
				return "XP Chip";
			case kUpgradeId.ChipGift:
				return "Gift Chip";
			case kUpgradeId.ChipPowerUp:
				return "Power Chip";

			case kUpgradeId.PowerUpCashfall:
				return "Cashfall";
			case kUpgradeId.PowerUpMegaDozer:
				return "Mega Dozer";
			case kUpgradeId.PowerUpQuake:
				return "Quake";
			case kUpgradeId.PowerUpShield:
				return "Shield";

			case kUpgradeId.SpecImproveChipsAttrs:
				return "Improve Chips";
			case kUpgradeId.SpecImprovePrizeAttrs:
				return "Improve Prizes";
			case kUpgradeId.SpecRegenSpeed:
				return "Regen Speed";
			case kUpgradeId.SpecRegenMax:
				return "Regen Max";
			case kUpgradeId.SpecOfflineRegenSpeed:
				return "Off.Regen Speed";
		}
		return "";
	}

	public static string GetUpgradeDescription(kUpgradeId upgradeId)
	{
		switch (upgradeId)
		{
			case kUpgradeId.ChipBasicCoin:
				return
					"Upgrading this coin increases" + "\n" +
					"the chance drop Chip" + "\n" +
					"«2 coins» or «5 coins»" + "\n" +
					"instead of regular coin!";
			case kUpgradeId.ChipGoldenCoin:
				return
					"Upgrading this Chip increases" + "\n" +
					"chance drop 2 Chips at once" + "\n" +
					"and amount of coins you will win!";
			case kUpgradeId.ChipGem:
				return
					"Upgrading this Chip increases" + "\n" +
					"chance drop 2 Chips at once" + "\n" +
					"and chance get 2 gems reward" + "\n" +
					"per each Chip";
			case kUpgradeId.ChipXP:
				return
					"Upgrading this Chip increases" + "\n" +
					"chance drop 2 Chips at once" + "\n" +
					"and amount of XP you will get";
			case kUpgradeId.ChipGift:
				return
					"Upgrading this Chip increases" + "\n" +
					"the rarity and counts of" + "\n" +
					"the prizes that spawns!";
			case kUpgradeId.ChipPowerUp:
				return
					"Upgrading this Chip increases" + "\n" +
					"chance drop 2 Chips at once" + "\n" +
					"and chance win 2 «Power Up» rewards!";

			// - - - - - - - - - - - - - - - - - - - - - - - - - - - -

			case kUpgradeId.PowerUpCashfall:
				return
					"Upgrading this power increases" + "\n" +
					"the number coins that" + "\n" +
					"will be drop on the table!";
			case kUpgradeId.PowerUpMegaDozer:
				return
					"Upgrading this power increases" + "\n" +
					"distance that the dozer" + "\n" +
					"is pushes out!";
			case kUpgradeId.PowerUpQuake:
				return
					"Upgrading this power increases" + "\n" +
					"the duration and force" + "\n" +
					"of the quake!";
			case kUpgradeId.PowerUpShield:
				return
					"Upgrading this power increases" + "\n" +
					"the time that shields" + "\n" +
					"stays on the table!";

			// - - - - - - - - - - - - - - - - - - - - - - - - - - - -

			case kUpgradeId.SpecImproveChipsAttrs:
				return
					"Upgrading this ability increases" + "\n" +
					"chance spawn random Chip(s) on table" + "\n" +
					"and max allow Chips on the table!";

			case kUpgradeId.SpecImprovePrizeAttrs:
				return
					"Upgrading this ability increases" + "\n" +
					"chance spawn random prize(s) on table" + "\n" +
					"and max allow prizes on the table!";

			case kUpgradeId.SpecRegenSpeed:
				return
					"Upgrading this ability decreases" + "\n" +
					"the time of regeneration of coins," + "\n" +
					"while you are in the game!";

			case kUpgradeId.SpecRegenMax:
				return
					"Upgrading this ability increases" + "\n" +
					"the total number of coins" + "\n" +
					"you can regenerate!";

			case kUpgradeId.SpecOfflineRegenSpeed:
				return
					"Upgrading this ability decreases" + "\n" +
					"the time of regeneration of coins," + "\n" +
					"while you are away from the game!";
		}
		return "";
	}
}
