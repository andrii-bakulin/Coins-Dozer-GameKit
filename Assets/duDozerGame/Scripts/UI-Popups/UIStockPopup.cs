//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIStockPopup : ExtMonoBehaviour
{
	public GameObject listItemPrefab;
	public GameObject listItemsHolder;

	void Start ()
	{
		listItemPrefab.SetActive(false);

		foreach (kObjectId prizeId in ObjectsManager.kAllPrizesList)
		{
			GameObject listItem = Instantiate(listItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			listItem.transform.SetParent(listItemsHolder.transform);
			listItem.transform.localScale = Vector3.one;
		
			listItem.GetComponent<UIStockPopupItem>().objectId = prizeId;
			listItem.SetActive(true);
		}

		RectTransform listItemsTrans = listItemsHolder.GetComponent<RectTransform>();
		listItemsTrans.sizeDelta = new Vector2(listItemsTrans.sizeDelta.x, 220 * (ObjectsManager.kAllPrizesList.Length + 0.5f));
		listItemsTrans.anchoredPosition = new Vector2(0, -400);
	}
}
