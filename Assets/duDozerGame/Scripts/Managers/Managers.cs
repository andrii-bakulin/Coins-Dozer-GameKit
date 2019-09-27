//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class Managers : MonoBehaviour
{
	public static Managers GetManagersCore()
	{
		GameObject managers = GameObject.Find("/Managers");
		return managers ? managers.GetComponentInParent<Managers>() : null;
	}

	public static BalanceManager GetBalanceManager()
	{
		GameObject managers = GameObject.Find("/Managers/BalanceManager");
		return managers ? managers.GetComponentInParent<BalanceManager>() : null;
	}

	public static BoardManager GetBoardManager()
	{
		GameObject managers = GameObject.Find("/Managers/BoardManager");
		return managers ? managers.GetComponentInParent<BoardManager>() : null;
	}

	public static ObjectsManager GetObjectsManager()
	{
		GameObject managers = GameObject.Find("/Managers/ObjectsManager");
		return managers ? managers.GetComponentInParent<ObjectsManager>() : null;
	}

	public static OptionsManager GetOptionsManager()
	{
		GameObject managers = GameObject.Find("/Managers/OptionsManager");
		return managers ? managers.GetComponentInParent<OptionsManager>() : null;
	}

	public static PlayerManager GetPlayerManager()
	{
		GameObject managers = GameObject.Find("/Managers/PlayerManager");
		return managers ? managers.GetComponentInParent<PlayerManager>() : null;
	}

	public static PopupManager GetPopupManager()
	{
		GameObject managers = GameObject.Find("/Managers/PopupManager");
		return managers ? managers.GetComponentInParent<PopupManager>() : null;
	}

	public static PowerUpsManager GetPowerUpsManager()
	{
		GameObject managers = GameObject.Find("/Managers/PowerUpsManager");
		return managers ? managers.GetComponentInParent<PowerUpsManager>() : null;
	}

	public static ResourcesManager GetResourcesManager()
	{
		GameObject managers = GameObject.Find("/Managers/ResourcesManager");
		return managers ? managers.GetComponentInParent<ResourcesManager>() : null;
	}

	public static UpgradesManager GetUpgradesManager()
	{
		GameObject managers = GameObject.Find("/Managers/UpgradesManager");
		return managers ? managers.GetComponentInParent<UpgradesManager>() : null;
	}

	//------------------------------------------------------------------------------------------------------------------

	void Awake()
	{
		Application.targetFrameRate = 30;
	}

	//------------------------------------------------------------------------------------------------------------------

	public static bool isAllowSaveStates = true;

	public void Start()
	{
		// Auto save all states each 30 seconds
		InvokeRepeating("SaveGameStates", 30, 30);
	}

	public void SaveGameStates()
	{
		if (isAllowSaveStates == false)
			return;

		GetBoardManager().SaveStates(false);
		GetPlayerManager().SaveStates(false);

		PlayerPrefs.Save();
	}

	void OnApplicationPause(bool paused)
	{
		if (paused)
		{	// switch to background
			SaveGameStates();
		}
	}

	void OnApplicationQuit()
	{
		SaveGameStates();
	}
}
