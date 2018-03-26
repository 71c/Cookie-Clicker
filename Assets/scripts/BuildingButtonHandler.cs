using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
public class BuildingButtonHandler : MonoBehaviour
{
	public BuildingButton button;
	public Text text;
	public Canvas renderCanvas;
	public List<BuildingButton> buildingButtons = new List<BuildingButton>();
	public List<GameObject> buttonElementHolders = new List<GameObject> ();

	public List<Text> buildingButtonLabels = new List<Text> ();
	public List<Text> buildingLevelTexts = new List<Text> ();

	public GameStats gameStats;

	string[] names = new string[] { "cursor", "grandma", "farm", "mine", "factory", "bank", "temple", "wizard tower" };
	int[] prices = new int[] { 15, 100, 1100, 12000, 130000, 1400000, 20000000, 330000000 };
	double[] baseCookiesPerSeconds = new double[] { 0.1, 1.0, 8.0, 47.0, 260.0, 1400.0, 7800.0, 44000.0 };

	void addNewButton(float x, float y) {
		int lastIndex = buildingButtons.Count;

		buttonElementHolders.Add (new GameObject ());
		buttonElementHolders.ElementAt (lastIndex).transform.SetParent (renderCanvas.transform, false);
		buttonElementHolders.ElementAt (lastIndex).transform.localPosition = new Vector2(x, y);

		// make button, add it to buildingButtons
		createButton (names [lastIndex], prices [lastIndex], baseCookiesPerSeconds [lastIndex]);
		buildingButtons.Add((BuildingButton)Instantiate(button, transform.position, transform.rotation));

		// set parent to element holder
		buildingButtons.ElementAt(lastIndex).transform.SetParent(buttonElementHolders.ElementAt (lastIndex).transform, false);
		buildingButtons.ElementAt (lastIndex).transform.localPosition = new Vector2 (0f, 0f);

		buildingButtons.ElementAt (lastIndex).isButtonVisible = false;



		// add a text to buildingButtonLables
		buildingButtonLabels.Add(Instantiate(text, transform.position, transform.rotation) as Text);
		// set parent to element holder
		buildingButtonLabels.ElementAt(lastIndex).transform.SetParent(buildingButtons.ElementAt (lastIndex).transform, false);
		// set the text to "??"
		setText (buildingButtonLabels.ElementAt (lastIndex), "??", 18);
		// set position
		buildingButtonLabels.ElementAt(lastIndex).transform.localPosition = new Vector2(0f, 0f);
		// set color
		buildingButtonLabels.ElementAt(lastIndex).color = Color.white;


		// add a text to buildingLevelTexts
		buildingLevelTexts.Add(Instantiate(text, transform.position, transform.rotation) as Text);
		// set text to "TEST"
		setText (buildingLevelTexts.ElementAt (lastIndex), "0", 36);
		// set parent to element holder
		buildingLevelTexts.ElementAt(lastIndex).transform.SetParent(buttonElementHolders.ElementAt (lastIndex).transform, false);
		// set position
		buildingLevelTexts.ElementAt(lastIndex).transform.localPosition = new Vector2(120f, 0f);
		// set color
		buildingLevelTexts.ElementAt(lastIndex).color = Color.white;
	}

	void Start() {
		float y = 4f;
		for (int i = 0; i < names.Length; i++) {
			addNewButton (286.7f, y * 52f);
//			y -= 1.605f;
			y -= 1.2f;
		}
		buildingButtons[0].isButtonVisible = true;
		buildingButtons[1].isButtonVisible = true;

		// TESTING
//		findButtonWithName ("cursor").count = 25;
	}

	void Update() {
		for (int i = 0; i < buildingButtonLabels.Count; i++) {

			BuildingButton currentButton = buildingButtons[i];

			if (gameStats.cookies >= currentButton.price && !currentButton.isNameVisible) {
				currentButton.isNameVisible = true;
				currentButton.isButtonVisible = true;
				if (i + 1 < buildingButtons.Count)
					buildingButtons [i + 1].isButtonVisible = true;
				if (i + 2 < buildingButtons.Count)
					buildingButtons [i + 2].isButtonVisible = true;
			}

			buttonElementHolders[i].gameObject.SetActive(currentButton.isButtonVisible);

			if (currentButton.isNameVisible)
				buildingButtonLabels[i].text = names [i] + " (+" + buildingButtons [i].getTotalCookiesPerSecond() + " CpS)\n" + buildingButtons [i].price + " cookies";
			else
				buildingButtonLabels[i].text = "??\n" + buildingButtons [i].price + " cookies";

			buildingLevelTexts.ElementAt(i).text = buildingButtons[i].count == 0 ? "" : buildingButtons[i].count + "";

			if (gameStats.cookies >= buildingButtons.ElementAt (i).price) {
				buildingButtons[i].GetComponent<Image> ().color = new Color (0.7f, 0.7f, 0.7f);
				buildingButtonLabels.ElementAt (i).color = Color.white;
			} else {
				buildingButtons[i].GetComponent<Image> ().color = Color.gray;
				buildingButtonLabels.ElementAt (i).color = new Color (0.7f, 0.7f, 0.7f);
			}
		}

		refreshCookiesPerSecond ();
	}

	void createButton(string newName, int newPrice, double newCPS) {
		button.myName = newName;
		button.price = newPrice;
		button.cookiesPerSecond = newCPS;
		button.cookiesPerSecondMultiplier = 1.0;
	}

	void setText(Text textObject, string words, int size) {
		textObject.fontSize = size; //Set the text box's text element font size and style
		textObject.text = words; //Set the text box's text element to the current textToDisplay
	}

	public void TaskOnClick(BuildingButton button) {
		if (gameStats.cookies >= button.price) {
			gameStats.cookiesPerSecond += (decimal)button.cookiesPerSecond;
			gameStats.cookies -= button.price;
			button.count++;
			button.price = (long) (button.price * button.priceIncrease + 1.0);
		}
	}


	public BuildingButton findButtonWithName(string nameToFind) {
		for (int i = 0; i < buildingButtons.Count; i++) {
			if (buildingButtons [i].myName == nameToFind)
				return buildingButtons [i];
		}
		return null;
	}

	public void refreshCookiesPerSecond() {
		double cookiesPerSecond = 0.0;
		for (int i = 0; i < buildingButtons.Count; i++)
			cookiesPerSecond += buildingButtons [i].getTotalCookiesPerSecondCombined ();
		gameStats.cookiesPerSecond = (decimal)cookiesPerSecond;
	}

	public int getNumberOfBuildings() {
		int buildingCount = 0;
		for (int i = 0; i < buildingButtons.Count; i++)
			buildingCount += buildingButtons[i].count;
		return buildingCount;
	}
}