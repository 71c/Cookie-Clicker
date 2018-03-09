using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InstantiateButtons : MonoBehaviour
{
	public UpgradeButton button;
	public Text text;
	public Canvas renderCanvas;
	public List<UpgradeButton> upgradeButtons = new List<UpgradeButton>();
	public List<Text> buttonLabels = new List<Text> ();

	public GameStats gameStats;

	string[] names = new string[] { "cursor", "grandma", "farm", "mine", "factory", "bank" };
	float[] prices = new float[] { 15, 100, 1100, 12000, 130000, 1400000 };
	float[] baseCookiesPerSeconds = new float[] { 0.1f, 1f, 8f, 47f, 260f, 1400f };

	void Start() {
		for (float y = 3.45f; y > -4.3f; y -= 2.14f) {
			int lastIndex = upgradeButtons.Count;

			createButton (names [lastIndex], prices [lastIndex], baseCookiesPerSeconds [lastIndex]);
			upgradeButtons.Add((UpgradeButton)Instantiate(button, new Vector2 (6.15f, y), transform.rotation));



			buttonLabels.Add(Instantiate(text, new Vector2 (6.15f * 52f, y * 52f), transform.rotation) as Text);

			buttonLabels.ElementAt(lastIndex).transform.SetParent(renderCanvas.transform, false); //Parent to the panel
			buttonLabels.ElementAt(lastIndex).fontSize = 18; //Set the text box's text element font size and style:
			buttonLabels.ElementAt(lastIndex).text = "lala"; //Set the text box's text element to the current textToDisplay:
		}
	}

	void Update() {
		for (int i = 0; i < buttonLabels.Count; i++) {
			buttonLabels.ElementAt(i).text = names[i] + " (+" + upgradeButtons[i].cookiesPerSecond + " CpS)\n" + upgradeButtons[i].price + " cookies";
		}
	}

	void createButton(string newName, float newPrice, float newCPS) {
		button.myName = newName;
		button.price = newPrice;
		button.cookiesPerSecond = newCPS;
	}
}