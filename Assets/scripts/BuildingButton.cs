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

	public string getPluralName() {
		return myName == "factory" ? "factories" : myName + "s";
	}

	public string getNameSingularOrPlural() {
		return count == 1 ? myName : getPluralName ();
	}

	public string getProductionDescriptionTotal() {
		return count + " " + getNameSingularOrPlural () + " producing " + getTotalCookiesPerSecondCombined ().ToString ("#,##0.##") + " cookie" + (getTotalCookiesPerSecondCombined ()==1.0?"":"s") + " per second";
	}

	public string getProductionDescriptionSingle() {
		return "each " + myName + " produces " + getTotalCookiesPerSecond ().ToString ("#,##0.##") + " cookies per second";
	}

}