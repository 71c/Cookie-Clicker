using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

	public Text cookiesText;
	public Text cookiesPerSecondText;

	private System.Threading.Timer timer;

	public decimal cookies = 0.0m;
	public decimal handmadeCookies = 0.0m;

	public decimal cookiesPerClick = 1m;
	public decimal cookiesPerClickMultiplier = 1.0m;
	public decimal cookiesPerClickAddOn = 0.0m;
	public decimal cookiesPerClickTotal;

	public decimal cookiesPerSecond = 0.0m;

	public decimal cookiesPerSecondMultiplier = 1.0m;
	public decimal cookiesPerSecondTotal = 0.0m;
	decimal oldCookiesPerSecondTotal = 0.0m;

	public string handmadeCookiesString;
	public string cookiesPerClickTotalString;

	decimal cookieAdd = 0.0m;

	int cookieAddPeriod = 25; // time between cookies updates in milliseconds


	void Start () {
		timer = new System.Threading.Timer (UpdateProperty);
		timer.Change (cookieAddPeriod, cookieAddPeriod);

		// TESTING
		cookies = 10000000000.0m;
	}

	private void UpdateProperty(object state) {
		lock(this) {
//			cookieAdd = cookiesPerSecondTotal / (1000.0m / cookieAddPeriod);
			cookies += cookiesPerSecondTotal / (1000.0m / cookieAddPeriod);
		}
	}

	void Update () {
		cookiesPerClickTotal = cookiesPerClick * cookiesPerClickMultiplier + cookiesPerClickAddOn;
		cookiesPerSecondTotal = cookiesPerSecond * cookiesPerSecondMultiplier;
		cookiesText.text = cookies.ToString("###,###0") + " cookies";
		cookiesPerSecondText.text = "Per second: " + cookiesPerSecondTotal.ToString(cookiesPerSecondTotal == (int)cookiesPerSecondTotal ? "N0" : "N1");
	
		handmadeCookiesString = handmadeCookies.ToString ();
		cookiesPerClickTotalString = cookiesPerClickTotal.ToString ();
	}
}