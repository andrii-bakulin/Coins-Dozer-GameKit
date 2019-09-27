//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using System;

public class UpgradeRules
{
	public kObjectId prize0_objectId = kObjectId.Undefined;
	public kObjectId prize1_objectId = kObjectId.Undefined;
	public kObjectId prize2_objectId = kObjectId.Undefined;

	public int prize0_amount;
	public int prize1_amount;
	public int prize2_amount;

	public void AddPrize(kObjectId objectId, int amount = 1)
	{
		if (prize0_objectId == kObjectId.Undefined)
		{
			prize0_objectId = objectId;
			prize0_amount = amount;
		}
		else if (prize1_objectId == kObjectId.Undefined)
		{
			prize1_objectId = objectId;
			prize1_amount = amount;
		}
		else if (prize2_objectId == kObjectId.Undefined)
		{
			prize2_objectId = objectId;
			prize2_amount = amount;
		}
	}
}
