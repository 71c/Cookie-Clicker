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
	public Canvas renderCanvas;
	public List<BuildingButton> buildingButtons = new List<BuildingButton>();
	public List<GameObject> buttonElementHolders = new List<GameObject> ();

	public List<Text> buildingButtonLabels = new List<Text> ();
	public List<Text> buildingLevelTexts = new List<Text> ();
	public List<Text> popupTexts = new List<Text> ();

	public GameStats gameStats;

	string[] names = new string[] { "cursor", "grandma", "farm", "mine", "factory", "bank", "temple", "wizard tower" };
	int[] prices = new int[] { 15, 100, 1100, 12000, 130000, 1400000, 20000000, 330000000 };
	double[] baseCookiesPerSeconds = new double[] { 0.1, 1.0, 8.0, 47.0, 260.0, 1400.0, 7800.0, 44000.0 };

	public List<RectTransform>[] buildingsDisplays;
	public List<RectTransform> buildingHolders;

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
		// set text to "0"
		setText (buildingLevelTexts.ElementAt (lastIndex), "0", 36);
		// set parent to element holder
		buildingLevelTexts.ElementAt(lastIndex).transform.SetParent(buttonElementHolders.ElementAt (lastIndex).transform, false);
		// set position
		buildingLevelTexts.ElementAt(lastIndex).transform.localPosition = new Vector2(120f, 0f);
		// set color
		buildingLevelTexts.ElementAt(lastIndex).color = Color.white;


		popupTexts.Add (Instantiate(popupText, transform.position, transform.rotation) as Text);
		popupTexts[lastIndex].transform.SetParent (buildingButtons [lastIndex].transform.GetChild(0).transform, false);
		popupTexts[lastIndex].rectTransform.localPosition = new Vector2(0f, 0f);
	}

	void Start() {
		buildingsDisplays = new List<RectTransform>[names.Length];
		for (int i = 0; i < buildingsDisplays.Length; i++) {
			buildingsDisplays [i] = new List<RectTransform> ();
		}

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

			if (currentButton.isNameVisible) {
				buildingButtonLabels [i].text = names [i] + "\n" + currentButton.price.ToString("N0") + " cookies";
				popupTexts [i].text = currentButton.myName + " (" + currentButton.price.ToString("N0") + " cookies)\n" + (currentButton.count == 0 ? "" : currentButton.getProductionDescriptionSingle() + "\n") + currentButton.getProductionDescriptionTotal();
//				
				popupTexts [i].rectTransform.anchoredPosition = new Vector2 (0f, 0f);
			} else {
				buildingButtonLabels [i].text = "??\n" + buildingButtons [i].price + " cookies";
				popupTexts [i].text = "??? (" + currentButton.price.ToString("N0") + " cookies)";
			}

			buildingLevelTexts.ElementAt(i).text = buildingButtons[i].count == 0 ? "" : buildingButtons[i].count + "";

			if (gameStats.cookies >= buildingButtons.ElementAt (i).price) {
				buildingButtons[i].GetComponent<Image> ().color = new Color (0.7f, 0.7f, 0.7f);
				buildingButtonLabels.ElementAt (i).color = Color.white;
			} else {
				buildingButtons[i].GetComponent<Image> ().color = Color.gray;
				buildingButtonLabels.ElementAt (i).color = new Color (0.7f, 0.7f, 0.7f);
			}

			if (buildingButtons[i].count > buildingsDisplays [i].Count) {
				if (buildingsDisplays [i].Count == 0) {
					buildingHolders.Add((RectTransform)Instantiate(buildingHolder, transform.position, transform. rotation));
					buildingHolders [buildingHolders.Count - 1].transform.SetParent (renderCanvas.transform, false);
					buildingHolders [buildingHolders.Count - 1].transform.position = new Vector2 (0f, 0f);
				}
				buildingsDisplays[i].Add((RectTransform)Instantiate(building, transform.position, transform.rotation));
				buildingsDisplays[i][buildingsDisplays[i].Count - 1].transform.SetParent (buildingHolders [i].transform, false);

				Vector2 buttonPosition = WorldToCanvasPosition (new RectTransform(), Camera.main, buttonElementHolders [i].transform.localPosition);
				buildingsDisplays[i][buildingsDisplays[i].Count - 1].transform.localPosition = new Vector2(buttonPosition.x, buttonPosition.y);



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

//	Vector2 pos = gameObject.transform.position;  // get the game object position
//	Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint
//
//	// set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
//	rectTransform.anchorMin = viewportPoint;  
//	rectTransform.anchorMax = viewportPoint;

//	public Vector2 worldPositionToCanvasPosition(Vector2 pos) {
//		RectTransform rectTransform;
//		RectTransform canvasRectT;
//		Transform objectToFollow;
//		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, objectToFollow.position);
//
//
//		rectTransform.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
//
//		return rectTransform.anchoredPosition;
//	}

//
//	public RectTransform canvasRectT;
//	public RectTransform healthBar;
//	public Transform objectToFollow;
//
//	Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, objectToFollow.position);
//	healthBar.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;

	private Vector2 WorldToCanvasPosition(RectTransform canvas, Camera camera, Vector2 position) {
		//Vector position (percentage from 0 to 1) considering camera size.
		//For example (0,0) is lower left, middle is (0.5,0.5)
		Vector2 temp = camera.WorldToViewportPoint(position);

		//Calculate position considering our percentage, using our canvas size
		//So if canvas size is (1100,500), and percentage is (0.5,0.5), current value will be (550,250)
		temp.x *= canvas.sizeDelta.x;
		temp.y *= canvas.sizeDelta.y;

		//The result is ready, but, this result is correct if canvas recttransform pivot is 0,0 - left lower corner.
		//But in reality its middle (0.5,0.5) by default, so we remove the amount considering cavnas rectransform pivot.
		//We could multiply with constant 0.5, but we will actually read the value, so if custom rect transform is passed(with custom pivot) , 
		//returned value will still be correct.

		temp.x -= canvas.sizeDelta.x * canvas.pivot.x;
		temp.y -= canvas.sizeDelta.y * canvas.pivot.y;

		return temp;
	}

}