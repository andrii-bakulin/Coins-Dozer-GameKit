//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class HlpConvert : ExtMonoBehaviour
{
	public static string SecondsToFriendlyLongTime(int seconds)
	{
		string res = "";

		if (seconds >= 60)
		{
			int minutes = seconds / 60;
			res = minutes.ToString() + " min ";
		}

		seconds %= 60;

		res += (seconds < 10 ? "0" : "") + seconds.ToString() + " sec";

		return res;
	}

	public static string SecondsToFriendlyTime(int seconds)
	{
		int minutes = seconds / 60;
		seconds %= 60;

		return minutes.ToString() + ":" + (seconds < 10 ? "0" : "") + seconds.ToString();
	}
}
