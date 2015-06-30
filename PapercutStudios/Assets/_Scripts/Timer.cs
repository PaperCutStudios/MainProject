using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public Text timerLabel;
	
	public float StartingCountDownTime;
	public float fCountDownTimer;
	private PRototyping UICommands;
	private float fInterval;
	private bool bTimerRunning;
	private bool ShownRule2;
	private bool ShownRule3;

	void Awake() {
		UICommands = FindObjectOfType<PRototyping>();
	}

	void Start () { 
		fCountDownTimer = StartingCountDownTime;
		fInterval = fCountDownTimer/3;
		bTimerRunning = true;
		ShownRule2 = false;
		ShownRule3 = false;
	}

	void Update () {
		if(bTimerRunning) {
			fCountDownTimer -= Time.deltaTime;
			
			float minutes = fCountDownTimer / 60;
			float seconds = fCountDownTimer % 60;
			//var fraction = (CountDownTime * 100) % 100;

			timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);

			if (fCountDownTimer <= StartingCountDownTime - fInterval && !ShownRule2) {
				UICommands.ShowNextRule();
				ShownRule2 = true;
			}

			if(fCountDownTimer <= StartingCountDownTime - (2*fInterval) && !ShownRule3) {
				UICommands.ShowNextRule();
				ShownRule3 = true;
			}

			if (fCountDownTimer <= 0.0f) 
			{
				UICommands.EndGame();
			}
		}
	}

	public void StopTimer () {
		bTimerRunning = false;
	}
}
