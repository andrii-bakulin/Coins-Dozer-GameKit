//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class OptionsManager : ExtMonoBehaviour
{
	[Header("ANDROID Config")]
	public string AppRatePackageIdAndroid = "";
	public string DeveloperPageIdAndroid = "";

	[Header("iOS Config")]
	public string AppRateBundleIdIos = "";
	public string DeveloperPageIdIos = "";

	[Header("Global Config")]
	public string FacebookPageUrl = "";

	//------------------------------------------------------------------------------------------------------------------

	public string AppRateId
	{
		get
		{
#if UNITY_ANDROID
			return AppRatePackageIdAndroid;
#elif UNITY_IOS
			return AppRateBundleIdIos;
#else
			return "";
#endif
		}
	}

	public string DeveloperPageId
	{
		get
		{
#if UNITY_ANDROID
			return DeveloperPageIdAndroid;
#elif UNITY_IOS
			return DeveloperPageIdIos;
#else
			return "";
#endif
		}
	}

	//------------------------------------------------------------------------------------------------------------------

	bool isSoundsEnabled;
	bool isVibrateEnabled;

	void Start()
	{
		isSoundsEnabled = PlayerPrefs.GetInt("Options:SoundsEnabled", 1) > 0;
		isVibrateEnabled = PlayerPrefs.GetInt("Options:VibrateEnabled", 1) > 0;

		ApplySoundsEnabled();
	}

	//------------------------------------------------------------------------------------------------------------------

	public void OpenMoreApps()
	{
		if (DeveloperPageId == "")
			return;

#if UNITY_ANDROID
		Application.OpenURL("http://play.google.com/store/apps/dev?id=" + DeveloperPageId);
#elif UNITY_IOS
		Application.OpenURL("itms-apps://itunes.apple.com/artist/id" + DeveloperPageId);
#else
		Application.OpenURL(DeveloperPageId);
#endif
	}

	public void OpenRateApp()
	{
		if (AppRateId == "")
			return;

		OpenRateAppByAppId(AppRateId);
	}

	public void OpenRateAppByAppId(string appRatePageId)
	{
		if (appRatePageId == "")
			return;

#if UNITY_ANDROID
		Application.OpenURL("market://details?id=" + appRatePageId);
#elif UNITY_IOS
		Application.OpenURL("itms-apps://itunes.apple.com/app/id" + appRatePageId);
#else
		Application.OpenURL(appRatePageId);
#endif
	}

	public void OpenFacebookPage()
	{
		if (FacebookPageUrl == "")
			return;

		Application.OpenURL(FacebookPageUrl);
	}

	//------------------------------------------------------------------------------------------------------------------

	public void ApplySoundsEnabled()
	{
		AudioListener.volume = IsSoundsEnabled() ? 1.0f : 0.0f;
	}

	public bool IsSoundsEnabled()
	{
		return isSoundsEnabled;
	}

	public void SetSoundsEnabled(bool enabled)
	{
		isSoundsEnabled = enabled;

		PlayerPrefs.SetInt("Options:SoundsEnabled", (enabled ? 1 : 0));
		PlayerPrefs.Save();

		ApplySoundsEnabled();
	}

	//------------------------------------------------------------------------------------------------------------------

	public bool IsVibrateEnabled()
	{
		return isVibrateEnabled;
	}

	public void SetVibrateEnabled(bool enabled)
	{
		isVibrateEnabled = enabled;

		PlayerPrefs.SetInt("Options:VibrateEnabled", (enabled ? 1 : 0));
		PlayerPrefs.Save();
	}

	public void Vibrate()
	{
		if (IsVibrateEnabled() == false)
			return;

#if !UNITY_WEBGL
		Handheld.Vibrate();
#endif
	}
}
