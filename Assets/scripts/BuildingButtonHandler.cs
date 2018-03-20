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

	string[] names = new string[] { "cursor", "grandma", "farm", "mine", "factory", "bank" };
	float[] prices = new float[] { 15, 100, 1100, 12000, 130000, 1400000 };
	float[] baseCookiesPerSeconds = new float[] { 0.1f, 1f, 8f, 47f, 260f, 1400f };

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
			y -= 1.605f;
		}
		buildingButtons[0].isButtonVisible = true;
		buildingButtons[1].isButtonVisible = true;
	}

	void Update() {
		for (int i = 0; i < buildingButtonLabels.Count; i++) {

			BuildingButton currentButton = buildingButtons[i];

			if (gameStats.score >= currentButton.price && !currentButton.isNameVisible) {
				currentButton.isNameVisible = true;
				currentButton.isButtonVisible = true;
				if (i + 1 < buildingButtons.Count)
					buildingButtons [i + 1].isButtonVisible = true;
				if (i + 2 < buildingButtons.Count)
					buildingButtons [i + 2].isButtonVisible = true;
			}

			buttonElementHolders[i].gameObject.SetActive(currentButton.isButtonVisible);

			if (currentButton.isNameVisible)
				buildingButtonLabels[i].text = names [i] + " (+" + buildingButtons [i].cookiesPerSecond + " CpS)\n" + buildingButtons [i].price + " cookies";
			else
				buildingButtonLabels[i].text = "??\n" + buildingButtons [i].price + " cookies";

			buildingLevelTexts.ElementAt(i).text = buildingButtons[i].level == 0 ? "" : buildingButtons[i].level + "";

			if (gameStats.score >= buildingButtons.ElementAt (i).price) {
				buildingButtons[i].GetComponent<Image> ().color = new Color (0.7f, 0.7f, 0.7f);
				buildingButtonLabels.ElementAt (i).color = Color.white;
			} else {
				buildingButtons[i].GetComponent<Image> ().color = Color.gray;
				buildingButtonLabels.ElementAt (i).color = new Color (0.7f, 0.7f, 0.7f);
			}
		}
	}

	void createButton(string newName, float newPrice, float newCPS) {
		button.myName = newName;
		button.price = newPrice;
		button.cookiesPerSecond = newCPS;
	}

	void setText(Text textObject, string words, int size) {
		textObject.fontSize = size; //Set the text box's text element font size and style:
		textObject.text = words; //Set the text box's text element to the current textToDisplay:
	}

	public void TaskOnClick(BuildingButton button) {
		if (gameStats.score >= button.price) {
			gameStats.pointsPerSecond += button.cookiesPerSecond;
			gameStats.score -= button.price;
			button.level++;
			button.price = Mathf.CeilToInt (button.price * Mathf.Pow (button.priceIncrease, button.level));
		}
	}
}