//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UICoinsIdleBar : ExtMonoBehaviour
{
	public UnityEngine.UI.Image[] idleCoinsImages;

	void Update()
	{
		if (idleCoinsImages == null || idleCoinsImages.Length != 6)
		{
			Debug.LogError("Array idleCoinsImages should have 6 object!");
			return;
		}

		for (int i = 0; i < 6; i++)
		{
			idleCoinsImages[i].enabled = playerManager.CoinsIdleCount > i;
		}
	}
}
