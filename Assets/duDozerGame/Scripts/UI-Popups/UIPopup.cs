//
// Copyright (C) 2017 Duksel Corp. - All rights reserved.
//                    Andrii Bakulin <ab@duksel.com>
//

using UnityEngine;
using UnityEngine.UI;

public class UIPopup : ExtMonoBehaviour
{
	Color backgroundColor = new Color(0.05f, 0.05f, 0.05f, 0.6f);

	GameObject backgroundObject = null;

	public void Open()
	{
		AddBackground();
	}

	public void Close()
	{
		Destroy(backgroundObject);
		Destroy(gameObject);
	}

	void AddBackground()
	{
		var bgTex = new Texture2D(1, 1);
		bgTex.SetPixel(0, 0, backgroundColor);
		bgTex.Apply();

		backgroundObject = new GameObject("PopupBackgroundLayer");
		var image = backgroundObject.AddComponent<Image>();
		var rect = new Rect(0, 0, bgTex.width, bgTex.height);
		var sprite = Sprite.Create(bgTex, rect, new Vector2(0.5f, 0.5f), 1);
		image.material.mainTexture = bgTex;
		image.sprite = sprite;
		var newColor = image.color;
		image.color = newColor;
		image.canvasRenderer.SetAlpha(0.0f);
		image.CrossFadeAlpha(1.0f, 0.4f, false);

		var canvas = GameObject.Find("Canvas");
		backgroundObject.transform.localScale = new Vector3(1, 1, 1);
		backgroundObject.GetComponent<RectTransform>().sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
		backgroundObject.transform.SetParent(canvas.transform, false);
		backgroundObject.transform.SetSiblingIndex(transform.GetSiblingIndex());
	}
}
