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

	void OnMouseDown() {
		gameStats.cookiesDouble += gameStats.cookiesPerClickTotal;
		gameStats.handmadeCookiesDouble += gameStats.cookiesPerClickTotal;
	}

	void OnMouseUp() {
		onCookieClick = false;
	}

	void OnMouseOver() { // while mouse on me
		float newSizeDesired;
		if (Input.GetMouseButton (0)) {
			if (onCookieClick)
				newSizeDesired = 1.04f;
			else
				newSizeDesired = 0.98f; // 0.98
		} else {
			newSizeDesired = 1.067f; // 1.08f
		}
		if (newSizeDesired != sizeDesired)
			sineStart = Time.realtimeSinceStartup;
		sizeDesired = newSizeDesired;
	}

	void OnMouseExit() { // once when mouse leaves me
		if (Input.GetMouseButton (0))
			onCookieClick = true;
		sizeDesired = 1.01f;
	}

	void Update() {
		float expectedFrames = Time.deltaTime * expectedFrameRate;

		envelope = envelope * Mathf.Pow(envelopeEndMultiply, expectedFrames);

		float newSize = sizeDesired * changeSpeed + size * (1 - changeSpeed);
		envelope += newSize - size;
		sine = Mathf.Sin ((Time.realtimeSinceStartup - sineStart) / period) * envelope;
		size = newSize;
		actualSize = scale * (size + (wobbleOn ? sine : 0f));
		transform.localScale = new Vector2 (actualSize, actualSize);


//		StreamWriter writer = new StreamWriter("Assets/test.txt", true);
//		writer.WriteLine(Time.frameCount + ", " + size);
//		writer.Close();
	}
}

// TODO: i used this for background: http://asbienestar.co/wood-floor-background-tumblr/wood-floor-background-tumblr-in-excellent-sek-woodbackground/