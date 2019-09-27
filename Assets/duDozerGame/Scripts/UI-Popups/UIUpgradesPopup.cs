//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;

public class UIUpgradesPopup : ExtMonoBehaviour
{
	public GameObject listHeaderChipsPrefab;
	public GameObject listHeaderPowerUpsPrefab;
	public GameObject listHeaderSpecsPrefab;

	public GameObject listItemPrefab;
	public GameObject listItemsHolder;

	void Start()
	{
		listHeaderChipsPrefab.SetActive(false);
		listHeaderPowerUpsPrefab.SetActive(false);
		listHeaderSpecsPrefab.SetActive(false);
		listItemPrefab.SetActive(false);

		Color itemBackColor = Color.white;

		foreach (kUpgradeId upgradeId in UpgradesManager.kAllUpgradeIds)
		{
			if (upgradeId == kUpgradeId.ChipBasicCoin)
			{
				GameObject listHeader = Instantiate(listHeaderChipsPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				listHeader.transform.SetParent(listItemsHolder.transform);
				listHeader.transform.localScale = Vector3.one;
				listHeader.SetActive(true);

				itemBackColor = (Color)new Color32(255, 220, 120, 107);
			}
			else if (upgradeId == kUpgradeId.PowerUpCashfall)
			{
				GameObject listHeader = Instantiate(listHeaderPowerUpsPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				listHeader.transform.SetParent(listItemsHolder.transform);
				listHeader.transform.localScale = Vector3.one;
				listHeader.SetActive(true);

				itemBackColor = (Color)new Color32(253, 122, 159, 80);
			}
			else if (upgradeId == kUpgradeId.SpecImproveChipsAttrs)
			{
				GameObject listHeader = Instantiate(listHeaderSpecsPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				listHeader.transform.SetParent(listItemsHolder.transform);
				listHeader.transform.localScale = Vector3.one;
				listHeader.SetActive(true);

				itemBackColor = (Color)new Color32(41, 167, 210, 50);
			}

			GameObject listItem = Instantiate(listItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			listItem.transform.SetParent(listItemsHolder.transform);
			listItem.transform.localScale = Vector3.one;

			UIUpgradesPopupItem upgradesPopupItem = listItem.GetComponent<UIUpgradesPopupItem>();
			upgradesPopupItem.upgradeId = upgradeId;
			upgradesPopupItem.backgroundImage.color = itemBackColor;

			listItem.SetActive(true);
		}

		RectTransform listItemsTrans = listItemsHolder.GetComponent<RectTransform>();
		listItemsTrans.sizeDelta = new Vector2( listItemsTrans.sizeDelta.x, 190 * (UpgradesManager.kAllUpgradeIds.Length + 3 + 0.5f));

		listItemsHolder.GetComponent<RectTransform>().anchoredPosition = new Vector2(-117.0f, -950.0f);
	}
}
