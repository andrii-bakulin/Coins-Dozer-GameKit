//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class PowerUpsManager : ExtMonoBehaviour
{
	public Camera mainCamera;

	int mainCameraIsFocusing; // 1=focus; 0=idle; -1=rollback;
	float mainCameraFocusLimit;
	float mainCameraFocusDelta; // per-second

	public GameObject shieldObjectLeft;
	public GameObject shieldObjectRight;
	public GameObject pusherObject;

	public AudioClip clipPowerUpCashfall;
	public AudioClip clipPowerUpMegaDozer;
	public AudioClip clipPowerUpQuake;
	public AudioClip clipPowerUpShield;
	public AudioClip clipPowerUpShieldOut;

	Animator shieldAnimatorLeft;
	Animator shieldAnimatorRight;
	PusherScript pusherScript;

	AudioSource playerPowerUpCashfall;
	AudioSource playerPowerUpMegaDozer;
	AudioSource playerPowerUpQuake;
	AudioSource playerPowerUpShield;
	AudioSource playerPowerUpShieldOut;

	bool isPowerUpActiveCashfall;
	bool isPowerUpActiveMegaDozer;
	bool isPowerUpActiveQuake;
	bool isPowerUpActiveShield;

	void Awake()
	{
		shieldAnimatorLeft = shieldObjectLeft.GetComponent<Animator>();
		shieldAnimatorRight = shieldObjectRight.GetComponent<Animator>();

		pusherScript = pusherObject.GetComponent<PusherScript>();
	}

	void Start()
	{
		isPowerUpActiveCashfall = false;
		isPowerUpActiveMegaDozer = false;
		isPowerUpActiveQuake = false;
		isPowerUpActiveShield = false;

		playerPowerUpCashfall = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		playerPowerUpCashfall.playOnAwake = false;
		playerPowerUpCashfall.clip = clipPowerUpCashfall;

		playerPowerUpMegaDozer = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		playerPowerUpMegaDozer.playOnAwake = false;
		playerPowerUpMegaDozer.clip = clipPowerUpMegaDozer;

		playerPowerUpQuake = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		playerPowerUpQuake.playOnAwake = false;
		playerPowerUpQuake.clip = clipPowerUpQuake;

		playerPowerUpShield = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		playerPowerUpShield.playOnAwake = false;
		playerPowerUpShield.clip = clipPowerUpShield;
	
		playerPowerUpShieldOut = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		playerPowerUpShieldOut.playOnAwake = false;
		playerPowerUpShieldOut.clip = clipPowerUpShieldOut;

		mainCameraIsFocusing = 0;
	}

	void Update()
	{
		if (mainCameraIsFocusing == 1)
		{
			if (mainCamera.fieldOfView > mainCameraFocusLimit )
			{
				mainCamera.fieldOfView -= mainCameraFocusDelta * Time.deltaTime;
			}
			else
			{
				mainCamera.fieldOfView = mainCameraFocusLimit;
				mainCameraIsFocusing = -1;
			}
		}
		else if (mainCameraIsFocusing == -1)
		{
			if (mainCamera.fieldOfView < 60.0f)
			{
				mainCamera.fieldOfView += mainCameraFocusDelta * Time.deltaTime / 2.0f;
			}
			else
			{
				mainCamera.fieldOfView = 60.0f;
				mainCameraIsFocusing = 0;
			}
		}

		ShieldUpdate();
	}
	
	//==================================================================================================================

	public static kPowerUpType GetRandomPowerUpType()
	{
		switch (Random.Range(0, 4))
		{
			default:
			case 0: return kPowerUpType.Cashfall;
			case 1: return kPowerUpType.MegaDozer;
			case 2: return kPowerUpType.Quake;
			case 3: return kPowerUpType.Shield;
		}
	}

	public void RunByUserActivePowerUp()
	{
		kPowerUpStrength strenght = playerManager.GetPowerUpStrength();

		if (strenght == kPowerUpStrength.None)
			return;

		switch (playerManager.PowerUpActiveType)
		{
			case kPowerUpType.Cashfall:
				if (RunCashfall(strenght) == false)
					return;
				break;
			
			case kPowerUpType.MegaDozer:
				if (RunMegaDozer(strenght) == false)
					return;
				break;
			
			case kPowerUpType.Quake:
				if (RunQuake(strenght) == false)
					return;
				break;
			
			case kPowerUpType.Shield:
				if (RunShield(strenght) == false)
					return;
				break;
		}

		playerManager.ResetPowerUpState();
	}

	//==================================================================================================================
	// Cashfall

	public void RunCashfallInstant()
	{
		if (isPowerUpActiveCashfall)
			return;

		if( playerManager.DecPowerUpAmount(1) == false )
			return;

		RunCashfall(kPowerUpStrength.Max);
	}

	public void RunCashfallOnAppLaunch()
	{
		RunCashfall(kPowerUpStrength.Max);
	}

	public bool RunCashfall(kPowerUpStrength strength)
	{
		if (isPowerUpActiveCashfall)
			return false;

		int coinsAmount = balanceManager.GetPowerUpCashfallCoinsAmount(strength);

		for (int i = 0; i < coinsAmount; i++)
		{
			boardManager.AddRandomCoin(new Vector3(0, i * 0.5f, 0));
		}

		isPowerUpActiveCashfall = true;
		Invoke("CashfallComplete", 1.0f);

		playerPowerUpCashfall.Play();

		return true;
	}

	void CashfallComplete()
	{
		isPowerUpActiveCashfall = false;
	}

	//==================================================================================================================
	// MegaDozer

	public void RunMegaDozerInstant()
	{
		if (isPowerUpActiveMegaDozer)
			return;

		if( playerManager.DecPowerUpAmount(1) == false )
			return;

		RunMegaDozer(kPowerUpStrength.Max);
	}

	public bool RunMegaDozer(kPowerUpStrength strength)
	{
		if (isPowerUpActiveMegaDozer)
			return false;

		float bonusLength = balanceManager.GetPowerUpMegaDozerBonusLength(strength);

		if (bonusLength < 0.5f)
			return false;

		pusherScript.ApplyMegaDozer(bonusLength);

		isPowerUpActiveMegaDozer = true;
		Invoke("MegaDozerComplete", 4.0f);

		playerPowerUpMegaDozer.Play();

		mainCameraFocusLimit = 60.0f - 15.0f;
		mainCameraFocusDelta = 5.0f;
		mainCameraIsFocusing = 1;

		return true;
	}

	void MegaDozerComplete()
	{
		isPowerUpActiveMegaDozer = false;
	}

	//==================================================================================================================
	// Quake

	public void RunQuakeInstant()
	{
		if (isPowerUpActiveQuake)
			return;

		if( playerManager.DecPowerUpAmount(1) == false )
			return;

		RunQuake(kPowerUpStrength.Max);
	}

	public bool RunQuake(kPowerUpStrength strength)
	{
		if (isPowerUpActiveQuake)
			return false;

		int powerUpIteration = balanceManager.GetPowerUpQuakeCounts(strength);

		for (int i = 0; i < powerUpIteration; i++)
		{
			if( i==0 )
				RunQuakeOnce();
			else
				Invoke("RunQuakeOnce", i);
		}

		isPowerUpActiveQuake = true;
		Invoke("QuakeComplete", powerUpIteration + 1);

		return true;
	}

	void QuakeComplete()
	{
		isPowerUpActiveQuake = false;
	}

	public void RunQuakeOnce()
	{
		RunQuakeOnce(1.0f);
	}

	public void RunQuakeOnce(float forceScale)
	{
		float deltaFocus = 3.0f * forceScale;

		mainCameraFocusLimit = 60.0f - deltaFocus;
		mainCameraFocusDelta = deltaFocus * 5.0f;
		mainCameraIsFocusing = 1;

		foreach (var boardObject in boardManager.objectsOnBoard)
		{
			BoardObjectScript boardScript = boardObject.GetComponent<BoardObjectScript>();

			if (boardScript != null && boardScript.IsInFallingState)
				continue; // no need to Quake "falling" objects!

			boardObject.GetComponent<Rigidbody>().AddForceAtPosition(balanceManager.GetPowerUpQuakeForce() * forceScale,
																	 boardObject.transform.position + Random.insideUnitSphere * 0.25f,
																	 ForceMode.Impulse);
		}

		if (forceScale >= 0.7f)
		{
			optionsManager.Vibrate();
			playerPowerUpQuake.Play();
		}
	}

	//==================================================================================================================
	// Shield

	public void RunShieldInstante()
	{
		if (isPowerUpActiveShield)
			return;

		if( playerManager.DecPowerUpAmount(1) == false )
			return;

		RunShield(kPowerUpStrength.Max);
	}

	float powerUpShieldDurationLeft = 0.0f;

	public bool RunShield(kPowerUpStrength strength)
	{
		if (isPowerUpActiveShield)
			return false;

		powerUpShieldDurationLeft = balanceManager.GetPowerUpShieldDuration(strength);

		shieldAnimatorLeft.SetBool("IsActive", true);
		shieldAnimatorRight.SetBool("IsActive", true);

		isPowerUpActiveShield = true;

		playerPowerUpShield.Play();

		return true;
	}

	void ShieldUpdate()
	{
		if (isPowerUpActiveShield == false)
			return;

		powerUpShieldDurationLeft -= Time.deltaTime;

		if (powerUpShieldDurationLeft > 0.0f)
			return;

		shieldAnimatorLeft.SetBool("IsActive", false);
		shieldAnimatorRight.SetBool("IsActive", false);

		powerUpShieldDurationLeft = 0.0f;
		isPowerUpActiveShield = false;

		playerPowerUpShieldOut.Play();
	}
}
