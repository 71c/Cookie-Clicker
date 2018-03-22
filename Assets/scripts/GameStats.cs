using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

	public Text cookiesText;
	public Text cookiesPerSecondText;

	private System.Threading.Timer timer;

	public int cookies = 0;

	public float cookiesPerClick = 1;
	public float cookiesPerClickMultiplier = 1f;
	public float cookiesPerClickAddOn = 0f;
	public float cookiesPerClickTotal;

	public float cookiesPerSecond = 0f;
	float oldCookiesPerSecond = 0f;


	void Start () {
		timer = new System.Threading.Timer (UpdateProperty);

		// TESTING
//		cookies = 100000;
	}

	private void UpdateProperty(object state) {
		lock(this) {
			cookies += 1;
		}
	}

	void Update () {
		if (cookiesPerClickTotal != cookiesPerClick * cookiesPerClickMultiplier + cookiesPerClickAddOn) {
			cookiesPerClickTotal = cookiesPerClick * cookiesPerClickMultiplier + cookiesPerClickAddOn;
		}

		
		cookiesText.text = "Cookies: " + cookies;
		cookiesPerSecondText.text = "CpS: " + cookiesPerSecond;

		if (oldCookiesPerSecond != cookiesPerSecond && cookiesPerSecond != 0f) {
			timer.Change ((int)(1000f / cookiesPerSecond), (int)(1000f / cookiesPerSecond));
			oldCookiesPerSecond = cookiesPerSecond;
		}
	}
}