﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public Text timerLabel;
	
	public float StartingCountDownTime;
	public float fCountDownTimer;
	private float fInterval;
	private bool bTimerRunning;
	private bool ShownRule2;
	private bool ShownRule3;

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
			
			int minutes = Mathf.FloorToInt( fCountDownTimer / 60);
			int seconds = Mathf.FloorToInt (fCountDownTimer % 60);
			//var fraction = (CountDownTime * 100) % 100;

			timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);

			if (fCountDownTimer <= StartingCountDownTime - fInterval && !ShownRule2) {
				UIManager.Instance.ShowNextRule();
				ShownRule2 = true;
			}

			if(fCountDownTimer <= StartingCountDownTime - (2*fInterval) && !ShownRule3) {
				UIManager.Instance.ShowNextRule();
				ShownRule3 = true;
			}

			if (fCountDownTimer <= 0.0f) 
			{
				UIManager.Instance.EndGame();
				bTimerRunning = false;
			}
		}
	}

	public void StopTimer () {
		bTimerRunning = false;
		timerLabel.gameObject.SetActive(false);
	}
}
