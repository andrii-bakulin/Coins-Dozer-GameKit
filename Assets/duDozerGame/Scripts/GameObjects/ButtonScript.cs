//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonScript : ExtMonoBehaviour, IPointerDownHandler
{
	public bool VisibleOnlyIfRwVideoReady = false;
	public bool VisibleOnlyIfShopEnabled = false;

	public int VisibleOnlyIfUserHasGems = 0;
	public int VisibleOnlyIfUserHasPowerUps = 0;

	RectTransform rectTransform;
	Vector3 scaleVisible;
	Vector3 scaleInvisible;

	//------------------------------------------------------------------------------------------------------------------

	void Start()
	{
		// Start UpdateCheckActivity only if required
		if (VisibleOnlyIfRwVideoReady || VisibleOnlyIfShopEnabled || VisibleOnlyIfUserHasGems > 0 || VisibleOnlyIfUserHasPowerUps > 0)
		{
			rectTransform = GetComponent<RectTransform>();

			scaleVisible = rectTransform.localScale;
			scaleInvisible = new Vector3(0.001f, 0.001f, 0.001f);

			UpdateCheckActivity();
		}
	}

	//------------------------------------------------------------------------------------------------------------------

	[Serializable]
	public class ButtonClickedEvent : UnityEvent { }

	[SerializeField]
	private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

	public ButtonClickedEvent onClick
	{
		get { return m_OnClick; }
		set { m_OnClick = value; }
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;

		m_OnClick.Invoke();
	}

	//------------------------------------------------------------------------------------------------------------------

	void UpdateCheckActivity()
	{
		bool isVisible = true;

		if (VisibleOnlyIfUserHasGems > 0)
		{
			isVisible = playerManager.GemsAmount >= VisibleOnlyIfUserHasGems;
		}
		else if (VisibleOnlyIfUserHasPowerUps > 0)
		{
			isVisible = playerManager.PowerUpsAmount >= VisibleOnlyIfUserHasPowerUps;
		}

		rectTransform.localScale = isVisible ? scaleVisible : scaleInvisible;

		Invoke("UpdateCheckActivity", 0.2f);
	}
}
