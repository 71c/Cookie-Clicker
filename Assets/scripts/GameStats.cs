using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

	public Text scoreText;
	public Text pointsPerSecondText;

	private System.Threading.Timer timer;

	public float score = 0;
	public int pointsPerClick = 1;
	public float pointsPerSecond = 0f;
	float oldPoinstPerSecond = 0f;


	void Start () {
		timer = new System.Threading.Timer (UpdateProperty);
	}

	private void UpdateProperty(object state) {
		lock(this) {
			score += 1f;
		}
	}

	void Update () {
		scoreText.text = "Cookies: " + score;
		pointsPerSecondText.text = "CpS: " + pointsPerSecond;

		if (oldPoinstPerSecond != pointsPerSecond && pointsPerSecond != 0f) {
			timer.Change ((int)(1000f / pointsPerSecond), (int)(1000f / pointsPerSecond));
			oldPoinstPerSecond = pointsPerSecond;
		}
	}
}