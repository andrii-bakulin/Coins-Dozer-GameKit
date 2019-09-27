//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIMainMenuPopup : ExtMonoBehaviour
{
	public UISwitcher soundsSwitcher;
	public UISwitcher vibrateSwitcher;

	public GameObject facebookButton;
	public GameObject moreGamesButton;
	public GameObject rateGameButton;

	void Start()
	{
		soundsSwitcher.InitState(optionsManager.IsSoundsEnabled());
		vibrateSwitcher.InitState(optionsManager.IsVibrateEnabled());

		facebookButton.SetActive( optionsManager.FacebookPageUrl != "");
		moreGamesButton.SetActive( optionsManager.DeveloperPageId != "");
		rateGameButton.SetActive( optionsManager.AppRateId != "" );
	}

	public void ClickOnFacebook()
	{
		optionsManager.OpenFacebookPage();
	}

	public void ClickOnMoreGames()
	{
		optionsManager.OpenMoreApps();
	}

	public void ClickOnRateUs()
	{
		optionsManager.OpenRateApp();
	}
}
