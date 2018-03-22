using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

	public Text cookiesText;
	public Text cookiesPerSecondText;

	private System.Threading.Timer timer;

	public long cookies = 0;
	public float cookiesFloat = 0f;

	public float cookiesPerClick = 1;
	public float cookiesPerClickMultiplier = 1f;
	public float cookiesPerClickAddOn = 0f;
	public float cookiesPerClickTotal;

	public float cookiesPerSecond = 0f;
	float oldCookiesPerSecond = 0f;

	float cookieAdd = 0f;

	int cookieAddPeriod = 25; // time between cookies updates in milliseconds

	void Start () {
		timer = new System.Threading.Timer (UpdateProperty);
		timer.Change (cookieAddPeriod, cookieAddPeriod);

		// TESTING
		cookiesFloat = 1000000f;
	}

	private void UpdateProperty(object state) {
		lock(this) {
			cookiesFloat += cookieAdd;
		}
	}

	void Update () {
		cookies = (int) (cookiesFloat + 0.5f);

		if (cookiesPerClickTotal != cookiesPerClick * cookiesPerClickMultiplier + cookiesPerClickAddOn) {
			cookiesPerClickTotal = cookiesPerClick * cookiesPerClickMultiplier + cookiesPerClickAddOn;
		}

		
		cookiesText.text = "Cookies: " + cookies;
		cookiesPerSecondText.text = "CpS: " + cookiesPerSecond;

		if (oldCookiesPerSecond != cookiesPerSecond && cookiesPerSecond != 0f) {
			cookieAdd = cookiesPerSecond / (1000f / cookieAddPeriod);
			oldCookiesPerSecond = cookiesPerSecond;
		}
	}
}