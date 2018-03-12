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

	public bool wobbleOn; // TODO: wobble

	public float sine;
	public float envelope = 0f;

	public float actualSize;

	public float envelopeIncrement = 0.02f;

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
		envelope = Mathf.Clamp(envelope - 0.067f * envelopeIncrement, 0f, envelopeIncrement);
		sine = Mathf.Sin (Time.frameCount / 1f) * envelope;
		size = sizeDesired * 0.6f + size * 0.4f;
		actualSize = scale * (size + sine);
		transform.localScale = new Vector2 (actualSize, actualSize);
	}

	void incrementEnvelope() {
		envelope = envelopeIncrement;
	}
}

// TODO: i used this for background: http://asbienestar.co/wood-floor-background-tumblr/wood-floor-background-tumblr-in-excellent-sek-woodbackground/