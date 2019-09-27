//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class BoardObjectScript : ExtMonoBehaviour
{
    // Require attach this GameObject to ObjectsManager > to appropriate object property
    [Header("!!! WARNING !!! Check comment in script")]

	public kObjectId objectId;

	public AudioClip sfxFallin;
	public AudioClip sfxFallout;

	AudioSource audioPlayer;

	bool isInFallingState;
	bool isFlyAwayMode;
	bool isIamZombie;
	float heavyBoomForceScale;

	public bool IsInFallingState
	{
		get { return isInFallingState; }
	}

	void Start()
	{
		audioPlayer = GetComponent<AudioSource>();

		if (audioPlayer == null)
			Debug.LogWarning("Failed to get AudioSource for BoardObjectScript");
	}

	public void Initialize(bool isInFallingState)
	{
		this.isInFallingState = isInFallingState;

		heavyBoomForceScale = 0.0f;

		isFlyAwayMode = false;

		if (isInFallingState)
		{
			switch (objectId)
			{
				case kObjectId.Coin2x:
					heavyBoomForceScale = 0.4f;
					break;

				case kObjectId.Coin5x:
					heavyBoomForceScale = 0.7f;
					break;

				case kObjectId.BonusChipGolden:
					heavyBoomForceScale = 1.0f;
					break;

				default:
					kObjectGroupdId objGroupId = ObjectsManager.GetObjectGroupId(objectId);

					if (objGroupId == kObjectGroupdId.PowerChip || objGroupId == kObjectGroupdId.BonusChip)
						heavyBoomForceScale = 0.5f;

					break;
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (isInFallingState)
		{
			// If I touch another BoardObject then need to check if 2nd object also in "FallingState"
			// then no need to "Quake". 2nd object should be in "idle" state!

			BoardObjectScript secondBoardObjectScript = collision.gameObject.GetComponent<BoardObjectScript>();

			if (secondBoardObjectScript != null && secondBoardObjectScript.IsInFallingState)
				return;

			// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

			isInFallingState = false;

			if (heavyBoomForceScale > 0.0f)
			{
				powerUpsManager.RunQuakeOnce(heavyBoomForceScale);
			}

			if (audioPlayer != null && sfxFallin != null)
				audioPlayer.PlayOneShot(sfxFallin);
		}
	}

	void Update()
	{
		if (isFlyAwayMode)
		{
			transform.localPosition += new Vector3(0, 7.0f * Time.deltaTime, 0);
			transform.Rotate(0, 90.0f * Time.deltaTime, 0, Space.World);
		}
		else if ( transform.localPosition.y < -1000.0f ) // it shoulde be really long distance!
		{
			// Silent Destroy
			boardManager.DropObjectDirect(gameObject);
			Destroy(gameObject);
		}
	}

	/**
	 * WARNING!
	 * This method should be call ONLY VIA BoardManager.DropObject !!!
	 * Because require to drop this object from active objects list in BoardManager !!!
	 */
	public void DestroyWithAnimation(BoardManager.FallAreaSide fallAreaSide)
	{
		if (fallAreaSide != BoardManager.FallAreaSide.FallSuccess)
		{
			Destroy(gameObject);
			return;
		}

		if (isIamZombie)
			return;

		kObjectGroupdId groupId = ObjectsManager.GetObjectGroupId(objectId);

		switch (groupId)
		{
			default:
			case kObjectGroupdId.Undefined:
			case kObjectGroupdId.Coin:
				isFlyAwayMode = false;
				break;

			case kObjectGroupdId.Prize:
			case kObjectGroupdId.BonusChip:
			case kObjectGroupdId.PowerChip:
				isFlyAwayMode = true;
				break;
		}

		Destroy(GetComponent<MeshCollider>());

		if (isFlyAwayMode)
		{
			Destroy(GetComponent<Rigidbody>());

			// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

			transform.localRotation = Quaternion.identity;

			if (groupId == kObjectGroupdId.BonusChip || groupId == kObjectGroupdId.PowerChip)
			{
				transform.Rotate(new Vector3(90, 180, 0));
			}

			// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
			// Normolize position to visible area:
			// X in range [-2.2 .. 2.2]
			// Y = -3.5
			// Z in range [6.3 ..  8.0]

			var pos = transform.localPosition;

			pos.x = Mathf.Max(-2.2f, Mathf.Min(+2.2f, pos.x));
			pos.y = -6.0f;
			pos.z = Mathf.Max(+6.3f, Mathf.Min(+8.0f, pos.x));

			transform.localPosition = pos;
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		if (audioPlayer != null && sfxFallout != null)
			audioPlayer.PlayOneShot(sfxFallout);

		Destroy(gameObject, 5.0f); // For 'animate==false' I still need wait for 5 sec to PlayFallout() sfx!
		isIamZombie = true;
	}
}
