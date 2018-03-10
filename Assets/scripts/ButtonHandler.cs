using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonHandler : MonoBehaviour
{
	public BuildingButton button;
	public Text text;
	public Canvas renderCanvas;
	public List<BuildingButton> buildingButtons = new List<BuildingButton>();
	public List<GameObject> buttonElementHolders = new List<GameObject> ();

	public List<Text> buildingButtonLables = new List<Text> ();
	public List<Text> buildingLevelTexts = new List<Text> ();


	public GameStats gameStats;

	string[] names = new string[] { "cursor", "grandma", "farm", "mine", "factory", "bank" };
	float[] prices = new float[] { 15, 100, 1100, 12000, 130000, 1400000 };
	float[] baseCookiesPerSeconds = new float[] { 0.1f, 1f, 8f, 47f, 260f, 1400f };



	void Start() {
		for (float y = 3.45f; y > -4.3f; y -= 2.14f) {
			int lastIndex = buildingButtons.Count;

			buttonElementHolders.Add (new GameObject ());
			buttonElementHolders.ElementAt (lastIndex).transform.SetParent (renderCanvas.transform, false);

			createButton (names [lastIndex], prices [lastIndex], baseCookiesPerSeconds [lastIndex]);
			buildingButtons.Add((BuildingButton)Instantiate(button, transform.position, transform.rotation));
			buildingButtons.ElementAt(lastIndex).transform.SetParent(buttonElementHolders.ElementAt (lastIndex).transform, false);

			buildingButtons.ElementAt(lastIndex).transform.position = new Vector2(6.15f * 52f, y * 52f);


			buildingButtonLables.Add(Instantiate(text, transform.position, transform.rotation) as Text);

			buildingButtonLables.ElementAt(lastIndex).transform.SetParent(buttonElementHolders.ElementAt (lastIndex).transform, false);
			setText (buildingButtonLables.ElementAt (lastIndex), "", 18);

			buildingLevelTexts.Add(Instantiate(text, transform.position, transform.rotation) as Text);
			setText (buildingLevelTexts.ElementAt (lastIndex), "TEST", 36);
//			buildingLevelTexts.ElementAt(lastIndex).transform.position = new Vector2(7.15f * 52f, y * 52f);
			buildingLevelTexts.ElementAt(lastIndex).transform.SetParent(buttonElementHolders.ElementAt (lastIndex).transform, false);
			buildingLevelTexts.ElementAt(lastIndex).transform.localPosition = new Vector2(0f, 0f);


		}
	}

	void Update() {
		for (int i = 0; i < buildingButtonLables.Count; i++) {
			buildingButtonLables.ElementAt(i).text = names[i] + " (+" + buildingButtons[i].cookiesPerSecond + " CpS)\n" + buildingButtons[i].price + " cookies";


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
}