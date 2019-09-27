//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class PopupManager : ExtMonoBehaviour
{
	[Header("References UI Popups")]
	public GameObject popupCoinOfflineReport;
	public GameObject popupCoinTimer;
	public GameObject popupLevelUp;
	public GameObject popupMainMenu;
	public GameObject popupPowerUps;
	public GameObject popupPrizes;
	public GameObject popupProfile;
	public GameObject popupStock;
	public GameObject popupUpgrades;
	public GameObject popupUpgradeDetails;

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

	protected GameObject OpenPopupByPopupObject(GameObject popupObject)
	{
		var popup = Instantiate(popupObject) as GameObject;
		popup.SetActive(true);

		Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

		popup.transform.SetParent(canvas.transform, false);
		popup.GetComponent<UIPopup>().Open();

		return popup;
	}

	public static bool IsAnyUIPopupVisible()
	{
		return GameObject.Find("/Canvas/PopupBackgroundLayer") != null;
	}

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

	string coinOfflineReport_userMessage;

	public string CoinOfflineReport_userMessage
	{
		get { return coinOfflineReport_userMessage; }
		set { coinOfflineReport_userMessage = value; }
	}

	public void OpenPopup_CoinOfflineReport(string userMessage)
	{
		CoinOfflineReport_userMessage = userMessage;

		OpenPopupByPopupObject(popupCoinOfflineReport);
	}

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

	public void OpenPopup_CoinTimer()
	{
		OpenPopupByPopupObject(popupCoinTimer);
	}

	public void OpenPopup_LevelUp()
	{
		OpenPopupByPopupObject(popupLevelUp);
	}

	public void OpenPopup_MainMenu()
	{
		OpenPopupByPopupObject(popupMainMenu);
	}

	public void OpenPopup_PowerUps()
	{
		OpenPopupByPopupObject(popupPowerUps);
	}

	public void OpenPopup_Prizes()
	{
		OpenPopupByPopupObject(popupPrizes);
	}

	public void OpenPopup_Profile()
	{
		OpenPopupByPopupObject(popupProfile);
	}

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

	public void OpenPopup_Stock()
	{
		OpenPopupByPopupObject(popupStock);
	}

	public void OpenPopup_Upgrades()
	{
		OpenPopupByPopupObject(popupUpgrades);
	}

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

	kUpgradeId upgradeDetails_upgradeId;

	public kUpgradeId UpgradeDetails_upgradeId
	{
		get { return upgradeDetails_upgradeId; }
		set { upgradeDetails_upgradeId = value; }
	}

	public void OpenPopup_UpgradeDetails(kUpgradeId upgradeId)
	{
		UpgradeDetails_upgradeId = upgradeId;
		OpenPopupByPopupObject(popupUpgradeDetails);
	}
}
