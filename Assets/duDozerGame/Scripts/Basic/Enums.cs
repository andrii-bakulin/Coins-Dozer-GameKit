//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using System;

//----------------------------------------------------------------------------------------------------------------------

public enum kObjectId
{
	Undefined = 0,

	Coin1x = 101,
	Coin2x = 102,
	Coin5x = 103,

	BonusChipGolden = 201,
	BonusChipGem = 202,
	BonusChipXP = 203,
	BonusChipGift = 204,
	BonusChipPowerUp = 205,

	PowerChipCashfall = 301,
	PowerChipMegaDozer = 302,
	PowerChipQuake = 303,
	PowerChipShield = 304,

	Prize0 = 400, // @notice: each next prizeId value should be +1
	Prize1 = 401,
	Prize2 = 402,
	Prize3 = 403,
	Prize4 = 404,
	Prize5 = 405,
	Prize6 = 406,
	Prize7 = 407,
	Prize8 = 408,
	Prize9 = 409,
};

public enum kObjectGroupdId
{
	Undefined = 0,
	Coin = 100,
	BonusChip = 200,
	PowerChip = 300,
	Prize = 400,
}

//----------------------------------------------------------------------------------------------------------------------

public enum kPowerUpType
{
	Undefined = 0,
	Cashfall = 501,
	MegaDozer = 502,
	Quake = 503,
	Shield = 504,
};

public enum kPowerUpStrength
{
	None = 610,
	Min = 611,
	Mid = 612,
	Max = 613,
};

//----------------------------------------------------------------------------------------------------------------------

public enum kUpgradeId
{
	Undefined = 0,

	ChipBasicCoin = 701,
	ChipGoldenCoin = 702,
	ChipGem = 703,
	ChipXP = 704,
	ChipGift = 705,
	ChipPowerUp = 706,

	PowerUpCashfall = 711,
	PowerUpMegaDozer = 712,
	PowerUpQuake = 713,
	PowerUpShield = 714,

	SpecImproveChipsAttrs = 721,
	SpecImprovePrizeAttrs = 722,
	SpecRegenSpeed = 723,
	SpecRegenMax = 724,
	SpecOfflineRegenSpeed = 725,
};
