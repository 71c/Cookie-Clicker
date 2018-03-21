using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeButtonHandler : MonoBehaviour {

	public Canvas renderCanvas;
	public Text text;
	public GameStats gameStats;
	public BuildingButtonHandler buildingButtonHandler;

	public BuildingUpgrade[] upgrades;
	public UpgradeButton[] upgradeButtons;
	public GameObject[] buttonElementHolders;
	public Text[] buttonTexts;
	public UpgradeButton upgradeButton;


	public ButtonsCollection jsonToBuildingUpgradeButtons(string json) {
		return JsonUtility.FromJson<ButtonsCollection>(json);
	}

	void Start () {
//		string json = "[{\"quantityNeeded\": \"1\", \"description\": \"The mouse and cursors are twice as efficient.\\n\\\"prod prod\\\"\", \"name\": \"Reinforced index finger\", \"basePrice\": \"100\"}, {\"quantityNeeded\": \"1\", \"description\": \"The mouse and cursors are twice as efficient.\\n\\\"it... it hurts to click...\\\"\", \"name\": \"Carpal tunnel prevention cream\", \"basePrice\": \"500\"}, {\"quantityNeeded\": \"10\", \"description\": \"The mouse and cursors are twice as efficient.\\n\\\"Look ma, both hands!\\\"\", \"name\": \"Ambidextrous\", \"basePrice\": \"10000\"}, {\"quantityNeeded\": \"25\", \"description\": \"The mouse and cursors gain +0.1 cookies for each non-cursor object owned.\\n\\\"clickity\\\"\", \"name\": \"Thousand fingers\", \"basePrice\": \"100000\"}, {\"quantityNeeded\": \"50\", \"description\": \"The mouse and cursors gain +0.5 cookies for each non-cursor object owned.\\n\\\"clickityclickity\\\"\", \"name\": \"Million fingers\", \"basePrice\": \"10000000\"}, {\"quantityNeeded\": \"100\", \"description\": \"The mouse and cursors gain +5 cookies for each non-cursor object owned.\\n\\\"clickityclickityclickity\\\"\", \"name\": \"Billion fingers\", \"basePrice\": \"100000000\"}, {\"quantityNeeded\": \"150\", \"description\": \"The mouse and cursors gain +50 cookies for each non-cursor object owned.\\n\\\"clickityclickityclickityclickity\\\"\", \"name\": \"Trillion fingers\", \"basePrice\": \"1000000000\"}, {\"quantityNeeded\": \"200\", \"description\": \"The mouse and cursors gain +500 cookies for each non-cursor object owned.\\n\\\"clickityclickityclickityclickityclick\\\"\", \"name\": \"Quadrillion fingers\", \"basePrice\": \"10000000000\"}, {\"quantityNeeded\": \"250\", \"description\": \"The mouse and cursors gain +5000 cookies for each non-cursor object owned.\\n\\\"man, just go click click click click click, it\\u2019s real easy, man.\\\"\", \"name\": \"Quintillion fingers\", \"basePrice\": \"10000000000000\"}, {\"quantityNeeded\": \"300\", \"description\": \"The mouse and cursors gain +50000 cookies for each non-cursor object owned.\\n\\\"sometimes\\nthings just\\nclick\\\"\", \"name\": \"Sextillion fingers\", \"basePrice\": \"10000000000000000\"}, {\"quantityNeeded\": \"350\", \"description\": \"The mouse and cursors gain +500000 cookies for each non-cursor object owned.\\n\\\"[cursory flavor text]\\\"\", \"name\": \"Septillion fingers\", \"basePrice\": \"10000000000000000000\"}, {\"quantityNeeded\": \"400\", \"description\": \"The mouse and cursors gain +5000000 cookies for each non-cursor object owned.\\n\\\"Turns out you can quite put your finger on it.\\\"\", \"name\": \"Octillion fingers\", \"basePrice\": \"10000000000000000000000\"}]";
		string json = "[{\"quantityNeeded\": \"1\", \"description\": \"The mouse and cursors are twice as efficient.\\n\\\"prod prod\\\"\", \"name\": \"Reinforced index finger\", \"basePrice\": \"100\"}, {\"quantityNeeded\": \"1\", \"description\": \"The mouse and cursors are twice as efficient.\\n\\\"it... it hurts to click...\\\"\", \"name\": \"Carpal tunnel prevention cream\", \"basePrice\": \"500\"}, {\"quantityNeeded\": \"10\", \"description\": \"The mouse and cursors are twice as efficient.\\n\\\"Look ma, both hands!\\\"\", \"name\": \"Ambidextrous\", \"basePrice\": \"10000\"}, {\"quantityNeeded\": \"25\", \"description\": \"The mouse and cursors gain +0.1 cookies for each non-cursor object owned.\\n\\\"clickity\\\"\", \"name\": \"Thousand fingers\", \"basePrice\": \"100000\"}, {\"quantityNeeded\": \"50\", \"description\": \"The mouse and cursors gain +0.5 cookies for each non-cursor object owned.\\n\\\"clickityclickity\\\"\", \"name\": \"Million fingers\", \"basePrice\": \"10000000\"}, {\"quantityNeeded\": \"100\", \"description\": \"The mouse and cursors gain +5 cookies for each non-cursor object owned.\\n\\\"clickityclickityclickity\\\"\", \"name\": \"Billion fingers\", \"basePrice\": \"100000000\"}]";

		json = "{\"buttons\":" + json + "}"; 
		ButtonsCollection buttonsCollection = jsonToBuildingUpgradeButtons(json);
		upgrades = buttonsCollection.buttons;


		upgradeButtons = new UpgradeButton[upgrades.Length];
		buttonElementHolders = new GameObject[upgrades.Length];
		buttonTexts = new Text[upgrades.Length];

		float y = 208f;
		for (int i = 0; i < buttonElementHolders.Length; i++) {
			buttonElementHolders[i] = new GameObject();
			buttonElementHolders [i].transform.SetParent (renderCanvas.transform, false);
			buttonElementHolders[i].transform.localPosition = new Vector2(086.7f, y);

			upgradeButtons [i] = (UpgradeButton)Instantiate (upgradeButton, transform.position, transform.rotation);
			upgradeButtons [i].transform.SetParent (buttonElementHolders [i].transform, false);
			upgradeButtons [i].transform.localPosition = new Vector2(0f, 0f);
			upgradeButtons [i].upgrade = upgrades [i];

			buttonTexts[i] = Instantiate (text, transform.position, transform.rotation);
			buttonTexts[i].transform.SetParent (upgradeButtons [i].transform, false);
			buttonTexts[i].transform.localPosition = new Vector2(0f, 0f);
			buttonTexts[i].text = upgrades[i].name;

			y -= 83.46f;
		}
	}

//	void Update() {
//		for (int i = 0; i < upgrades.Length; i++) {
//			if ((long)(upgrades[i].basePrice) <= gameStats.score)
//				buttonElementHolders[i].gameObject.SetActive(true);
//		}
//	}

	public void TaskOnClick(UpgradeButton button) {
		if (Convert.ToInt64 (button.upgrade.basePrice) <= gameStats.score && !button.upgrade.enabled && buildingButtonHandler.findButtonWithName("cursor").level >= Convert.ToInt64 (button.upgrade.quantityNeeded)) {
			button.upgrade.enabled = true;
			gameStats.pointsPerClick *= 2;
		}
	}
	
}


public class ButtonsCollection {
	public BuildingUpgrade[] buttons;
}