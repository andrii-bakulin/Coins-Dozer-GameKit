//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class FallAreaScript : ExtMonoBehaviour
{
	public BoardManager.FallAreaSide fallAreaSide;

	void OnTriggerEnter(Collider other)
	{
		boardManager.ObjectDidFall(other.gameObject, fallAreaSide);
	}
}
