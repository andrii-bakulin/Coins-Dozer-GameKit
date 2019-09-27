//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIPowerUpsButton : ExtMonoBehaviour
{
	public GameObject amountBox;
	public UnityEngine.UI.Text amountLabel;

	void Start()
	{
		UpdateStates();
	}

	void UpdateStates()
	{
		int powersAmount = playerManager.PowerUpsAmount;

		if (powersAmount > 0)
		{
			if (amountBox != null)
				amountBox.SetActive(true);

			if (amountLabel != null)
				amountLabel.text = powersAmount.ToString();
		}
		else
		{
			if (amountBox != null)
				amountBox.SetActive(false);
		}

		Invoke("UpdateStates", 0.1f);
	}
}
