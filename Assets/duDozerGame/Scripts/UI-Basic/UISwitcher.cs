//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;
using UnityEngine.UI;

public class UISwitcher : ExtMonoBehaviour
{
	public Image backImage;
	public Sprite backEnabledSprite;
	public Sprite backDisabledSprite;

	public Image buttonImage;
	public Sprite buttonEnabledSprite;
	public Sprite buttonDisabledSprite;

	public bool isSoundsSwitcher;
	public bool isVibrateSwitcher;

	//------------------------------------------------------------------------------------------------------------------

	Slider _sliderObject;

	Slider sliderObject
	{
		get
		{
			if (_sliderObject == null)
				_sliderObject = GetComponent<Slider>();

			return _sliderObject;
		}
	}

	//------------------------------------------------------------------------------------------------------------------

	public void InitState(bool enabled)
	{
		sliderObject.value = enabled ? 1.0f : 0.0f;
	}

	public void SwitchStateDidChange()
	{
		bool isEnabled = sliderObject.value > 0;

		if (isSoundsSwitcher)
			optionsManager.SetSoundsEnabled(isEnabled);

		if (isVibrateSwitcher)
		{
			optionsManager.SetVibrateEnabled(isEnabled);
			optionsManager.Vibrate();
		}
	
		UpdateState();
	}

	void UpdateState()
	{
		if (backImage != null)
			backImage.sprite = sliderObject.value > 0 ? backEnabledSprite : backDisabledSprite;

		if (buttonImage != null)
			buttonImage.sprite = sliderObject.value > 0 ? buttonEnabledSprite : buttonDisabledSprite;
	}
}
