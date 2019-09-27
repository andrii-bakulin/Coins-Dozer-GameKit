//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIStockPopupItem : ExtMonoBehaviour
{
	public kObjectId objectId = kObjectId.Undefined;

	public UnityEngine.UI.Image iconImage;
	public UnityEngine.UI.Text amountLabel;
	public GameObject sellButton;

	public GameObject CoinsAmountHolder;
	public GameObject GemsAmountHolder;
	public GameObject PowerUpsAmountHolder;

	void Start()
	{
		if (objectId == kObjectId.Undefined)
			return;

		iconImage.sprite = resourcesManager.GetPrizeSprite(objectId);

		int amount;

		amount = balanceManager.GetPrizeSellRewardCoins(objectId);
		CoinsAmountHolder.SetActive(amount > 0);
		CoinsAmountHolder.GetComponentInChildren<UnityEngine.UI.Text>().text = amount.ToString();

		amount = balanceManager.GetPrizeSellRewardGems(objectId);
		GemsAmountHolder.SetActive(amount > 0);
		GemsAmountHolder.GetComponentInChildren<UnityEngine.UI.Text>().text = amount.ToString();

		amount = balanceManager.GetPrizeSellRewardPowerUps(objectId);
		PowerUpsAmountHolder.SetActive(amount > 0);
		PowerUpsAmountHolder.GetComponentInChildren<UnityEngine.UI.Text>().text = amount.ToString();

		UpdateStates();
	}

	void UpdateStates()
	{
		int amount = playerManager.GetPrizeAmount(objectId);

		amountLabel.text = amount.ToString();
		sellButton.SetActive(amount > 0);

		Invoke("UpdateStates", 0.2f);
	}

	public void SellPrize()
	{
		playerManager.SellPrize(objectId);
	}
}
