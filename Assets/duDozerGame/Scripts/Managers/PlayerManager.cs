//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ExtMonoBehaviour
{
	[Header("Dozer Game Config")]
	public int StartCoinsAmount = 100;
	public int StartGemsAmount = 10;
	public int StartPowerUpsAmount = 20;

	//------------------------------------------------------------------------------------------------------------------

	public const float kIncCoinsAmountPerSeconds = 0.33f;

	public const float kOfflineRegenCoinsTimeLimitMin = 2.5f * 60.0f;
	public const float kOfflineRegenCoinsTimeLimitMax = 15 * 24 * 60 * 60;

	//------------------------------------------------------------------------------------------------------------------

	int level;
	int experience;

	kObjectId levelUpGiftPrizeObjectId;
	kObjectId levelUpGiftChipObjectId;

	int coinsAmount;
	int coinsRewardBuffer; // This value use to incrase 'coinsAmount' but one-by-one with small delay
	int gemsAmount;
	int powerUpsAmount;

	kObjectId activePrizeObjectId;
	int activePrizeProgress;
	kObjectId changePrizeObjectId1;
	kObjectId changePrizeObjectId2;
	kObjectId changePrizeObjectId3;

	kPowerUpType powerUpActiveType;
	int powerUpFillProgress;

	float coinsRegenSecondsLeft;
	int coinsIdleCount;

	Dictionary<kObjectId, int> prizesBank;
	Dictionary<kUpgradeId, int> upgradeLevels;

	bool isProUser;

	//------------------------------------------------------------------------------------------------------------------

	void Awake()
	{
		prizesBank = new Dictionary<kObjectId, int>();
		upgradeLevels = new Dictionary<kUpgradeId, int>();
	}

	void Start()
	{
		LoadStates();
	
		Invoke("DoRewardOfflineRegenCoins", 1.0f);
	}

	// This call on user Launch application && Back from background to foreground
	void OnApplicationPause(bool paused)
	{
		if (paused == false)
		{
			Invoke("DoRewardOfflineRegenCoins", 1.0f);
		}
	}

	void Update()
	{
		UpdateIdleCoinsState();
		UpdateCoinsRegenState();
	}

	//==================================================================================================================

	string playerGuid;

	public string getPlayerGUID()
	{
		if (playerGuid == null)
		{
			playerGuid = PlayerPrefs.GetString("playerGuid", "");

			if (playerGuid == "")
			{
				playerGuid = System.Guid.NewGuid().ToString();

				PlayerPrefs.SetString("playerGuid", playerGuid);
				PlayerPrefs.Save();
			}
		}

		return playerGuid;
	}

	//==================================================================================================================
	// Level & Experience

	public int Level
	{
		get { return level; }
	}

	public int Experience
	{
		get { return experience; }
	}

	public int ExperienceLimit
	{
		get { return 20 + level * 5; }
	}

	public void IncExperienceAmount(int amount)
	{
		experience += amount;

		while (experience >= this.ExperienceLimit)
		{
			experience -= this.ExperienceLimit;
			LevelUp();
		}
	}

	public kObjectId LevelUpGiftPrizeObjectId
	{
		get { return levelUpGiftPrizeObjectId; }
	}

	public kObjectId LevelUpGiftChipObjectId
	{
		get { return levelUpGiftChipObjectId; }
	}

	void LevelUp()
	{
		level++;

		popupManager.OpenPopup_LevelUp();
	}

	public void DropPrizesOnLevelUp()
	{
		boardManager.AddObjectByObjectId(levelUpGiftPrizeObjectId);
		boardManager.AddObjectByObjectId(levelUpGiftChipObjectId);

		GenerateLevelUpGifts();
	}

	void GenerateLevelUpGifts()
	{
		levelUpGiftPrizeObjectId = balanceManager.GetRandomPrizeObjectId();
		levelUpGiftChipObjectId = balanceManager.GetRandomChipObjectId();
	}

	//==================================================================================================================
	// Coins

	public int CoinsAmount
	{
		get { return coinsAmount; }
	}

	public int CoinsRewardBuffer
	{
		get { return coinsRewardBuffer; }
	}

	public void IncCoinsAmount(int amount, bool isImmediately = false)
	{
		if (amount == 0)
			return;

		if (isImmediately)
		{
			coinsAmount += amount;
		}
		else
		{
			coinsRewardBuffer += amount;
		}
	}

	public void DecCoinsAmountOnUserClicked()
	{
		coinsAmount--;

		coinsIdleCount--;

		Invoke("SpawnIdleCoin", 2.5f);
	}

	void SpawnIdleCoin()
	{
		coinsIdleCount++;
	}

	public bool IsAllowAddCoinOnUserClicked()
	{
		if (coinsIdleCount == 0)
			return false;

		if (coinsAmount == 0)
			return false;

		if (PopupManager.IsAnyUIPopupVisible())
			return false;

		return true;
	}

	void UpdateCoinsRegenState()
	{
		coinsRegenSecondsLeft -= Time.deltaTime;

		if (coinsRegenSecondsLeft <= 0.0f)
		{
			coinsRegenSecondsLeft = balanceManager.CoinsRegenTimeout;

			if (coinsAmount < balanceManager.CoinsRegenMaxAmount)
			{
				IncCoinsAmount(1, true);
			}
		}
	}

	void DoRewardOfflineRegenCoins()
	{
		try
		{
			long lastOnlineTs = GetLastOnlineTime();

			if (lastOnlineTs == 0)
				throw new System.Exception("lastOnlineTs = 0");

			System.TimeSpan diffTimes = System.DateTime.Now - System.DateTime.FromFileTime(lastOnlineTs);

			int secondsWasOffline = (int)diffTimes.TotalSeconds;

			Debug.Log("PlayerManager.DoRewardOfflineRegenCoins: secondsWasOffline=" + secondsWasOffline);

			if (secondsWasOffline < kOfflineRegenCoinsTimeLimitMin)
				throw new System.Exception("secondsWasOffline[" + secondsWasOffline + "] < " + kOfflineRegenCoinsTimeLimitMin + " sec");

			if (secondsWasOffline > kOfflineRegenCoinsTimeLimitMax)
				throw new System.Exception("secondsWasOffline[" + secondsWasOffline + "] > " + kOfflineRegenCoinsTimeLimitMax + " sec");

			// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

			int maxAllowCoinsReward = balanceManager.CoinsRegenMaxAmount - this.coinsAmount;

			string message = "";

			if (maxAllowCoinsReward > 0)
			{
				int coinsReward = secondsWasOffline / balanceManager.OfflineRegenTimeForOneCoin;

				coinsReward = Mathf.Min(coinsReward, maxAllowCoinsReward);

				if (coinsReward > 0)
				{
					if( coinsReward == 1 )
						message = "While you were offline" + "\n" + "you gained 1 Coin";
					else
						message = "While you were offline" + "\n" + "you gained " + coinsReward.ToString() + " Coins";

					this.IncCoinsAmount(coinsReward);
				}
				else
				{
					message = "While you were offline" + "\n" + "you gained 0 Coins";
				}
			}
			else
			{
				message = "You already have" + "\n" + "more coins than"+ "\n" + "the Coin Regen. Limit";
			}

			if (message == "")
				throw new System.Exception("Message is empty");

			if (PopupManager.IsAnyUIPopupVisible() == false)
			{
				// Prevent open few CoinOfflineReport popups
				popupManager.OpenPopup_CoinOfflineReport(message);
			}

			SetLastOnlineTime(true);
		}
		catch (System.Exception e)
		{
			Debug.Log("PlayerManager.DoRewardOfflineRegenCoins: " + e.Message);
			return;
		}
	}

	//==================================================================================================================
	// Gems

	public int GemsAmount
	{
		get { return gemsAmount; }
	}

	public void IncGemsAmount(int amount)
	{
		if (amount == 0)
			return;

		gemsAmount += amount;
	}

	public bool DecGemsAmount(int amount)
	{
		if (gemsAmount < amount)
			return false;

		gemsAmount -= amount;

		return true;
	}

	//==================================================================================================================
	// PowerUps

	public int PowerUpsAmount
	{
		get { return powerUpsAmount; }
	}

	public void IncPowerUpAmount(int amount)
	{
		if (amount == 0)
			return;

		powerUpsAmount += amount;
	}

	public bool DecPowerUpAmount(int amount)
	{
		if (powerUpsAmount < amount)
			return false;

		powerUpsAmount -= amount;

		return true;
	}

	//==================================================================================================================
	// Prize Progress

	public kObjectId ActivePrizeObjectId
	{
		get { return activePrizeObjectId; }
		set { activePrizeObjectId = value; GenerateChangePrizeObjects(); }
	}

	public int ActivePrizeProgress
	{
		get { return activePrizeProgress; }
	}

	public kObjectId ChangePrizeObjectId1
	{
		get { return changePrizeObjectId1; }
	}

	public kObjectId ChangePrizeObjectId2
	{
		get { return changePrizeObjectId2; }
	}

	public kObjectId ChangePrizeObjectId3
	{
		get { return changePrizeObjectId3; }
	}

	public void IncActivePrizeProgress(int amount)
	{
		activePrizeProgress += amount;

		if (activePrizeProgress < this.ActivePrizeProgressLimit)
			return;

		// Trigger drop prize
		boardManager.AddObjectByObjectId(activePrizeObjectId);

		activePrizeProgress -= this.ActivePrizeProgressLimit;
		activePrizeObjectId = balanceManager.GetRandomPrizeObjectId();
	}

	public int ActivePrizeProgressLimit
	{
		get { return 30 + Mathf.Min( 30, Mathf.FloorToInt(level/2) ); }
	}

	public void GenerateChangePrizeObjects()
	{
		changePrizeObjectId1 = activePrizeObjectId;
		changePrizeObjectId2 = activePrizeObjectId;
		changePrizeObjectId3 = activePrizeObjectId;

		while (changePrizeObjectId1 == activePrizeObjectId)
		{
			changePrizeObjectId1 = kObjectId.Prize0 + Random.Range(0, 5); // 0..4
		}

		while (changePrizeObjectId2 == activePrizeObjectId)
		{
			changePrizeObjectId2 = kObjectId.Prize5 + Random.Range(0, 3); // 0..2
		}

		while (changePrizeObjectId3 == activePrizeObjectId)
		{
			changePrizeObjectId3 = kObjectId.Prize8 + Random.Range(0, 2); // 0..1
		}
	}

	//==================================================================================================================
	// Power Ups

	public kPowerUpType PowerUpActiveType
	{
		get { return powerUpActiveType; }
	}

	public int PowerUpFillProgress
	{
		get { return powerUpFillProgress; }
	}

	public void IncPowerUpProgress(int amount)
	{
		powerUpFillProgress = Mathf.Min(powerUpFillProgress + amount, PowerUprProgressLimit);
	}

	public int PowerUprProgressLimit
	{
		get { return 30 + Mathf.Min(15, Mathf.FloorToInt(level / 3)); }
	}

	public float PowerUpProgressComplete
	{
		get { return powerUpFillProgress / (float)this.PowerUprProgressLimit; }
	}

	public kPowerUpStrength GetPowerUpStrength()
	{
		if (powerUpFillProgress >= this.PowerUprProgressLimit)
			return kPowerUpStrength.Max;

		if (powerUpFillProgress >= this.PowerUprProgressLimit * 0.66f)
			return kPowerUpStrength.Mid;

		if (powerUpFillProgress >= this.PowerUprProgressLimit * 0.33f)
			return kPowerUpStrength.Min;

		return kPowerUpStrength.None;
	}

	public void ResetPowerUpState()
	{
		powerUpFillProgress = 0;
		powerUpActiveType = PowerUpsManager.GetRandomPowerUpType();
	}

	//==================================================================================================================
	// Prizes Bank

	public int GetPrizeAmount(kObjectId prizeId)
	{
		if (prizesBank.ContainsKey(prizeId) == false)
			return 0;

		return prizesBank[prizeId];
	}

	public void IncPrizeAmount(kObjectId prizeId, int amount = 1)
	{
		if (prizesBank.ContainsKey(prizeId) == false)
			prizesBank[prizeId] = 0;

		prizesBank[prizeId] += amount;
	}

	public void DecPrizeAmount(kObjectId prizeId, int amount = 1)
	{
		if (prizesBank.ContainsKey(prizeId) == false)
			prizesBank[prizeId] = 0;

		prizesBank[prizeId] -= amount;

		if (prizesBank[prizeId] < 0)
			prizesBank[prizeId] = 0;
	}

	public bool SellPrize(kObjectId prizeId)
	{
		int amount = GetPrizeAmount(prizeId);

		if (amount <= 0)
			return false;

		DecPrizeAmount(prizeId);

		IncCoinsAmount(balanceManager.GetPrizeSellRewardCoins(prizeId),true);
		IncGemsAmount(balanceManager.GetPrizeSellRewardGems(prizeId));
		IncPowerUpAmount(balanceManager.GetPrizeSellRewardPowerUps(prizeId));

		return true;
	}

	//==================================================================================================================
	// Upgrades

	public int GetUpgradeLevel(kUpgradeId upgradeId)
	{
		if (upgradeLevels.ContainsKey(upgradeId) == false)
			return 0;

		int upgradeLevel = upgradeLevels[upgradeId];

		if (upgradeLevel > 5)
			upgradeLevel = 5;

		return upgradeLevel;
	}

	public bool IncUpgradeLevel(kUpgradeId upgradeId)
	{
		if (upgradeLevels.ContainsKey(upgradeId) == false)
			upgradeLevels[upgradeId] = 0;

		if (upgradeLevels[upgradeId] >= 5)
			return false;

		upgradeLevels[upgradeId]++;

		balanceManager.ResetBalanceStates();

		return true;
	}

	//==================================================================================================================

	public float CoinsRegenSecondsLeft
	{
		get { return coinsRegenSecondsLeft; }
	}

	public int CoinsIdleCount
	{
		get { return coinsIdleCount; }
	}

	float coinsRewardIdleTimer = 0.0f;

	public void UpdateIdleCoinsState()
	{
		if (coinsRewardIdleTimer <= 0.0f)
		{
			coinsRewardIdleTimer = kIncCoinsAmountPerSeconds;

			if (coinsRewardBuffer > 0)
			{
				int incPortion;

				if (coinsRewardBuffer > 100)
				{
					incPortion = 100;
				}
				else if (coinsRewardBuffer > 20)
				{
					incPortion = 10;
				}
				else
				{
					incPortion = 1;
				}

				IncCoinsAmount(incPortion, true);
				coinsRewardBuffer -= incPortion;
			}
		}
		else
		{
			coinsRewardIdleTimer -= Time.deltaTime;
		}
	}

	//==================================================================================================================
	// Load & Save

	public bool LoadStates()
	{
		SetDefaultStates();

		try
		{
			level = PlayerPrefs.GetInt("level", level);
			experience = PlayerPrefs.GetInt("exp", experience);

			levelUpGiftPrizeObjectId = (kObjectId)PlayerPrefs.GetInt("levelUpGiftPrize", (int)levelUpGiftPrizeObjectId);
			levelUpGiftChipObjectId = (kObjectId)PlayerPrefs.GetInt("levelUpGiftChip", (int)levelUpGiftChipObjectId);

			coinsAmount = PlayerPrefs.GetInt("coins", coinsAmount);
			coinsRewardBuffer = PlayerPrefs.GetInt("coinsbuf", coinsRewardBuffer);
			gemsAmount = PlayerPrefs.GetInt("gems", gemsAmount);
			powerUpsAmount = PlayerPrefs.GetInt("powerups", powerUpsAmount);

			activePrizeObjectId = (kObjectId)PlayerPrefs.GetInt("prizeId", (int)activePrizeObjectId);
			activePrizeProgress = PlayerPrefs.GetInt("prizePrg", activePrizeProgress);

			powerUpActiveType = (kPowerUpType)PlayerPrefs.GetInt("powerUpId", (int)powerUpActiveType);
			powerUpFillProgress = PlayerPrefs.GetInt("powerUpPrg", powerUpFillProgress);

			foreach (var prizeId in ObjectsManager.kAllPrizesList)
			{
				prizesBank[prizeId] = PlayerPrefs.GetInt("bankPrize" + prizeId, GetPrizeAmount(prizeId));
			}

			foreach (var upgradeId in UpgradesManager.kAllUpgradeIds)
			{
				upgradeLevels[upgradeId] = PlayerPrefs.GetInt("upgradeLvl" + upgradeId, GetUpgradeLevel(upgradeId));
			}

			isProUser = PlayerPrefs.GetInt("isProUser", 0) > 0 ? true : false;

			// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
			// Require to validate all data!

			level = Mathf.Max(level, 1);

			experience = Mathf.Max(experience, 0);
			experience = Mathf.Min(experience, this.ExperienceLimit - 1);

			coinsAmount = Mathf.Max(coinsAmount, 0);
			coinsRewardBuffer = Mathf.Max(coinsRewardBuffer, 0);
			gemsAmount = Mathf.Max(gemsAmount, 0);
			powerUpsAmount = Mathf.Max(powerUpsAmount, 0);

			if (objectsManager.IsPrize(activePrizeObjectId) == false)
				activePrizeObjectId = balanceManager.GetRandomPrizeObjectId();

			activePrizeProgress = Mathf.Max(activePrizeProgress, 0);
			activePrizeProgress = Mathf.Min(activePrizeProgress, ActivePrizeProgressLimit - 1);

			switch (powerUpActiveType)
			{
				case kPowerUpType.Cashfall:
				case kPowerUpType.MegaDozer:
				case kPowerUpType.Quake:
				case kPowerUpType.Shield:
					// okey!
					break;

				default:
					powerUpActiveType = kPowerUpType.Shield;
					break;
			}

			powerUpFillProgress = Mathf.Max(powerUpFillProgress, 0);
			powerUpFillProgress = Mathf.Min(powerUpFillProgress, powerUpFillProgress - 1);

			if (levelUpGiftPrizeObjectId == kObjectId.Undefined || levelUpGiftChipObjectId == kObjectId.Undefined)
				GenerateLevelUpGifts();
		}
		catch (System.Exception e)
		{
			SetDefaultStates();
			Debug.LogWarning("PlayerManager.LoadStates: " + e.Message);
			return false;
		}

		balanceManager.ResetBalanceStates();

		return true;
	}

	public void SetDefaultStates()
	{
		// Set default values for reason if error happened on read data!

		level = 1;
		experience = 0;

		GenerateLevelUpGifts();

		coinsAmount = StartCoinsAmount;
		gemsAmount = StartGemsAmount;
		powerUpsAmount = StartPowerUpsAmount;

		coinsRewardBuffer = 0;

		activePrizeObjectId = kObjectId.Prize0;
		activePrizeProgress = 0;

		powerUpActiveType = kPowerUpType.Shield;
		powerUpFillProgress = 0;

		isProUser = false;

		// [1.B] Default values not non-read value

		coinsRegenSecondsLeft = 20f;
		coinsIdleCount = 6;

		GenerateChangePrizeObjects();
	}

	public void SaveStates(bool isAutoSaveWrite)
	{
		Debug.Log("PlayerManager.SaveStates");

		try
		{
			PlayerPrefs.SetInt("level", level);
			PlayerPrefs.SetInt("exp", experience);

			PlayerPrefs.SetInt("levelUpGiftPrize", (int)levelUpGiftPrizeObjectId);
			PlayerPrefs.SetInt("levelUpGiftChip", (int)levelUpGiftChipObjectId);

			PlayerPrefs.SetInt("coins", coinsAmount);
			PlayerPrefs.SetInt("coinsbuf", coinsRewardBuffer);
			PlayerPrefs.SetInt("gems", gemsAmount);
			PlayerPrefs.SetInt("powerups", powerUpsAmount);

			PlayerPrefs.SetInt("prizeId", (int)activePrizeObjectId);
			PlayerPrefs.SetInt("prizePrg", activePrizeProgress);

			PlayerPrefs.SetInt("powerUpId", (int)powerUpActiveType);
			PlayerPrefs.SetInt("powerUpPrg", powerUpFillProgress);

			foreach (var prizeId in ObjectsManager.kAllPrizesList)
			{
				PlayerPrefs.SetInt("bankPrize" + prizeId, GetPrizeAmount(prizeId));
			}

			foreach (var upgradeId in UpgradesManager.kAllUpgradeIds)
			{
				PlayerPrefs.SetInt("upgradeLvl" + upgradeId, GetUpgradeLevel(upgradeId));
			}

			PlayerPrefs.SetInt("isProUser", (isProUser ? 1 : 0));

			SetLastOnlineTime(false);

			if (isAutoSaveWrite)
				PlayerPrefs.Save();
		}
		catch (System.Exception e)
		{
			Debug.LogWarning("PlayerManager.SaveStates: " + e.Message);
		}
	}

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	long GetLastOnlineTime()
	{
		return System.Convert.ToInt64(PlayerPrefs.GetString("lastOnlineTimestamp","0"));
	}

	void SetLastOnlineTime(bool isAutoSaveWrite)
	{
		long lastOnlineTs = GetLastOnlineTime();

		if (lastOnlineTs > 0)
		{
			System.TimeSpan diffTimes = System.DateTime.Now - System.DateTime.FromFileTime(lastOnlineTs);

			int secondsWasOffline = (int)diffTimes.TotalSeconds;

			if (secondsWasOffline < kOfflineRegenCoinsTimeLimitMin)
				return; // no need to save lastOnlineTs if user was online less then #-seconds
		}

		//Debug.LogWarning("PlayerManager.SetLastOnlineTime");
		PlayerPrefs.SetString("lastOnlineTimestamp", System.DateTime.Now.ToFileTime().ToString());

		if (isAutoSaveWrite)
			PlayerPrefs.Save();
	}
}
