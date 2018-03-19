using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cookie : MonoBehaviour {

	Transform transfrom;
	public GameStats gameStats;

	public float v = 1f;
	public bool onCookieClick;
	public float sizeDesired = 1f;
	public float size = 1f;

	public bool wobbleOn; // TODO: wobble on/off

	public float sine;
	public float envelope = 0f;

	public float actualSize;

	public float envelopeIncrement = 0.055f;

//	public float envelopeEndSpeed = 0.8f; // 0.067f; // 0.05525f; // 0.0435f; // 0.02f; // 0.067f;
	public float envelopeEndMultiply = 0.9125f;

	public float period = 0.04375f;

	float sineStart = 0f; // TODO maybe if i somehow get the game good enough for me to need to worry about that

	public float scale;

	void OnMouseDown() {
		gameStats.score += gameStats.pointsPerClick;
		incrementEnvelope ();
	}

	void OnMouseUp() {
		onCookieClick = false;
		incrementEnvelope ();
	}

	void OnMouseOver() { // while mouse on me
		float newSizeDesired;
		if (Input.GetMouseButton (0)) {
			if (onCookieClick)
				newSizeDesired = 1.04f;
			else
				newSizeDesired = 0.98f;
		} else {
			newSizeDesired = 1.08f;
		}
		if (newSizeDesired != sizeDesired)
			sineStart = Time.realtimeSinceStartup;
		if (newSizeDesired < sizeDesired)
			envelope = -Mathf.Abs (envelope);
		sizeDesired = newSizeDesired;
	}

	void OnMouseExit() { // once when mouse leaves me
		incrementEnvelope();
		if (Input.GetMouseButton (0))
			onCookieClick = true;
		sizeDesired = 1f;
	}

	void OnMouseEnter() { // once when mouse enters me
		incrementEnvelope();
	}

	void Update() {
//		envelope = Mathf.Clamp(envelope - envelopeEndSpeed * envelopeIncrement, 0f, envelopeIncrement);
		envelope = Mathf.Clamp(envelope * Mathf.Pow(envelopeEndMultiply, Time.deltaTime / (1f / 50f)), -envelopeIncrement, envelopeIncrement);

		sine = Mathf.Sin ((Time.realtimeSinceStartup - sineStart) / period) * envelope;
		size = sizeDesired * 0.5f + size * 0.5f;
		actualSize = scale * (size + sine);
		transform.localScale = new Vector2 (actualSize, actualSize);
	}

	void incrementEnvelope() {
		envelope = envelopeIncrement;
	}
}

// TODO: i used this for background: http://asbienestar.co/wood-floor-background-tumblr/wood-floor-background-tumblr-in-excellent-sek-woodbackground/