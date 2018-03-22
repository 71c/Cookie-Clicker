using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour {

	public GameStats gameStats;

	public float priceIncrease = 1.15f;
	public int count = 0;

	public string myName;
	public int price;
	public float cookiesPerSecond;
	public float cookiesPerSecondMultiplier = 1f;
	public float cookiesPerSecondAddOn = 0f;

	public bool isButtonVisible;
	public bool isNameVisible;

	public float getTotalCookiesPerSecond() {
		return count * (cookiesPerSecond * cookiesPerSecondMultiplier + cookiesPerSecondAddOn);
	}
}