//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UICoinTimerPopup : ExtMonoBehaviour
{
	public UnityEngine.UI.Text secondsLabel;

	void Start()
	{
		UpdateStates();
	}
	
	void UpdateStates()
	{
		secondsLabel.text = ((int)Mathf.Ceil(playerManager.CoinsRegenSecondsLeft)).ToString() + " sec";

		Invoke("UpdateStates", 0.5f);
	}
}
