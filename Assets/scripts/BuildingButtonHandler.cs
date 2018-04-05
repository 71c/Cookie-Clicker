using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
public class BuildingButtonHandler : MonoBehaviour {
	public BuildingButton button;
	public Text text;
	public Text popupText;
	public RectTransform building;
	public RectTransform buildingHolder;
	public RectTransform scrollOnMe;
	public Canvas renderCanvas;
	public List<BuildingButton> buildingButtons = new List<BuildingButton>();
	public List<GameObject> buttonElementHolders = new List<GameObject> ();


	public List<Text> buildingButtonLabelNames = new List<Text> ();
	public List<Text> buildingButtonLabelPrices = new List<Text> ();
	public List<Text> buildingLevelTexts = new List<Text> ();
	public List<Text> popupTexts = new List<Text> ();

	public GameStats gameStats;

	string[] names = new string[] { "cursor", "grandma", "farm", "mine", "factory", "bank", "temple", "wizard tower" };
	int[] prices = new int[] { 15, 100, 1100, 12000, 130000, 1400000, 20000000, 330000000 };
	double[] baseCookiesPerSeconds = new double[] { 0.1, 1.0, 8.0, 47.0, 260.0, 1400.0, 7800.0, 44000.0 };

	public List<RectTransform>[] buildingsDisplays;
	public List<RectTransform> buildingHolders;
	public List<RectTransform> scrollOnMees;

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
		buildingButtonLabelNames.Add(Instantiate(text, transform.position, transform.rotation) as Text);
		buildingButtonLabelNames [lastIndex].alignment = TextAnchor.UpperLeft;
		// set parent to element holder
		buildingButtonLabelNames.ElementAt(lastIndex).transform.SetParent(buildingButtons.ElementAt (lastIndex).transform, false);
		// set the text to "??"
		setText (buildingButtonLabelNames.ElementAt (lastIndex), "??", 18);
		// set position
		buildingButtonLabelNames.ElementAt(lastIndex).transform.localPosition = new Vector2(5f, 0f);
		// set color
		buildingButtonLabelNames.ElementAt(lastIndex).color = Color.white;




		buildingButtonLabelPrices.Add(Instantiate(text, transform.position, transform.rotation) as Text);
		buildingButtonLabelPrices [lastIndex].alignment = TextAnchor.UpperLeft;
		buildingButtonLabelPrices[lastIndex].transform.SetParent(buildingButtons.ElementAt (lastIndex).transform, false);
		setText (buildingButtonLabelPrices[lastIndex], "??", 14);
		buildingButtonLabelPrices[lastIndex].transform.localPosition = new Vector2(5f, -20f);
		buildingButtonLabelPrices[lastIndex].color = Color.white;



		// add a text to buildingLevelTexts
		buildingLevelTexts.Add(Instantiate(text, transform.position, transform.rotation) as Text);
		buildingLevelTexts [lastIndex].alignment = TextAnchor.UpperLeft;
		// set text to "0"
		setText (buildingLevelTexts.ElementAt (lastIndex), "0", 20);
		// set parent to element holder
		buildingLevelTexts.ElementAt(lastIndex).transform.SetParent(buttonElementHolders.ElementAt (lastIndex).transform, false);
		// set position
		buildingLevelTexts.ElementAt(lastIndex).transform.localPosition = new Vector2(151.1f, 0f);
		// set color
		buildingLevelTexts.ElementAt(lastIndex).color = Color.white;


		popupTexts.Add (Instantiate(popupText, transform.position, transform.rotation) as Text);
		popupTexts[lastIndex].transform.SetParent (buildingButtons [lastIndex].transform.GetChild(0).transform, false);
		popupTexts[lastIndex].rectTransform.localPosition = new Vector2(0f, 0f);
	}

	void Start() {
		buildingsDisplays = new List<RectTransform>[names.Length];

		float y = 208f;
		for (int i = 0; i < names.Length; i++) {
			float x = 350f;

			addNewButton (x, y);

			buildingsDisplays [i] = new List<RectTransform> ();

//			buildingHolders.Add((RectTransform)Instantiate(buildingHolder, transform.position, transform. rotation));
//			buildingHolders [buildingHolders.Count - 1].transform.SetParent (renderCanvas.transform, false);
//			buildingHolders [buildingHolders.Count - 1].transform.localPosition = new Vector2 (x - 410f, y);
//			buildingHolders [buildingHolders.Count - 1].gameObject.SetActive (false);

			scrollOnMees.Add((RectTransform)Instantiate(scrollOnMe, transform.position, transform. rotation));
			scrollOnMees [scrollOnMees.Count - 1].transform.SetParent (renderCanvas.transform, false);
			scrollOnMees [scrollOnMees.Count - 1].localPosition = new Vector2 (-170f, y);
			scrollOnMees [scrollOnMees.Count - 1].gameObject.SetActive (false);

			buildingHolders.Add((RectTransform)Instantiate(buildingHolder, transform.position, transform. rotation));
			buildingHolders [buildingHolders.Count - 1].transform.SetParent (scrollOnMees[scrollOnMees.Count - 1].transform, false);
			buildingHolders [buildingHolders.Count - 1].transform.localPosition = new Vector2 (0f, 0f);

			ScrollRect scrollRect = scrollOnMees [scrollOnMees.Count - 1].GetComponent<ScrollRect>();
			scrollRect.content = buildingHolders [buildingHolders.Count - 1];

			y -= 62.4f;
		}
		buildingButtons[0].isButtonVisible = true;
		buildingButtons[1].isButtonVisible = true;
	}

	void Update() {
		for (int i = 0; i < buildingButtonLabelNames.Count; i++) {

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

			if (currentButton.isNameVisible) {
				buildingButtonLabelNames [i].text = names [i];

				popupTexts [i].text = currentButton.myName + " (" + gameStats.formatNumber(currentButton.price, 0) + " cookies)\n" + (currentButton.count == 0 ? "" : currentButton.getProductionDescriptionSingle() + "\n") + currentButton.getProductionDescriptionTotal();
				popupTexts [i].rectTransform.anchoredPosition = new Vector2 (0f, 0f);
			} else {
				buildingButtonLabelNames [i].text = "???";
				popupTexts [i].text = "??? (" + gameStats.formatNumber(currentButton.price, 0) + " cookies)";
			}
			buildingButtonLabelPrices [i].text = gameStats.formatNumber (currentButton.price, 0) + " cookies";


			buildingLevelTexts.ElementAt(i).text = buildingButtons[i].count == 0 ? "" : buildingButtons[i].count + "";

			if (gameStats.cookies >= buildingButtons.ElementAt (i).price) {
				buildingButtons[i].GetComponent<Image> ().color = new Color (0.7f, 0.7f, 0.7f);
				buildingButtonLabelNames.ElementAt (i).color = Color.white;
				buildingButtonLabelPrices [i].color = Color.green;
			} else {
				buildingButtons[i].GetComponent<Image> ().color = Color.gray;
				buildingButtonLabelNames.ElementAt (i).color = new Color (0.7f, 0.7f, 0.7f);
				buildingButtonLabelPrices [i].color = Color.red;
			}

			if (buildingButtons[i].count > buildingsDisplays [i].Count) {
				if (buildingsDisplays [i].Count == 0) // we just got a new building, the first of its kind
					scrollOnMees [i].gameObject.SetActive (true);
				buildingsDisplays[i].Add((RectTransform)Instantiate(building, transform.position, transform.rotation));
				buildingsDisplays[i][buildingsDisplays[i].Count - 1].transform.SetParent (buildingHolders [i].transform, false);
				buildingsDisplays[i][buildingsDisplays[i].Count - 1].gameObject.SetActive (true);
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