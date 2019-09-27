//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using System.Collections.Generic;
using UnityEngine;

public class BoardManager : ExtMonoBehaviour
{
	public enum FallAreaSide
	{
		FallSuccess = 0,
		FalloutLeft = -1,
		FalloutRight = +1,
	};

	public GameObject ObjectsDropArea;
	public GameObject CoinsDropPointer;
	public ParticleSystem fxSuccessFalls;
	public ParticleSystem fxOutsideFallsLeft;
	public ParticleSystem fxOutsideFallsRight;

	public GameObject tapInfoHelper;

	public List<GameObject> objectsOnBoard;

	void Awake()
	{
		objectsOnBoard = new List<GameObject>();
	}

	void Start()
	{
		InitDropPointsAndAreas();

		if (tapInfoHelper != null)
			tapInfoHelper.SetActive(true);

		LoadStates();
	}

	//==================================================================================================================

	void LoadStates()
	{
		try
		{
			int count = PlayerPrefs.GetInt("boardCount", 0);

			if (count == 0)
				throw new System.Exception("Apply-Default-States");

			for (int index = 0; index < count; index++)
			{
				string prefix = "boardObj" + index;

				kObjectId objectId = (kObjectId)PlayerPrefs.GetInt(prefix + "id", (int)kObjectId.Undefined);

				Vector3 position = Vector3.zero;
				position.x = PlayerPrefs.GetFloat(prefix + "px", 0.0f);
				position.y = PlayerPrefs.GetFloat(prefix + "py", 0.0f);
				position.z = PlayerPrefs.GetFloat(prefix + "pz", 0.0f);

				Vector3 rotation = Vector3.zero;
				rotation.x = PlayerPrefs.GetFloat(prefix + "rx", 0.0f);
				rotation.y = PlayerPrefs.GetFloat(prefix + "ry", 0.0f);
				rotation.z = PlayerPrefs.GetFloat(prefix + "rz", 0.0f);

				if (objectId == kObjectId.Undefined)
					continue;

				CreateObject(objectId, position, Quaternion.Euler(rotation), false);
			}
		}
		catch (System.Exception)
		{
			ApplyDefaultStates();
		}
	}

	public void SaveStates(bool isAutoSaveWrite)
	{
		try
		{
			PlayerPrefs.SetInt("boardCount", objectsOnBoard.Count);

			int index = 0;
			foreach (GameObject objectData in objectsOnBoard)
			{
				string prefix = "boardObj" + index;

				PlayerPrefs.SetInt(prefix + "id", (int)ObjectsManager.GetObjectId(objectData));

				{
					Vector3 val = objectData.transform.position;
					PlayerPrefs.SetFloat(prefix + "px", val.x);
					PlayerPrefs.SetFloat(prefix + "py", val.y);
					PlayerPrefs.SetFloat(prefix + "pz", val.z);
				}

				{
					Vector3 val = objectData.transform.rotation.eulerAngles;
					PlayerPrefs.SetFloat(prefix + "rx", val.x);
					PlayerPrefs.SetFloat(prefix + "ry", val.y);
					PlayerPrefs.SetFloat(prefix + "rz", val.z);
				}

				index++;
			}

			if (isAutoSaveWrite)
				PlayerPrefs.Save();
		}
		catch (System.Exception)
		{
			return;
		}
	}

	//==================================================================================================================
	// This method define what will be drop on the board on app launch!

	void ApplyDefaultStates()
	{
		for (int z = 0; z < 6; z++)
		{
			for (int x = 0; x < 5; x++)
			{
				if (z == 5 && (x == 0 || x == 4))
					continue;

				Vector3 position = new Vector3(1.025f * (x - 2),
											   6.0f + (Mathf.Abs(x - 2) + (5 - z)) * 0.5f + Random.Range(0.0f, 2.0f),
											   3.33f - 1.025f * (z - 2));

				// Why fixed "kObjectId.Coin1x" ?
				// To prevent "collision & quake" when it'll be fly
				// ps: maybe I was fixed this bug, so maybe allow to change to "balanceManager.CoinObjectId"

				CreateObject(kObjectId.Coin1x, position, Quaternion.identity, true);
			}
		}

		float delay = 3.0f;

		for (int i = 0; i < 5; i++)
		{
			if (i % 2 == 0)
				Invoke("AddRandomChip", delay);
			else
				Invoke("AddRandomPrize", delay);

			delay += 0.75f;
		}

		powerUpsManager.Invoke("RunCashfallOnAppLaunch", delay);
	}

	//==================================================================================================================

	GameObject CreateObject(kObjectId objectId, Vector3 position, Quaternion rotation, bool isInFallingState)
	{
		GameObject etalonObject = objectsManager.getObjectById(objectId);

		if (etalonObject == null)
		{
			Debug.LogWarning("CreateObject: Cannot detect etalonObject by objectId=" + objectId.ToString());
			return null;
		}

		GameObject boardObject = Instantiate(etalonObject, position, rotation) as GameObject;

		boardObject.GetComponent<BoardObjectScript>().Initialize(isInFallingState);

		objectsOnBoard.Add(boardObject);

		return boardObject;
	}

	void DropObject(GameObject boardObject, FallAreaSide fallAreaSide)
	{
		if (objectsOnBoard.Contains(boardObject))
			objectsOnBoard.Remove(boardObject);

		boardObject.GetComponent<BoardObjectScript>().DestroyWithAnimation(fallAreaSide);
	}

	public void DropObjectDirect(GameObject boardObject)
	{
		if (objectsOnBoard.Contains(boardObject))
			objectsOnBoard.Remove(boardObject);
	}

	//==================================================================================================================
	// Add Objects (any kind)

	public void AddCoinOnUserClicked()
	{
		if (playerManager.IsAllowAddCoinOnUserClicked() == false)
		{
			if (playerManager.CoinsAmount == 0)
			{
				if (PopupManager.IsAnyUIPopupVisible() == false)
				{
					popupManager.OpenPopup_CoinTimer();
				}
			}
			return;
		}

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			CreateObject(balanceManager.CoinObjectId,
						 new Vector3(hit.point.x, GetHeightCoinsDrop(), hit.point.z),
						 Quaternion.identity,
						 true);

			playerManager.DecCoinsAmountOnUserClicked();

			if (tapInfoHelper != null)
			{
				Destroy(tapInfoHelper);
				tapInfoHelper = null;
			}
		}

		this.AddRandomChipsOrPrizes_onAutoBonus();
	}

	public bool AddRandomCoin(Vector3 bonusOffset)
	{
		CreateObject(balanceManager.CoinObjectId, GetRandomObjectsDropPoint() + bonusOffset, Random.rotation, true);

		return true;
	}

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	public void AddRandomChipsOrPrizes_onAutoBonus()
	{
		var objectIdsList = balanceManager.GetAutoBonusSpawnObjectIds();

		if (objectIdsList == null)
			return;

		foreach (var objectId in objectIdsList)
		{
			AddObjectByObjectId(objectId);
		}
	}

	public void AddRandomChipOrPrize_onGiftChipCollected()
	{
		List<kObjectId> objectIdsList = balanceManager.GetGiftSpawnObjectIds();

		foreach (var objectId in objectIdsList)
		{
			AddObjectByObjectId(objectId);
		}
	}

	public void AddRandomChip()
	{
		AddObjectByObjectId(balanceManager.GetRandomChipObjectId());
	}

	public void AddRandomPrize()
	{
		AddObjectByObjectId(balanceManager.GetRandomPrizeObjectId());
	}

	public void AddObjectByObjectId(kObjectId objectId)
	{
		bool randomRotation = ObjectsManager.GetObjectGroupId(objectId) == kObjectGroupdId.Prize;

		CreateObject(
			objectId,
			GetRandomObjectsDropPoint(),
			randomRotation ? Random.rotation : Quaternion.identity,
			true);
	}

	//==================================================================================================================
	// Helpers

	float heightCoinsDrop;
	Vector3 objectsDropCenter;
	Vector3 objectsDropHalfSize;

	void InitDropPointsAndAreas()
	{
		ObjectsDropArea.SetActive(false);
		CoinsDropPointer.SetActive(false);

		heightCoinsDrop = CoinsDropPointer.transform.TransformPoint(Vector3.zero).y;

		objectsDropCenter = ObjectsDropArea.transform.TransformPoint(Vector3.zero);
		objectsDropHalfSize = ObjectsDropArea.GetComponent<MeshRenderer>().bounds.size / 2.0f;
	}

	Vector3 GetRandomObjectsDropPoint()
	{
		return objectsDropCenter + new Vector3(
			Random.Range(-objectsDropHalfSize.x, +objectsDropHalfSize.x),
			Random.Range(-objectsDropHalfSize.y, +objectsDropHalfSize.y),
			Random.Range(-objectsDropHalfSize.z, +objectsDropHalfSize.z));
	}

	float GetHeightCoinsDrop()
	{
		return heightCoinsDrop;
	}

	//==================================================================================================================

	public void ObjectDidFall(GameObject objectRef, FallAreaSide fallAreaSide)
	{
		kObjectId objectId = ObjectsManager.GetObjectId(objectRef);

		if (objectId == kObjectId.Undefined)
			return;

		if (fallAreaSide == FallAreaSide.FallSuccess)
		{
			switch (objectId)
			{
				case kObjectId.Coin1x:
					playerManager.IncCoinsAmount(1);
					break;

				case kObjectId.Coin2x:
					playerManager.IncCoinsAmount(2);
					break;

				case kObjectId.Coin5x:
					playerManager.IncCoinsAmount(5);
					break;

				// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

				case kObjectId.BonusChipGolden:
					playerManager.IncCoinsAmount(balanceManager.CoinsRewardAmountForGoldenChip);
					break;

				case kObjectId.BonusChipGem:
					playerManager.IncGemsAmount(balanceManager.GemsRewardAmountForGemChip);
					break;
				
				case kObjectId.BonusChipXP:
					playerManager.IncExperienceAmount(balanceManager.ExperienceAmountForXPChip);
					break;
				
				case kObjectId.BonusChipGift:
					AddRandomChipOrPrize_onGiftChipCollected();
					break;

				case kObjectId.BonusChipPowerUp:
					playerManager.IncPowerUpAmount(balanceManager.PowerUpRewardAmountForPowerUpChip);
					break;

				// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

				case kObjectId.PowerChipCashfall:
					powerUpsManager.RunCashfall(kPowerUpStrength.Max);
					break;

				case kObjectId.PowerChipMegaDozer:
					powerUpsManager.RunMegaDozer(kPowerUpStrength.Max);
					break;

				case kObjectId.PowerChipQuake:
					powerUpsManager.RunQuake(kPowerUpStrength.Max);
					break;

				case kObjectId.PowerChipShield:
					powerUpsManager.RunShield(kPowerUpStrength.Max);
					break;

					// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

				default:
				{
					if (ObjectsManager.GetObjectGroupId(objectId) == kObjectGroupdId.Prize)
						playerManager.IncPrizeAmount(objectId, 1);

					break;
				}
			}

			// Also every coins/object/etc drop will give 1 exp
			playerManager.IncExperienceAmount(1);
			playerManager.IncActivePrizeProgress(1);

			if (fxSuccessFalls.isPlaying == true)
				fxSuccessFalls.Stop();

			fxSuccessFalls.Play();
		}
		else
		{
			if( objectId == kObjectId.Coin1x )
				playerManager.IncPowerUpProgress(1);
			else
				playerManager.IncPowerUpProgress(2);

			if (fallAreaSide == FallAreaSide.FalloutLeft)
			{
				if (fxOutsideFallsLeft.isPlaying == true)
					fxOutsideFallsLeft.Stop();

				fxOutsideFallsLeft.Play();
			}
			else if (fallAreaSide == FallAreaSide.FalloutRight)
			{
				if (fxOutsideFallsRight.isPlaying == true)
					fxOutsideFallsRight.Stop();

				fxOutsideFallsRight.Play();
			}
		}

		DropObject(objectRef,fallAreaSide);
	}

	public int GetChipsOnBoard()
	{
		int count = 0;

		foreach (GameObject objectRef in objectsOnBoard)
		{
			if (objectsManager.IsChip(objectRef))
				count++;
		}

		return count;
	}

	public int GetPrizesOnBoard()
	{
		int count = 0;

		foreach (GameObject objectRef in objectsOnBoard)
		{
			if (objectsManager.IsPrize(objectRef))
				count++;
		}

		return count;
	}
}
