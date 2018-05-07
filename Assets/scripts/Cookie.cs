using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Cookie : MonoBehaviour {

	Transform transfrom;
	public GameStats gameStats;

	public float v = 1f;
	public bool onCookieClick;
	public float sizeDesired = 1f;
	public float size = 1f;

	public bool wobbleOn = true;

	public float sine;
	public float envelope = 0f;

	public float actualSize;

	public float envelopeEndMultiply = 0.9125f;

	public float period = 0.04375f;

	float sineStart = 0f;

	public float scale;

	float expectedFrameRate = 50f;

	public float changeSpeed = 0.5f;

	public Camera m_OrthographicCamera;

	public float sizeD = 0f;

	void OnMouseDown() {
		gameStats.cookies += gameStats.cookiesPerClickTotal;
		gameStats.handmadeCookies += gameStats.cookiesPerClickTotal;
	}

	void OnMouseUp() {
		onCookieClick = false;
	}

	void OnMouseOver() { // while mouse on me
		float newSizeDesired = Input.GetMouseButton (0) && !onCookieClick ? 0.98f : 1.05f;
		if (newSizeDesired != sizeDesired)
			sineStart = Time.realtimeSinceStartup;
		sizeDesired = newSizeDesired;
	}

	void OnMouseExit() { // once when mouse leaves me
		if (Input.GetMouseButton (0))
			onCookieClick = true;
		sizeDesired = 1.0f;
	}

	void Update() {
		float expectedFrames = Time.deltaTime * expectedFrameRate;

		envelope = envelope * Mathf.Pow(envelopeEndMultiply, expectedFrames);

		float newSize = sizeDesired * changeSpeed + size * (1 - changeSpeed);
		envelope += newSize - size;
		sine = Mathf.Sin ((Time.realtimeSinceStartup - sineStart) / period) * envelope;
		size = newSize;
		actualSize = scale * (size + (wobbleOn ? sine : 0f));

		RectTransform myRectTransform = GetComponent<RectTransform>();
		myRectTransform.localScale = new Vector2 (actualSize, actualSize);
	

		// this is orteil's method
//		sizeD += (sizeDesired - size) * 0.75f;
//		sizeD *= 0.75f;
//		size += sizeD;
//		size = Mathf.Max (0.1f, size);
//		transform.localScale = new Vector2 (scale * size, scale * size);
	}

	void Start() {
//		float x = Screen.width;
//		float y = Screen.height;
//		float myX = x * (-6.65f / 882f);
//
//		RectTransform myRectTransform = GetComponent<RectTransform>();
//		myRectTransform.localPosition = new Vector3(myX, transform.position.y, -1f);
	}
}



// TODO: i used this for background: http://asbienestar.co/wood-floor-background-tumblr/wood-floor-background-tumblr-in-excellent-sek-woodbackground/