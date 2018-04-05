using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameStats : MonoBehaviour {

	public Text cookiesText;
	public Text cookiesPerSecondText;
	public Text cookiesPerClickTextTest;

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

	public string handmadeCookiesString;
	public string cookiesPerClickTotalString;

	int cookieAddPeriod = 25; // time between cookies updates in milliseconds

	Dictionary<int, string> zeroCountsToWords = new Dictionary<int, string>();

	public bool shortenNumbers = true;

	void Start () {
		timer = new System.Threading.Timer (UpdateProperty);
		timer.Change (cookieAddPeriod, cookieAddPeriod);

		zeroCountsToWords.Add(33, "decillion");
		zeroCountsToWords.Add(3, "thousand");
		zeroCountsToWords.Add(36, "undecillion");
		zeroCountsToWords.Add(6, "million");
		zeroCountsToWords.Add(39, "duodecillion");
		zeroCountsToWords.Add(9, "billion");
		zeroCountsToWords.Add(42, "tredecillion");
		zeroCountsToWords.Add(12, "trillion");
		zeroCountsToWords.Add(15, "quadrillion");
		zeroCountsToWords.Add(18, "quintillion");
		zeroCountsToWords.Add(21, "sextillion");
		zeroCountsToWords.Add(24, "septillion");
		zeroCountsToWords.Add(27, "octillion");
		zeroCountsToWords.Add(30, "nonillion");

//		cookies = 100000000m; // testing
	}

	private void UpdateProperty(object state) {
		lock(this) {
			cookies += cookiesPerSecondTotal / (1000.0m / cookieAddPeriod);
		}
	}

	void Update () {
		cookiesPerClickTotal = cookiesPerClick * cookiesPerClickMultiplier + cookiesPerClickAddOn;
		cookiesPerSecondTotal = cookiesPerSecond * cookiesPerSecondMultiplier;
		string formattedCookieCount = formatNumber (cookies, shortenNumbers ? 3 : 0);
		cookiesText.text = formattedCookieCount + " cookies";
		cookiesPerSecondText.text = "Per second: " + cookiesPerSecondTotal.ToString(cookiesPerSecondTotal == (int)cookiesPerSecondTotal ? "N0" : "N1");

		cookiesPerClickTextTest.text = "per click orig: " + cookiesPerClick + "\nper click total: " + cookiesPerClickTotal + "\nclick mult: " + cookiesPerClickMultiplier + "\naddOn: " + cookiesPerClickAddOn; // (test)

		handmadeCookiesString = handmadeCookies.ToString ();
		cookiesPerClickTotalString = cookiesPerClickTotal.ToString ();
	}

	string spelledOutNumber(decimal num, int places) {
		if (num < 1000m)
			return Decimal.Round(num) + "";
		num = Decimal.Round(num / 1000m) * 1000m;
		num = Decimal.Round(num * (decimal)Math.Pow(10.0, (double)places)) / (decimal)Math.Pow(10.0, (double)places);
		string strNum = num + "";
		int headNumLen = strNum.Length % 3 == 0 ? 3 : strNum.Length % 3;
		int zerosLen = strNum.Length - headNumLen;
		float firstPart = Mathf.Round((float)num / Mathf.Pow(10f, (float)zerosLen) * Mathf.Pow(10f, (float)places)) / Mathf.Pow(10f, (float)places);
		return (firstPart == (int)firstPart ? (int)firstPart : firstPart) + " " + zeroCountsToWords [zerosLen];
	}
		
	public string formatNumber(decimal num, int places) {
		if (shortenNumbers)
			return spelledOutNumber(num, places);
		return num.ToString ("#,##0." + new String('#', places));
	}
}