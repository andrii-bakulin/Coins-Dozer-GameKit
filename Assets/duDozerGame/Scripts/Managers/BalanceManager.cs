//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;
using System.Collections.Generic;

public class BalanceManager : ExtMonoBehaviour
{
	bool isBalanceStatesInited = false;

	float autoBonus_chanceSpawnChip;
	float autoBonus_chanceSpawnPrize;

	int maxCountChipsOnBoard;
	int maxCountPrizesOnBoard;

	float basicCoin_chanceSpawnCoin2x;
	float basicCoin_chanceSpawnCoin5x;

	int goldenCoin_rewardCoinsAmount;
	float goldenCoin_chanceSpawn2x;

	float gemChip_chanceDoubleReward;
	float gemChip_chanceSpawn2x;

	float xpChip_experienceBonus;
	float xpChip_chanceSpawn2x;

	float powerUpChip_chanceDoubleReward;
	float powerUpChip_chanceSpawn2x;

	float giftChip_chanceSpawn2x;
	float giftChip_chanceSpawnPrizes2x;
	float giftChip_chanceSpawnPrizes3x;
	float giftChip_chanceSpawnChips1x;
	float giftChip_chanceSpawnChips2x;

	int powerUpCashfallMaxCoinsAmount;
	float powerUpMegaDozerMaxLength;
	float powerUpQuakeMaxForceScale;
	float powerUpShieldMaxDuration;

	float coinsRegenTimeout;
	int coinsRegenCapacityBonus;
	int coinsRegenOfflineSecondsBoosts;

	List<kObjectId> chipsObjectIds;
	List<kObjectId> chipsPowerUpsObjectIds;
	List<kObjectId> prizesObjectIds;

	void InitBalanceStates()
	{
		if (isBalanceStatesInited)
			return;

		switch (playerManager.GetUpgradeLevel(kUpgradeId.ChipBasicCoin))
		{
			default:
			case 0: basicCoin_chanceSpawnCoin2x = 0.05f; basicCoin_chanceSpawnCoin5x = 0.020f; break;
			case 1: basicCoin_chanceSpawnCoin2x = 0.06f; basicCoin_chanceSpawnCoin5x = 0.025f; break;
			case 2: basicCoin_chanceSpawnCoin2x = 0.07f; basicCoin_chanceSpawnCoin5x = 0.030f; break;
			case 3: basicCoin_chanceSpawnCoin2x = 0.08f; basicCoin_chanceSpawnCoin5x = 0.035f; break;
			case 4: basicCoin_chanceSpawnCoin2x = 0.09f; basicCoin_chanceSpawnCoin5x = 0.040f; break;
			case 5: basicCoin_chanceSpawnCoin2x = 0.10f; basicCoin_chanceSpawnCoin5x = 0.045f; break;
		}

		switch (playerManager.GetUpgradeLevel(kUpgradeId.ChipGoldenCoin))
		{
			default:
			case 0: goldenCoin_rewardCoinsAmount = 20; goldenCoin_chanceSpawn2x = 0.05f; break;
			case 1: goldenCoin_rewardCoinsAmount = 22; goldenCoin_chanceSpawn2x = 0.06f; break;
			case 2: goldenCoin_rewardCoinsAmount = 24; goldenCoin_chanceSpawn2x = 0.07f; break;
			case 3: goldenCoin_rewardCoinsAmount = 26; goldenCoin_chanceSpawn2x = 0.08f; break;
			case 4: goldenCoin_rewardCoinsAmount = 28; goldenCoin_chanceSpawn2x = 0.09f; break;
			case 5: goldenCoin_rewardCoinsAmount = 30; goldenCoin_chanceSpawn2x = 0.10f; break;
		}

		switch (playerManager.GetUpgradeLevel(kUpgradeId.ChipGem))
		{
			default:
			case 0: gemChip_chanceDoubleReward = 0.00f; gemChip_chanceSpawn2x = 0.05f; break;
			case 1: gemChip_chanceDoubleReward = 0.02f; gemChip_chanceSpawn2x = 0.06f; break;
			case 2: gemChip_chanceDoubleReward = 0.04f; gemChip_chanceSpawn2x = 0.07f; break;
			case 3: gemChip_chanceDoubleReward = 0.06f; gemChip_chanceSpawn2x = 0.08f; break;
			case 4: gemChip_chanceDoubleReward = 0.08f; gemChip_chanceSpawn2x = 0.09f; break;
			case 5: gemChip_chanceDoubleReward = 0.10f; gemChip_chanceSpawn2x = 0.10f; break;
		}

		switch (playerManager.GetUpgradeLevel(kUpgradeId.ChipXP))
		{
			default:
			case 0: xpChip_experienceBonus = 0.15f; xpChip_chanceSpawn2x = 0.05f; break;
			case 1: xpChip_experienceBonus = 0.18f; xpChip_chanceSpawn2x = 0.06f; break;
			case 2: xpChip_experienceBonus = 0.21f; xpChip_chanceSpawn2x = 0.07f; break;
			case 3: xpChip_experienceBonus = 0.24f; xpChip_chanceSpawn2x = 0.08f; break;
			case 4: xpChip_experienceBonus = 0.27f; xpChip_chanceSpawn2x = 0.09f; break;
			case 5: xpChip_experienceBonus = 0.30f; xpChip_chanceSpawn2x = 0.10f; break;
		}

		switch (playerManager.GetUpgradeLevel(kUpgradeId.ChipPowerUp))
		{
			default:
			case 0: powerUpChip_chanceDoubleReward = 0.00f; powerUpChip_chanceSpawn2x = 0.05f; break;
			case 1: powerUpChip_chanceDoubleReward = 0.02f; powerUpChip_chanceSpawn2x = 0.06f; break;
			case 2: powerUpChip_chanceDoubleReward = 0.04f; powerUpChip_chanceSpawn2x = 0.07f; break;
			case 3: powerUpChip_chanceDoubleReward = 0.06f; powerUpChip_chanceSpawn2x = 0.08f; break;
			case 4: powerUpChip_chanceDoubleReward = 0.08f; powerUpChip_chanceSpawn2x = 0.09f; break;
			case 5: powerUpChip_chanceDoubleReward = 0.10f; powerUpChip_chanceSpawn2x = 0.10f; break;
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		switch (playerManager.GetUpgradeLevel(kUpgradeId.ChipGift))
		{
			default:
			case 0: giftChip_chanceSpawn2x = 0.05f; giftChip_chanceSpawnPrizes2x = 0.10f; giftChip_chanceSpawnPrizes3x = 0.00f; giftChip_chanceSpawnChips1x = 0.05f; giftChip_chanceSpawnChips2x = 0.00f; break;
			case 1: giftChip_chanceSpawn2x = 0.06f; giftChip_chanceSpawnPrizes2x = 0.12f; giftChip_chanceSpawnPrizes3x = 0.02f; giftChip_chanceSpawnChips1x = 0.06f; giftChip_chanceSpawnChips2x = 0.01f; break;
			case 2: giftChip_chanceSpawn2x = 0.07f; giftChip_chanceSpawnPrizes2x = 0.14f; giftChip_chanceSpawnPrizes3x = 0.04f; giftChip_chanceSpawnChips1x = 0.07f; giftChip_chanceSpawnChips2x = 0.02f; break;
			case 3: giftChip_chanceSpawn2x = 0.08f; giftChip_chanceSpawnPrizes2x = 0.16f; giftChip_chanceSpawnPrizes3x = 0.06f; giftChip_chanceSpawnChips1x = 0.08f; giftChip_chanceSpawnChips2x = 0.03f; break;
			case 4: giftChip_chanceSpawn2x = 0.09f; giftChip_chanceSpawnPrizes2x = 0.18f; giftChip_chanceSpawnPrizes3x = 0.08f; giftChip_chanceSpawnChips1x = 0.09f; giftChip_chanceSpawnChips2x = 0.04f; break;
			case 5: giftChip_chanceSpawn2x = 0.10f; giftChip_chanceSpawnPrizes2x = 0.20f; giftChip_chanceSpawnPrizes3x = 0.10f; giftChip_chanceSpawnChips1x = 0.10f; giftChip_chanceSpawnChips2x = 0.05f; break;
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		chipsObjectIds = new List<kObjectId>();
		chipsObjectIds.Add(kObjectId.BonusChipGolden);
		chipsObjectIds.Add(kObjectId.BonusChipGem);
		chipsObjectIds.Add(kObjectId.BonusChipXP);
		chipsObjectIds.Add(kObjectId.BonusChipGift);
		chipsObjectIds.Add(kObjectId.BonusChipPowerUp);

		chipsPowerUpsObjectIds = new List<kObjectId>();
		chipsPowerUpsObjectIds.Add(kObjectId.BonusChipPowerUp);
		chipsPowerUpsObjectIds.Add(kObjectId.PowerChipCashfall);
		chipsPowerUpsObjectIds.Add(kObjectId.PowerChipMegaDozer);
		chipsPowerUpsObjectIds.Add(kObjectId.PowerChipQuake);
		chipsPowerUpsObjectIds.Add(kObjectId.PowerChipShield);

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		float prize0rare, prize9rare;

		switch (playerManager.GetUpgradeLevel(kUpgradeId.ChipGift))
		{
			default:
			case 0: prize0rare = 5; prize9rare = 0; break;
			case 1: prize0rare = 5; prize9rare = 1; break;
			case 2: prize0rare = 5; prize9rare = 2; break;
			case 3: prize0rare = 5; prize9rare = 3; break;
			case 4: prize0rare = 5; prize9rare = 4; break;
			case 5: prize0rare = 5; prize9rare = 5; break;
		}

		prizesObjectIds = new List<kObjectId>();

		float deltaRare = (prize9rare - prize0rare) / ((float)ObjectsManager.kAllPrizesList.Length - 1.0f);

		int index = 0;
		foreach (var prizeId in ObjectsManager.kAllPrizesList)
		{
			int prizeRare = Mathf.RoundToInt(prize0rare + (deltaRare * (float)index));

			for (int r = 0; r < prizeRare; r++)
			{
				prizesObjectIds.Add(prizeId);
			}

			index++;
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		switch (playerManager.GetUpgradeLevel(kUpgradeId.PowerUpCashfall))
		{
			default:
			case 0: powerUpCashfallMaxCoinsAmount = 10; break;
			case 1: powerUpCashfallMaxCoinsAmount = 12; break;
			case 2: powerUpCashfallMaxCoinsAmount = 14; break;
			case 3: powerUpCashfallMaxCoinsAmount = 16; break;
			case 4: powerUpCashfallMaxCoinsAmount = 18; break;
			case 5: powerUpCashfallMaxCoinsAmount = 20; break;
		}

		switch (playerManager.GetUpgradeLevel(kUpgradeId.PowerUpMegaDozer))
		{
			default:
			case 0: powerUpMegaDozerMaxLength = 2.0f; break;
			case 1: powerUpMegaDozerMaxLength = 2.4f; break;
			case 2: powerUpMegaDozerMaxLength = 2.8f; break;
			case 3: powerUpMegaDozerMaxLength = 3.2f; break;
			case 4: powerUpMegaDozerMaxLength = 3.6f; break;
			case 5: powerUpMegaDozerMaxLength = 4.0f; break;
		}

		switch (playerManager.GetUpgradeLevel(kUpgradeId.PowerUpQuake))
		{
			default:
			case 0: powerUpQuakeMaxForceScale = 1.00f; break;
			case 1: powerUpQuakeMaxForceScale = 1.10f; break;
			case 2: powerUpQuakeMaxForceScale = 1.20f; break;
			case 3: powerUpQuakeMaxForceScale = 1.30f; break;
			case 4: powerUpQuakeMaxForceScale = 1.40f; break;
			case 5: powerUpQuakeMaxForceScale = 1.50f; break;
		}

		switch (playerManager.GetUpgradeLevel(kUpgradeId.PowerUpShield))
		{
			default:
			case 0: powerUpShieldMaxDuration = 10.0f; break;
			case 1: powerUpShieldMaxDuration = 12.0f; break;
			case 2: powerUpShieldMaxDuration = 14.0f; break;
			case 3: powerUpShieldMaxDuration = 16.0f; break;
			case 4: powerUpShieldMaxDuration = 18.0f; break;
			case 5: powerUpShieldMaxDuration = 20.0f; break;
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		switch (playerManager.GetUpgradeLevel(kUpgradeId.SpecImproveChipsAttrs))
		{
			default:
			case 0: autoBonus_chanceSpawnChip = 0.020f; maxCountChipsOnBoard = 5; break;
			case 1: autoBonus_chanceSpawnChip = 0.025f; maxCountChipsOnBoard = 6; break;
			case 2: autoBonus_chanceSpawnChip = 0.030f; maxCountChipsOnBoard = 7; break;
			case 3: autoBonus_chanceSpawnChip = 0.033f; maxCountChipsOnBoard = 8; break;
			case 4: autoBonus_chanceSpawnChip = 0.035f; maxCountChipsOnBoard = 9; break;
			case 5: autoBonus_chanceSpawnChip = 0.040f; maxCountChipsOnBoard = 10; break;
		}

		switch (playerManager.GetUpgradeLevel(kUpgradeId.SpecImprovePrizeAttrs))
		{
			default:
			case 0: autoBonus_chanceSpawnPrize = 0.020f; maxCountPrizesOnBoard = 2; break;
			case 1: autoBonus_chanceSpawnPrize = 0.025f; maxCountPrizesOnBoard = 3; break;
			case 2: autoBonus_chanceSpawnPrize = 0.030f; maxCountPrizesOnBoard = 4; break;
			case 3: autoBonus_chanceSpawnPrize = 0.033f; maxCountPrizesOnBoard = 5; break;
			case 4: autoBonus_chanceSpawnPrize = 0.035f; maxCountPrizesOnBoard = 6; break;
			case 5: autoBonus_chanceSpawnPrize = 0.040f; maxCountPrizesOnBoard = 7; break;
		}

		switch (playerManager.GetUpgradeLevel(kUpgradeId.SpecRegenSpeed))
		{
			default:
			case 0: coinsRegenTimeout = 30f; break;
			case 1: coinsRegenTimeout = 28f; break;
			case 2: coinsRegenTimeout = 26f; break;
			case 3: coinsRegenTimeout = 24f; break;
			case 4: coinsRegenTimeout = 22f; break;
			case 5: coinsRegenTimeout = 20f; break;
		}

		switch (playerManager.GetUpgradeLevel(kUpgradeId.SpecRegenMax))
		{
			default:
			case 0: coinsRegenCapacityBonus = 0; break;
			case 1: coinsRegenCapacityBonus = 25; break;
			case 2: coinsRegenCapacityBonus = 50; break;
			case 3: coinsRegenCapacityBonus = 75; break;
			case 4: coinsRegenCapacityBonus = 100; break;
			case 5: coinsRegenCapacityBonus = 125; break;
		}

		switch (playerManager.GetUpgradeLevel(kUpgradeId.SpecOfflineRegenSpeed))
		{
			default:
			case 0: coinsRegenOfflineSecondsBoosts = 0; break;
			case 1: coinsRegenOfflineSecondsBoosts = 12; break;
			case 2: coinsRegenOfflineSecondsBoosts = 24; break;
			case 3: coinsRegenOfflineSecondsBoosts = 36; break;
			case 4: coinsRegenOfflineSecondsBoosts = 48; break;
			case 5: coinsRegenOfflineSecondsBoosts = 60; break;
		}

		isBalanceStatesInited = true;
	}

	public void ResetBalanceStates()
	{
		isBalanceStatesInited = false;
	}

	//------------------------------------------------------------------------------------------------------------------

	float Rand()
	{
		return Random.Range(0.0f, 1.0f);
	}

	bool IsApply(float chance)
	{
		return Rand() <= chance;
	}

	//------------------------------------------------------------------------------------------------------------------

	public float CoinsRegenTimeout
	{
		get { InitBalanceStates(); return coinsRegenTimeout; }
	}

	public int CoinsRegenMaxAmount
	{
		get { InitBalanceStates(); return GetCoinsRegenMaxAmount(playerManager.Level); }
	}

	public int OfflineRegenTimeForOneCoin
	{
		get { InitBalanceStates(); return GetOfflineRegenTimeForOneCoin(playerManager.Level); }
	}

	public int MaxCountChipsOnBoard
	{
		get { InitBalanceStates(); return maxCountChipsOnBoard; }
	}

	public int MaxCountPrizesOnBoard
	{
		get { InitBalanceStates(); return maxCountPrizesOnBoard; }
	}

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	public int CoinsRegenMaxAmountNextLevel
	{
		get { InitBalanceStates(); return GetCoinsRegenMaxAmount(playerManager.Level + 1); }
	}

	public int OfflineRegenTimeForOneCoinNextLevel
	{
		get { InitBalanceStates(); return GetOfflineRegenTimeForOneCoin(playerManager.Level + 1); }
	}

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	int GetCoinsRegenMaxAmount(int level)
	{
		return 40 + (level - 1) * 2 + coinsRegenCapacityBonus;
	}

	int GetOfflineRegenTimeForOneCoin(int level)
	{
		// On upgrade level 0: 6:30 .. 4:00 (dec step 2 sec/level)
		// On upgrade level 5: 5:30 .. 3:00 (dec step 2 sec/level)
		return (int)Mathf.Max(6.5f * 60 - (level - 1) * 2, 4.0f * 60) - coinsRegenOfflineSecondsBoosts;
	}

	//------------------------------------------------------------------------------------------------------------------

	public kObjectId CoinObjectId
	{
		get { InitBalanceStates(); return IsApply(basicCoin_chanceSpawnCoin5x) ? kObjectId.Coin5x : (IsApply(basicCoin_chanceSpawnCoin2x) ? kObjectId.Coin2x : kObjectId.Coin1x); }
	}

	public int CoinsRewardAmountForGoldenChip
	{
		get { InitBalanceStates(); return goldenCoin_rewardCoinsAmount; }
	}

	public int GemsRewardAmountForGemChip
	{
		get { InitBalanceStates(); return IsApply(gemChip_chanceDoubleReward) ? 2 : 1; }
	}

	public int ExperienceAmountForXPChip
	{
		get { InitBalanceStates(); return Mathf.RoundToInt(playerManager.ExperienceLimit * xpChip_experienceBonus); }
	}

	public int PowerUpRewardAmountForPowerUpChip
	{
		get { InitBalanceStates(); return IsApply(powerUpChip_chanceDoubleReward) ? 2 : 1; }
	}

	//------------------------------------------------------------------------------------------------------------------
	// Power Ups

	public int GetPowerUpCashfallCoinsAmount(kPowerUpStrength strength)
	{
		InitBalanceStates();
		switch (strength)
		{
			default:
			case kPowerUpStrength.Min: return Mathf.RoundToInt((float)powerUpCashfallMaxCoinsAmount * 0.50f);
			case kPowerUpStrength.Mid: return Mathf.RoundToInt((float)powerUpCashfallMaxCoinsAmount * 0.75f);
			case kPowerUpStrength.Max: return Mathf.RoundToInt((float)powerUpCashfallMaxCoinsAmount * 1.00f);
		}
	}

	public float GetPowerUpMegaDozerBonusLength(kPowerUpStrength strength)
	{
		InitBalanceStates();
		switch (strength)
		{
			default:
			case kPowerUpStrength.Min: return powerUpMegaDozerMaxLength * 0.50f;
			case kPowerUpStrength.Mid: return powerUpMegaDozerMaxLength * 0.75f;
			case kPowerUpStrength.Max: return powerUpMegaDozerMaxLength * 1.00f;
		}
	}

	public int GetPowerUpQuakeCounts(kPowerUpStrength strength)
	{
		InitBalanceStates();
		switch (strength)
		{
			default:
			case kPowerUpStrength.Min: return 1;
			case kPowerUpStrength.Mid: return 2;
			case kPowerUpStrength.Max: return 3;
		}
	}

	public Vector3 GetPowerUpQuakeForce()
	{
		InitBalanceStates();
		return new Vector3(Random.Range(-2.5f, +2.5f), Random.Range(2.0f, 6.0f), Random.Range(0.25f, 1.25f)) * powerUpQuakeMaxForceScale;
	}

	public float GetPowerUpShieldDuration(kPowerUpStrength strength)
	{
		InitBalanceStates();
		switch (strength)
		{
			default:
			case kPowerUpStrength.Min: return powerUpShieldMaxDuration * 0.60f;
			case kPowerUpStrength.Mid: return powerUpShieldMaxDuration * 0.80f;
			case kPowerUpStrength.Max: return powerUpShieldMaxDuration * 1.00f;
		}
	}

	//------------------------------------------------------------------------------------------------------------------

	int GetSpawnObjectsCount(kObjectId objectId)
	{
		switch (objectId)
		{
			case kObjectId.BonusChipGolden: return IsApply(goldenCoin_chanceSpawn2x) ? 2 : 1;
			case kObjectId.BonusChipGem:    return IsApply(gemChip_chanceSpawn2x)    ? 2 : 1;
			case kObjectId.BonusChipXP:		return IsApply(xpChip_chanceSpawn2x)     ? 2 : 1;
			case kObjectId.BonusChipGift: 	return IsApply(giftChip_chanceSpawn2x)   ? 2 : 1;
		}

		if (objectId == kObjectId.BonusChipPowerUp || ObjectsManager.GetObjectGroupId(objectId) == kObjectGroupdId.PowerChip)
			return IsApply(powerUpChip_chanceSpawn2x) ? 2 : 1;

		return 1;
	}

	public List<kObjectId> GetGiftSpawnObjectIds()
	{
		InitBalanceStates();

		List<kObjectId> objectIdsList = new List<kObjectId>();

		int chipsCount = IsApply(giftChip_chanceSpawnChips2x) ? 2 : (IsApply(giftChip_chanceSpawnChips1x) ? 1 : 0);

		for (int i = 0; i < chipsCount; i++)
		{
			kObjectId objectId = this.GetRandomChipObjectId();
			int count = GetSpawnObjectsCount(objectId);

			for (int j = 0; j < count; j++)
				objectIdsList.Add(objectId);
		}

		int prizesCount = IsApply(giftChip_chanceSpawnPrizes3x) ? 3 : (IsApply(giftChip_chanceSpawnPrizes2x) ? 2 : 1);

		for (int i = 0; i < prizesCount; i++)
		{
			kObjectId objectId = this.GetRandomPrizeObjectId();
			int count = GetSpawnObjectsCount(objectId);

			for (int j = 0; j < count; j++)
				objectIdsList.Add(objectId);
		}

		return objectIdsList;
	}

	//------------------------------------------------------------------------------------------------------------------

	public List<kObjectId> GetAutoBonusSpawnObjectIds()
	{
		InitBalanceStates();

		List<kObjectId> objectIdsList = null;

		if (IsApply(autoBonus_chanceSpawnChip))
		{
			if (objectIdsList == null)
				objectIdsList = new List<kObjectId>();

			int itemsOnBoard = boardManager.GetChipsOnBoard();
			int itemsMaxCount = this.MaxCountChipsOnBoard;

			kObjectId objectId = this.GetRandomChipObjectId();
			int spawnCount = GetSpawnObjectsCount(objectId);

			for (int i = 0; i < spawnCount; i++)
			{
				if (itemsOnBoard >= itemsMaxCount)
					break;

				objectIdsList.Add(objectId);
				itemsOnBoard++;
			}
		}

		if (IsApply(autoBonus_chanceSpawnPrize))
		{
			if (objectIdsList == null)
				objectIdsList = new List<kObjectId>();

			int itemsOnBoard = boardManager.GetPrizesOnBoard();
			int itemsMaxCount = this.MaxCountPrizesOnBoard;

			kObjectId objectId = this.GetRandomPrizeObjectId();
			int spawnCount = GetSpawnObjectsCount(objectId);

			for (int i = 0; i < spawnCount; i++)
			{
				if (itemsOnBoard >= itemsMaxCount)
					break;

				objectIdsList.Add(objectId);
				itemsOnBoard++;
			}
		}

		return objectIdsList;
	}

	//------------------------------------------------------------------------------------------------------------------

	public kObjectId GetRandomChipObjectId()
	{
		InitBalanceStates();

		kObjectId objectId = chipsObjectIds[Random.Range(0, chipsObjectIds.Count)];

		if (objectId == kObjectId.BonusChipPowerUp)
		{
			objectId = GetRandomPowerChipObjectId();
		}

		return objectId;
	}

	public kObjectId GetRandomPowerChipObjectId()
	{
		InitBalanceStates();
		return chipsPowerUpsObjectIds[Random.Range(0, chipsPowerUpsObjectIds.Count)];
	}

	public kObjectId GetRandomPrizeObjectId()
	{
		InitBalanceStates();
		return prizesObjectIds[Random.Range(0, prizesObjectIds.Count)];
	}

	//------------------------------------------------------------------------------------------------------------------
	// Upgrades prices

	public int GetPrizeSellRewardCoins(kObjectId prizeId)
	{
		// @marker: PrizesList
		switch (prizeId)
		{
			case kObjectId.Prize0: return 10;
			case kObjectId.Prize1: return 12;
			case kObjectId.Prize2: return 15;
			case kObjectId.Prize3: return 12;
			case kObjectId.Prize4: return 16;
			case kObjectId.Prize5: return 12;
			case kObjectId.Prize6: return 16;
			case kObjectId.Prize7: return 20;
			case kObjectId.Prize8: return 15;
			case kObjectId.Prize9: return 20;
		}
		return 0;
	}

	public int GetPrizeSellRewardGems(kObjectId prizeId)
	{
		// @marker: PrizesList
		switch (prizeId)
		{
			case kObjectId.Prize3: return 1;
			case kObjectId.Prize4: return 1;
			case kObjectId.Prize8: return 1;
			case kObjectId.Prize9: return 1;
		}
		return 0;
	}

	public int GetPrizeSellRewardPowerUps(kObjectId prizeId)
	{
		// @marker: PrizesList
		switch (prizeId)
		{
			case kObjectId.Prize5: return 1;
			case kObjectId.Prize6: return 1;
			case kObjectId.Prize8: return 1;
			case kObjectId.Prize9: return 1;
		}
		return 0;
	}
}
