//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : ExtMonoBehaviour
{
	[Header("References to Objects: Coins")]
	public GameObject coin1x;
	public GameObject coin2x;
	public GameObject coin5x;

	[Header("References to Objects: Bonus Chips")]
	public GameObject bonusChipGolden;
	public GameObject bonusChipGem;
	public GameObject bonusChipXP;
	public GameObject bonusChipGift;
	public GameObject bonusChipPowerUp;

	[Header("References to Objects: PowerUp Chips")]
	public GameObject powerChipCashfall;
	public GameObject powerChipMegaDozer;
	public GameObject powerChipQuake;
	public GameObject powerChipShield;

	[Header("References to Objects: Prizes")]
	public GameObject prize0;
	public GameObject prize1;
	public GameObject prize2;
	public GameObject prize3;
	public GameObject prize4;
	public GameObject prize5;
	public GameObject prize6;
	public GameObject prize7;
	public GameObject prize8;
	public GameObject prize9;

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	public static kObjectId[] kAllPrizesList = {
		kObjectId.Prize0,
		kObjectId.Prize1,
		kObjectId.Prize2,
		kObjectId.Prize3,
		kObjectId.Prize4,
		kObjectId.Prize5,
		kObjectId.Prize6,
		kObjectId.Prize7,
		kObjectId.Prize8,
		kObjectId.Prize9,
	};

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	Dictionary<kObjectId, GameObject> objects;

	void Awake()
	{
		objects = new Dictionary<kObjectId, GameObject>();

		pushObject(coin1x, kObjectId.Coin1x);
		pushObject(coin2x, kObjectId.Coin2x);
		pushObject(coin5x, kObjectId.Coin5x);

		pushObject(bonusChipGolden, kObjectId.BonusChipGolden);
		pushObject(bonusChipGem, kObjectId.BonusChipGem);
		pushObject(bonusChipXP, kObjectId.BonusChipXP);
		pushObject(bonusChipGift, kObjectId.BonusChipGift);
		pushObject(bonusChipPowerUp, kObjectId.BonusChipPowerUp);

		pushObject(powerChipCashfall, kObjectId.PowerChipCashfall);
		pushObject(powerChipMegaDozer, kObjectId.PowerChipMegaDozer);
		pushObject(powerChipQuake, kObjectId.PowerChipQuake);
		pushObject(powerChipShield, kObjectId.PowerChipShield);

		pushObject(prize0, kObjectId.Prize0);
		pushObject(prize1, kObjectId.Prize1);
		pushObject(prize2, kObjectId.Prize2);
		pushObject(prize3, kObjectId.Prize3);
		pushObject(prize4, kObjectId.Prize4);
		pushObject(prize5, kObjectId.Prize5);
		pushObject(prize6, kObjectId.Prize6);
		pushObject(prize7, kObjectId.Prize7);
		pushObject(prize8, kObjectId.Prize8);
		pushObject(prize9, kObjectId.Prize9);
	}

	void Start()
	{
	}

	void pushObject(GameObject objectRef, kObjectId objectId)
	{
		if (objectRef == null)
			return;

		BoardObjectScript boardObjectScript = objectRef.GetComponent<BoardObjectScript>();

		if (boardObjectScript == null)
		{
			Debug.LogWarning("ObjectsManager: Cannot pushObject with objectId=" + objectId.ToString());
			return;
		}

		boardObjectScript.objectId = objectId;

		objects[objectId] = objectRef;
	}

	public GameObject getObjectById(kObjectId objectId)
	{
		if (objects.ContainsKey(objectId) == false)
			return null;

		return objects[objectId];
	}

	//------------------------------------------------------------------------------------------------------------------

	public bool IsChip(GameObject objectRef)
	{
		return IsChip(GetObjectId(objectRef));
	}

	public bool IsChip(kObjectId objectId)
	{
		kObjectGroupdId objectGroupId = GetObjectGroupId(objectId);
		return objectGroupId == kObjectGroupdId.BonusChip || objectGroupId == kObjectGroupdId.PowerChip;
	}

	public bool IsBonusChip(GameObject objectRef)
	{
		return IsBonusChip(GetObjectId(objectRef));
	}

	public bool IsBonusChip(kObjectId objectId)
	{
		return GetObjectGroupId(objectId) == kObjectGroupdId.BonusChip;
	}

	public bool IsPowerChip(GameObject objectRef)
	{
		return IsPowerChip(GetObjectId(objectRef));
	}

	public bool IsPowerChip(kObjectId objectId)
	{
		return GetObjectGroupId(objectId) == kObjectGroupdId.PowerChip;
	}

	public bool IsPrize(GameObject objectRef)
	{
		return IsPrize(GetObjectId(objectRef));
	}

	public bool IsPrize(kObjectId objectId)
	{
		return GetObjectGroupId(objectId) == kObjectGroupdId.Prize;
	}

	//------------------------------------------------------------------------------------------------------------------

	public static kObjectId GetObjectId(GameObject objectRef)
	{
		BoardObjectScript boardObjectScript = objectRef.GetComponent<BoardObjectScript>();

		if (boardObjectScript == null)
			return kObjectId.Undefined;

		return boardObjectScript.objectId;
	}

	public static kObjectGroupdId GetObjectGroupId(GameObject objectRef)
	{
		return GetObjectGroupId(GetObjectId(objectRef));
	}

	public static kObjectGroupdId GetObjectGroupId(kObjectId objectId)
	{
		switch (objectId)
		{
			case kObjectId.Undefined:
				return kObjectGroupdId.Undefined;

			case kObjectId.Coin1x:
			case kObjectId.Coin2x:
			case kObjectId.Coin5x:
				return kObjectGroupdId.Coin;

			case kObjectId.BonusChipGolden:
			case kObjectId.BonusChipGem:
			case kObjectId.BonusChipXP:
			case kObjectId.BonusChipGift:
			case kObjectId.BonusChipPowerUp:
				return kObjectGroupdId.BonusChip;

			case kObjectId.PowerChipCashfall:
			case kObjectId.PowerChipMegaDozer:
			case kObjectId.PowerChipQuake:
			case kObjectId.PowerChipShield:
				return kObjectGroupdId.PowerChip;
		}

		foreach (var prizeId in ObjectsManager.kAllPrizesList)
		{
			if (prizeId == objectId)
				return kObjectGroupdId.Prize;
		}

		return kObjectGroupdId.Undefined;
	}
}
