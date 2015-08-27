using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public Text timerLabel;
	public float StartingCountDownTime;
	public float fCountDownTimer;
	public float fFlashTime;
	public AudioClip ringing;
	private float fCurrentFlashTimer;
	private float fInterval;
	private bool bTimerRunning;
	private bool ShownRule2;
	private bool ShownRule3;

	void Start () { 
		fCountDownTimer = GameObject.FindObjectOfType<PlayerManager>().GameTime;
		fInterval = Mathf.Round((((fCountDownTimer/(3+Random.Range(-0.25f,0.25f)))*100f)/100f));
		bTimerRunning = true;
		ShownRule2 = false;
		ShownRule3 = false;
	}

	void Update () {
		if(bTimerRunning) {
			fCountDownTimer -= Time.deltaTime;
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
				fCountDownTimer = 0.0f;
				UIManager.Instance.EndGame();
				AudioSource.PlayClipAtPoint(ringing,transform.position);
				StopTimer();
			}			
			int minutes = Mathf.FloorToInt( fCountDownTimer / 60);
			int seconds = Mathf.FloorToInt (fCountDownTimer % 60);
			timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
		}
		if(!bTimerRunning) {
			timerLabel.color = new Color(((Mathf.Sin(Time.time * fFlashTime) + 1.0f)/2.0f), 0f,0f,1f);
//			if(fCurrentFlashTimer <= 0f) {
//
//				timerLabel.color = Color.red;
//				fCurrentFlashTimer = fFlashTime;
//			}
//			if(fCurrentFlashTimer > 0f) {
//				Debug.Log("Flash Black");
//				timerLabel.color = Color.black;
//				fCurrentFlashTimer -= Time.deltaTime;
//			}
		}
	}

	public void StopTimer () {
		if(bTimerRunning) {
			bTimerRunning = false;
		}
	}
}
