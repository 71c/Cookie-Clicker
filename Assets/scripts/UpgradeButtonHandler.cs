using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text.RegularExpressions;

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

	int oldTotalNonCursors = 0;


	public ButtonsCollection jsonToBuildingUpgradeButtons(string json) {
		return JsonUtility.FromJson<ButtonsCollection>(json);
	}

	void Start () {
//		string json = "[{"upgradeType": "cursor", "quantityNeeded": "1", "description": "The mouse and cursors are twice as efficient.\n\"prod prod\"", "name": "Reinforced index finger", "basePrice": "100"}, {"upgradeType": "cursor", "quantityNeeded": "1", "description": "The mouse and cursors are twice as efficient.\n\"it... it hurts to click...\"", "name": "Carpal tunnel prevention cream", "basePrice": "500"}, {"upgradeType": "cursor", "quantityNeeded": "10", "description": "The mouse and cursors are twice as efficient.\n\"Look ma, both hands!\"", "name": "Ambidextrous", "basePrice": "10000"}, {"upgradeType": "cursor", "quantityNeeded": "25", "description": "The mouse and cursors gain +0.1 cookies for each non-cursor object owned.\n\"clickity\"", "name": "Thousand fingers", "basePrice": "100000"}, {"upgradeType": "cursor", "quantityNeeded": "50", "description": "The mouse and cursors gain +0.5 cookies for each non-cursor object owned.\n\"clickityclickity\"", "name": "Million fingers", "basePrice": "10000000"}, {"upgradeType": "cursor", "quantityNeeded": "100", "description": "The mouse and cursors gain +5 cookies for each non-cursor object owned.\n\"clickityclickityclickity\"", "name": "Billion fingers", "basePrice": "100000000"}, {"upgradeType": "cursor", "quantityNeeded": "150", "description": "The mouse and cursors gain +50 cookies for each non-cursor object owned.\n\"clickityclickityclickityclickity\"", "name": "Trillion fingers", "basePrice": "1000000000"}, {"upgradeType": "cursor", "quantityNeeded": "200", "description": "The mouse and cursors gain +500 cookies for each non-cursor object owned.\n\"clickityclickityclickityclickityclick\"", "name": "Quadrillion fingers", "basePrice": "10000000000"}, {"upgradeType": "cursor", "quantityNeeded": "250", "description": "The mouse and cursors gain +5000 cookies for each non-cursor object owned.\n\"man, just go click click click click click, it\u2019s real easy, man.\"", "name": "Quintillion fingers", "basePrice": "10000000000000"}, {"upgradeType": "cursor", "quantityNeeded": "300", "description": "The mouse and cursors gain +50000 cookies for each non-cursor object owned.\n\"sometimes\nthings just\nclick\"", "name": "Sextillion fingers", "basePrice": "10000000000000000"}, {"upgradeType": "cursor", "quantityNeeded": "350", "description": "The mouse and cursors gain +500000 cookies for each non-cursor object owned.\n\"[cursory flavor text]\"", "name": "Septillion fingers", "basePrice": "10000000000000000000"}, {"upgradeType": "cursor", "quantityNeeded": "400", "description": "The mouse and cursors gain +5000000 cookies for each non-cursor object owned.\n\"Turns out you can quite put your finger on it.\"", "name": "Octillion fingers", "basePrice": "10000000000000000000000"}]";
		string json = "[{\"upgradeType\": \"cursor\", \"quantityNeeded\": \"1\", \"description\": \"The mouse and cursors are twice as efficient.\\n\\\"prod prod\\\"\", \"name\": \"Reinforced index finger\", \"basePrice\": \"100\"}, {\"upgradeType\": \"cursor\", \"quantityNeeded\": \"1\", \"description\": \"The mouse and cursors are twice as efficient.\\n\\\"it... it hurts to click...\\\"\", \"name\": \"Carpal tunnel prevention cream\", \"basePrice\": \"500\"}, {\"upgradeType\": \"cursor\", \"quantityNeeded\": \"10\", \"description\": \"The mouse and cursors are twice as efficient.\\n\\\"Look ma, both hands!\\\"\", \"name\": \"Ambidextrous\", \"basePrice\": \"10000\"}, {\"upgradeType\": \"cursor\", \"quantityNeeded\": \"25\", \"description\": \"The mouse and cursors gain +0.1 cookies for each non-cursor object owned.\\n\\\"clickity\\\"\", \"name\": \"Thousand fingers\", \"basePrice\": \"100000\"}, {\"upgradeType\": \"cursor\", \"quantityNeeded\": \"50\", \"description\": \"The mouse and cursors gain +0.5 cookies for each non-cursor object owned.\\n\\\"clickityclickity\\\"\", \"name\": \"Million fingers\", \"basePrice\": \"10000000\"}, {\"upgradeType\": \"cursor\", \"quantityNeeded\": \"100\", \"description\": \"The mouse and cursors gain +5 cookies for each non-cursor object owned.\\n\\\"clickityclickityclickity\\\"\", \"name\": \"Billion fingers\", \"basePrice\": \"100000000\"}, {\"upgradeType\": \"cursor\", \"quantityNeeded\": \"150\", \"description\": \"The mouse and cursors gain +50 cookies for each non-cursor object owned.\\n\\\"clickityclickityclickityclickity\\\"\", \"name\": \"Trillion fingers\", \"basePrice\": \"1000000000\"}]";


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

			buttonTexts[i] = Instantiate (text, transform.position, transform.rotation);
			buttonTexts[i].transform.SetParent (upgradeButtons [i].transform, false);
			buttonTexts[i].transform.localPosition = new Vector2(0f, 0f);

			y -= 83.46f;
		}
	}

	void Update() {
		List<BuildingUpgrade> upgradesNuffBuildings = new List<BuildingUpgrade> ();
		for (int i = 0; i < upgrades.Length; i++) {
			BuildingUpgrade upgrade = upgrades[i];

			updateCursorUpgradeIfNeeded (upgrade);

			if (buildingButtonHandler.findButtonWithName (upgrade.upgradeType).count >= Convert.ToInt64 (upgrade.quantityNeeded) && !upgrade.enabled)
				upgradesNuffBuildings.Add (upgrade);
		}
		upgradesNuffBuildings = sortByPrice (upgradesNuffBuildings);
		for (int i = 0; i < upgradeButtons.Length; i++) {
			buttonElementHolders[i].gameObject.SetActive(upgradesNuffBuildings.Count > 0);
			if (upgradesNuffBuildings.Count > 0) {
				// set the upgrade of the button to the last upgrade in the list and remove the upgrade from the list
				upgradeButtons [i].upgrade = upgradesNuffBuildings [upgradesNuffBuildings.Count - 1];
				upgradesNuffBuildings.RemoveAt (upgradesNuffBuildings.Count - 1);
				buttonTexts [i].text = upgradeButtons [i].upgrade.name;

				// set the color according to whether it is affordable or not
				if (gameStats.cookies >= Convert.ToInt64 (upgradeButtons [i].upgrade.basePrice)) {
					upgradeButtons [i].GetComponent<Image> ().color = new Color (0.7f, 0.7f, 0.7f);
					buttonTexts [i].color = Color.white;
				} else {
					upgradeButtons [i].GetComponent<Image> ().color = Color.gray;
					buttonTexts [i].color = new Color (0.7f, 0.7f, 0.7f);
				}
			}
		}
	}

	List<BuildingUpgrade> sortByPrice(List<BuildingUpgrade> theList) {
		return theList.OrderByDescending(upgrade => Convert.ToInt64 (upgrade.basePrice)).ToList();
	}

	void updateCursorUpgradeIfNeeded(BuildingUpgrade upgrade) {
		if (upgrade.enabled && upgrade.upgradeType == "cursor" && !(upgrade.name == "Reinforced index finger" || upgrade.name == "Carpal tunnel prevention cream" || upgrade.name == "Ambidextrous")) {
			int totalBuildings = buildingButtonHandler.getNumberOfBuildings();
			int totalCursors = buildingButtonHandler.findButtonWithName ("cursor").count;
			int totalNonCursors = totalBuildings - totalCursors;
			if (totalNonCursors != oldTotalNonCursors) {
				oldTotalNonCursors = totalNonCursors;

				string description = upgrade.description;
				string cookieGainPerNonCursorString = new Regex("(?<=\\+)(\\d|\\.)+(?= cookies)").Match(description).Groups[0].ToString();

				float cookieGainPerNonCursor = (float) Convert.ToDouble (cookieGainPerNonCursorString);

				float cookiesGained = cookieGainPerNonCursor * totalNonCursors;

				gameStats.cookiesPerClickAddOn -= upgrade.cookiesPerSecondAddOn;
				buildingButtonHandler.findButtonWithName ("cursor").cookiesPerSecondAddOn -= upgrade.cookiesPerSecondAddOn;

				upgrade.cookiesPerSecondAddOn = cookiesGained;

				gameStats.cookiesPerClickAddOn += upgrade.cookiesPerSecondAddOn;
				buildingButtonHandler.findButtonWithName ("cursor").cookiesPerSecondAddOn += upgrade.cookiesPerSecondAddOn;
			}
		}
	}

	public void TaskOnClick(UpgradeButton button) {
		if (Convert.ToInt64 (button.upgrade.basePrice) <= gameStats.cookies && !button.upgrade.enabled && buildingButtonHandler.findButtonWithName(button.upgrade.upgradeType).count >= Convert.ToInt64 (button.upgrade.quantityNeeded)) {
			button.upgrade.enabled = true;
			if (button.upgrade.upgradeType == "cursor") {
				if (button.upgrade.name == "Reinforced index finger" || button.upgrade.name == "Carpal tunnel prevention cream" || button.upgrade.name == "Ambidextrous") {
					gameStats.cookiesPerClickMultiplier *= 2;
					buildingButtonHandler.findButtonWithName ("cursor").cookiesPerSecondMultiplier *= 2;
				}
			}
		}
	}
	
}


public class ButtonsCollection {
	public BuildingUpgrade[] buttons;
}