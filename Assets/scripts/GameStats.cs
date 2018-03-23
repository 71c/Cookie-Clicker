using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

	public Text cookiesText;
	public Text cookiesPerSecondText;

	private System.Threading.Timer timer;

	public double cookiesDouble = 0.0;
	public long cookies = 0;
	public double handmadeCookiesDouble = 0.0;
	public long handmadeCookies = 0;

	public double cookiesPerClick = 1;
	public double cookiesPerClickMultiplier = 1.0;
	public double cookiesPerClickAddOn = 0.0;
	public double cookiesPerClickTotal;

	public double cookiesPerSecond = 0.0;

	public double cookiesPerSecondMultiplier = 1.0;
	public double cookiesPerSecondTotal = 0.0;
	double oldCookiesPerSecondTotal = 0.0;

	double cookieAdd = 0.0;

	int cookieAddPeriod = 25; // time between cookies updates in milliseconds

	void Start () {
		timer = new System.Threading.Timer (UpdateProperty);
		timer.Change (cookieAddPeriod, cookieAddPeriod);

		// TESTING
		cookiesDouble = 1000000000000000000.0;
	}

	private void UpdateProperty(object state) {
		lock(this) {
			cookiesDouble += cookieAdd;
		}
	}

	void Update () {
		// these round to the nearest integer
		cookies = (long) (cookiesDouble + 0.5);
		handmadeCookies = (long) (handmadeCookiesDouble + 0.5);

		if (cookiesPerClickTotal != cookiesPerClick * cookiesPerClickMultiplier + cookiesPerClickAddOn) {
			cookiesPerClickTotal = cookiesPerClick * cookiesPerClickMultiplier + cookiesPerClickAddOn;
		}

		cookiesPerSecondTotal = cookiesPerSecond * cookiesPerSecondMultiplier;
		
		cookiesText.text = cookies.ToString("N0") + " cookies";
		cookiesPerSecondText.text = "Per second: " + cookiesPerSecondTotal.ToString("N0");

		if (oldCookiesPerSecondTotal != cookiesPerSecondTotal && cookiesPerSecondTotal != 0f) {
			cookieAdd = cookiesPerSecondTotal / (1000f / cookieAddPeriod);
			oldCookiesPerSecondTotal = cookiesPerSecondTotal;
		}
	}
}