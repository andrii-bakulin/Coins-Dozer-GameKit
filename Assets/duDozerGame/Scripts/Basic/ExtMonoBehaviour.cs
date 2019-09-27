//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class ExtMonoBehaviour : MonoBehaviour
{
	//------------------------------------------------------------------------------------------------------------------
	// Lazy init properties

	private BalanceManager _balanceManager = null;
	private BoardManager _boardManager = null;
	private ObjectsManager _objectsManager = null;
	private OptionsManager _optionsManager = null;
	private PlayerManager _playerManager = null;
	private PopupManager _popupManager = null;
	private PowerUpsManager _powerUpsManager = null;
	private ResourcesManager _resourcesManager = null;
	private UpgradesManager _upgradesManager = null;

	//------------------------------------------------------------------------------------------------------------------

	protected BalanceManager balanceManager
	{
		get
		{
			if (_balanceManager == null)
				_balanceManager = Managers.GetBalanceManager();

			return _balanceManager;
		}
	}

	protected BoardManager boardManager
	{
		get
		{
			if (_boardManager == null)
				_boardManager = Managers.GetBoardManager();

			return _boardManager;
		}
	}

	protected ObjectsManager objectsManager
	{
		get
		{
			if (_objectsManager == null)
				_objectsManager = Managers.GetObjectsManager();

			return _objectsManager;
		}
	}

	protected OptionsManager optionsManager
	{
		get
		{
			if (_optionsManager == null)
				_optionsManager = Managers.GetOptionsManager();

			return _optionsManager;
		}
	}

	protected PlayerManager playerManager
	{
		get
		{
			if (_playerManager == null)
				_playerManager = Managers.GetPlayerManager();

			return _playerManager;
		}
	}

	protected PopupManager popupManager
	{
		get
		{
			if (_popupManager == null)
				_popupManager = Managers.GetPopupManager();

			return _popupManager;
		}
	}

	protected PowerUpsManager powerUpsManager
	{
		get
		{
			if (_powerUpsManager == null)
				_powerUpsManager = Managers.GetPowerUpsManager();

			return _powerUpsManager;
		}
	}

	protected ResourcesManager resourcesManager
	{
		get
		{
			if (_resourcesManager == null)
				_resourcesManager = Managers.GetResourcesManager();

			return _resourcesManager;
		}
	}

	protected UpgradesManager upgradesManager
	{
		get
		{
			if (_upgradesManager == null)
				_upgradesManager = Managers.GetUpgradesManager();

			return _upgradesManager;
		}
	}
}
