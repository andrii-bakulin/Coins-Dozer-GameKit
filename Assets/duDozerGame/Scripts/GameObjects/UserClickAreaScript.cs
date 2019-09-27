//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UserClickAreaScript : ExtMonoBehaviour
{
	void OnMouseDown()
	{
		if (playerManager.CoinsAmount > 0)
		{
			// Require drop coin with small delay.
			// Why?
			// If you click on UI-button -> then need to show popup -> and then droping of button will be ignored
			Invoke("DropCoin", 0.15f);
		}
		else
		{
			// If user don't have coins -> require to show "CoinsPopup" right now!
			// To prevent showing few-popups if user make spam-clicks.
			DropCoin();
		}
	}

	void DropCoin()
	{
		boardManager.AddCoinOnUserClicked();
	}
}
