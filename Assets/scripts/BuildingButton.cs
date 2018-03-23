using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour {

	public GameStats gameStats;

	public float priceIncrease = 1.15f;
	public int count = 0;

	public string myName;
	public long price;
	public double cookiesPerSecond;
	public double cookiesPerSecondMultiplier = 1.0;
	public double cookiesPerSecondAddOn = 0.0;

	public bool isButtonVisible;
	public bool isNameVisible;

	public double getTotalCookiesPerSecondCombined() {
		return count * getTotalCookiesPerSecond();
	}

	public double getTotalCookiesPerSecond() {
		return cookiesPerSecond * cookiesPerSecondMultiplier + cookiesPerSecondAddOn;
	}
}