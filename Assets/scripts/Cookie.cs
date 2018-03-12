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

	public float envelopeIncrement = 0.055f; // it seems that this number doesn't matter

//	public float envelopeEndSpeed = 0.8f; // 0.067f; // 0.05525f; // 0.0435f; // 0.02f; // 0.067f;
	public float envelopeEndMultiply = 0.875f;

	public float period = 1.25f;

//	float sineStartingFramecount; // TODO maybe if i somehow get the game good enough for me to need to worry about that

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
		//		envelope += 1f;
		if (Input.GetMouseButton (0)) {
			if (onCookieClick)
				sizeDesired = 1.04f;
			else
				sizeDesired = 0.96f;
		} else {
			sizeDesired = 1.1f;
		}
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
		envelope = Mathf.Clamp(envelope * envelopeEndMultiply, 0f, envelopeIncrement);

		sine = Mathf.Sin (Time.frameCount / period) * envelope;
		size = sizeDesired * 0.6f + size * 0.4f;
		actualSize = scale * (size + sine);
		transform.localScale = new Vector2 (actualSize, actualSize);
	}

	void incrementEnvelope() {
		envelope = envelopeIncrement;
	}
}

// TODO: i used this for background: http://asbienestar.co/wood-floor-background-tumblr/wood-floor-background-tumblr-in-excellent-sek-woodbackground/