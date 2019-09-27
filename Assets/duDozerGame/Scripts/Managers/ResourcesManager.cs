//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class ResourcesManager : ExtMonoBehaviour
{
	[Header("References to Sprite-Icons: Chips")]
	public Sprite IconChipBasicCoin;
	public Sprite IconChipGoldenCoin;
	public Sprite IconChipGem;
	public Sprite IconChipXP;
	public Sprite IconChipGift;
	public Sprite IconChipPowerUp;

	[Header("References to Sprite-Icons: PowerUps")]
	public Sprite IconPowerUpCashfall;
	public Sprite IconPowerUpMegaDozer;
	public Sprite IconPowerUpQuake;
	public Sprite IconPowerUpShield;

	[Header("References to Sprite-Icons: Special Upgrades")]
	public Sprite IconSpecImproveChipsAttrs;
	public Sprite IconSpecImprovePrizeAttrs;
	public Sprite IconSpecRegenSpeed;
	public Sprite IconSpecRegenMax;
	public Sprite IconSpecOfflineRegenSpeed;

	[Header("References to Sprite-Icons: Prizes")]
	public Sprite IconPrize0;
	public Sprite IconPrize1;
	public Sprite IconPrize2;
	public Sprite IconPrize3;
	public Sprite IconPrize4;
	public Sprite IconPrize5;
	public Sprite IconPrize6;
	public Sprite IconPrize7;
	public Sprite IconPrize8;
	public Sprite IconPrize9;

	public Sprite GetChipSprite(kObjectId objectId)
	{
		// @marker: ChipsList
		switch (objectId)
		{
			case kObjectId.BonusChipGolden:	return IconChipGoldenCoin;
			case kObjectId.BonusChipGem: return IconChipGem;
			case kObjectId.BonusChipXP: return IconChipXP;
			case kObjectId.BonusChipGift: return IconChipGift;
			case kObjectId.BonusChipPowerUp: return IconChipPowerUp;

			case kObjectId.PowerChipCashfall: return IconPowerUpCashfall;
			case kObjectId.PowerChipMegaDozer: return IconPowerUpMegaDozer;
			case kObjectId.PowerChipQuake: return IconPowerUpQuake;
			case kObjectId.PowerChipShield: return IconPowerUpShield;
		}

		Debug.LogWarning("ResourcesManager: GetChipSprite: undefined objectId=" + objectId.ToString());
		return null;
	}

	public Sprite GetPrizeSprite(kObjectId objectId)
	{
		// @marker: PrizesList
		switch (objectId)
		{
			case kObjectId.Prize0: return IconPrize0;
			case kObjectId.Prize1: return IconPrize1;
			case kObjectId.Prize2: return IconPrize2;
			case kObjectId.Prize3: return IconPrize3;
			case kObjectId.Prize4: return IconPrize4;
			case kObjectId.Prize5: return IconPrize5;
			case kObjectId.Prize6: return IconPrize6;
			case kObjectId.Prize7: return IconPrize7;
			case kObjectId.Prize8: return IconPrize8;
			case kObjectId.Prize9: return IconPrize9;
		}

		Debug.LogWarning("ResourcesManager: GetPrizeSprite: undefined objectId=" + objectId.ToString());
		return null;
	}
}
