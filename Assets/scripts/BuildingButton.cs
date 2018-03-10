using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour {

	public GameStats gameStats;

	public float priceIncrease = 1.15f;
	public int level = 0;

	public string myName;
	public float price;
	public float cookiesPerSecond;

	public Color color;
	public Renderer rend;

	public float availableIntensity;
	public float unavailableIntensity;

	public float[] availableIntensities;
	public float[] unavailableIntensities;

	float intensity;



	int NORMAL = 0;
	int HOVER = 1;
	int CLICK = 2;
	int mouseState = 0;


	void Start () {
		availableIntensity = 0.78f;
		unavailableIntensity = 0.51f;

		availableIntensities = new float[] {0.78f, 0.88f, 0.68f}; // normal, hover, click
		unavailableIntensities = new float[] {0.51f, 0.61f, 0.61f}; // normal, hover, click

		rend = GetComponent<Renderer>();
		rend.material.color = color;
	}

	void OnMouseDown() {
		
	}

	void OnMouseUp() {
		mouseState = NORMAL;
		if (gameStats.score >= price &&  IsPointerOverUIObject()) {
			gameStats.pointsPerSecond += cookiesPerSecond;
			gameStats.score -= price;
			level++;
			price = Mathf.CeilToInt (price * Mathf.Pow (priceIncrease, level));
		}
	}

	void OnMouseOver() {
		if (Input.GetMouseButton (0))
			mouseState = CLICK;
		else
			mouseState = HOVER;
	}

	void OnMouseExit() {
		if (!Input.GetMouseButton (0))
			mouseState = NORMAL;
	}

	void Update() {


		// DEBUG
//		transform.position = new Vector2(Random.value, Random.value);


		intensity = gameStats.score >= price ? availableIntensities[mouseState] : unavailableIntensities[mouseState]; // change color

		color = new Color(intensity, intensity, intensity); // change color
		rend.material.color = color; // set color
	}

	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
}

/*
 * Available: normal, hover, click
 * Unavailable: normal, hover
 */