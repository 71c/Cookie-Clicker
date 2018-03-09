using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour {

	Transform transfrom;
	public GameStats gameStats;

	public float v = 1f;
	public bool onCookieClick;
	public float sizeDesired = 0.5f;
	public float size = 0.5f;

	public bool wobbleOn; // TODO: wobble

	void OnMouseDown() {
		gameStats.score += gameStats.pointsPerClick;
	}

	void OnMouseUp() {
		onCookieClick = false;
	}

	void OnMouseOver() {
		if (Input.GetMouseButton (0)) {
			if (onCookieClick)
				sizeDesired = 0.52f;
			else
				sizeDesired = 0.48f;
		} else {
			sizeDesired = 0.52f;
		}
	}

	void OnMouseExit() {
		if (Input.GetMouseButton (0))
			onCookieClick = true;
		sizeDesired = 0.5f;
	}

	void Update() {
		size = sizeDesired * 0.6f + size * 0.4f;
		transform.localScale = new Vector2 (size, size);
	}
}