//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class PusherScript : ExtMonoBehaviour
{
	const float kSpeedPerSec = 1.5f;

	bool isPushDirection = true;

	float curOffset;
	float minOffset;
	float maxOffset;
	float maxOffsetDefault = 3.0f;

	Vector3 zeroOffset;

	void Start()
	{
		zeroOffset = new Vector3(0.0f, 0.2f, -5.0f);

		minOffset = 0.1f;
		maxOffset = maxOffsetDefault;

		curOffset = minOffset;
	}
	
	void Update ()
	{
		if (isPushDirection)
		{
			curOffset += Time.deltaTime * kSpeedPerSec;

			if (curOffset >= maxOffset)
			{
				curOffset = maxOffset;
				isPushDirection = false;
			}
		}
		else
		{
			curOffset -= Time.deltaTime * kSpeedPerSec;

			if (curOffset <= minOffset)
			{
				curOffset = minOffset;
				maxOffset = maxOffsetDefault;
				isPushDirection = true;
			}
		}

		this.GetComponent<Rigidbody>().MovePosition( zeroOffset + new Vector3(0, 0, curOffset) );
	}

	public bool ApplyMegaDozer(float bonusOffset)
	{
		maxOffset += bonusOffset;
		isPushDirection = true;

		return true;
	}
}
