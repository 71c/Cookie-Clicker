using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.IO;
//using System.Numerics;
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
		string path = "Assets/Resources/json.txt";

		//Read the text from directly from the json.txt file
		StreamReader reader = new StreamReader(path); 
		string json = reader.ReadToEnd();
		reader.Close();

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

//			y -= 83.46f;
			y -= 62.4f;
		}
	}

	bool quantityMet(BuildingUpgrade upgrade) {
		bool a = upgrade.upgradeClass == "building" && buildingButtonHandler.findButtonWithName (upgrade.upgradeType).count >= Convert.ToInt64 (upgrade.quantityNeeded);

		string theThing = new Regex (" cookies").Replace (upgrade.quantityNeeded, "");
		bool b = upgrade.upgradeClass == "flavored cookies" && theThing.Length < 20 && gameStats.cookies >= Convert.ToInt64 (theThing);

		theThing = upgrade.quantityNeeded.Replace (" hand-made cookies", "");
		bool c = upgrade.upgradeClass == "clicking upgrades" && theThing.Length < 20 && gameStats.handmadeCookies >= Convert.ToInt64 (theThing);

		BuildingButton theBuilding = buildingButtonHandler.findButtonWithName (new Regex ("(?<=15 ).+(?=s and)").Match (upgrade.quantityNeeded).Groups [0].ToString ());
		bool d = upgrade.upgradeClass == "grandma types" && theBuilding != null && theBuilding.count >= 15 && buildingButtonHandler.findButtonWithName ("grandma").count >= 1;

		bool e = upgrade.basePrice.Length < 20; // this shouldn't exist. temporary fix. the numbers get too big. also seen above.

		return (a || b || c || d) && e;
	}

	void Update() {
		List<BuildingUpgrade> upgradesQuantityMet = new List<BuildingUpgrade> ();
		for (int i = 0; i < upgrades.Length; i++) {
			BuildingUpgrade upgrade = upgrades[i];

			updateCursorUpgradeIfNeeded (upgrade);

			if (!upgrade.enabled) {
				if (quantityMet(upgrade))
					upgradesQuantityMet.Add (upgrade);
			}

		}
		upgradesQuantityMet = sortByPrice (upgradesQuantityMet);
		for (int i = 0; i < upgradeButtons.Length; i++) {
			buttonElementHolders[i].gameObject.SetActive(upgradesQuantityMet.Count > 0);
			if (upgradesQuantityMet.Count > 0) {
				// set the upgrade of the button to the last upgrade in the list and remove the upgrade from the list
				upgradeButtons [i].upgrade = upgradesQuantityMet [upgradesQuantityMet.Count - 1];
				upgradesQuantityMet.RemoveAt (upgradesQuantityMet.Count - 1);
//				buttonTexts [i].text = upgradeButtons [i].upgrade.name + "\n(" + upgradeButtons[i].upgrade.basePrice + ")\n" + upgradeButtons[i].upgrade.description;
				buttonTexts [i].text = upgradeButtons [i].upgrade.name + "\n(" + upgradeButtons[i].upgrade.basePrice + ")";
				buttonTexts [i].rectTransform.localPosition = new Vector2 (buttonTexts [i].rectTransform.localPosition.x, 15f);

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
		if (Convert.ToInt64 (button.upgrade.basePrice) <= gameStats.cookies && !button.upgrade.enabled && quantityMet(button.upgrade)) {
			button.upgrade.enabled = true;
			gameStats.cookiesDouble -= Convert.ToInt64 (button.upgrade.basePrice);
			if (!(button.upgrade.upgradeType == "grandma types" || button.upgrade.upgradeType == "cursor") || button.upgrade.name == "Reinforced index finger" || button.upgrade.name == "Carpal tunnel prevention cream" || button.upgrade.name == "Ambidextrous") {
				if (button.upgrade.upgradeType == "flavored cookies") {
					string description = button.upgrade.description;
					string multiplierIncrease = new Regex("(?<=\\+)\\d+(?=%)").Match(description).Groups[0].ToString();
					print (multiplierIncrease);
					gameStats.cookiesPerSecondMultiplier *= 1f + float.Parse(multiplierIncrease) / 100f;
				} else {
					buildingButtonHandler.findButtonWithName (button.upgrade.upgradeType).cookiesPerSecondMultiplier *= 2;
				}
				if (button.upgrade.upgradeType == "cursor")
					gameStats.cookiesPerClickMultiplier *= 2;
			}
		}
	}
	
}


public class ButtonsCollection {
	public BuildingUpgrade[] buttons;
}