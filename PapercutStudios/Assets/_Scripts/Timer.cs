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
	public float fInterval1;
	public float fInterval2;
	private bool bTimerRunning;
	private bool ShownRule2;
	private bool ShownRule3;

	void Start () { 
		fCountDownTimer = GameObject.FindObjectOfType<PlayerManager>().GameTime;
		fInterval1 = ((fCountDownTimer/3)+Random.Range(-10,10));
		fInterval2 = ((fCountDownTimer/1.5f) +Random.Range(-10,10));
		bTimerRunning = true;
		ShownRule2 = false;
		ShownRule3 = false;
	}

	void Update () {
		if(bTimerRunning) {
			fCountDownTimer -= Time.deltaTime;
			if (fCountDownTimer <= StartingCountDownTime - fInterval1 && !ShownRule2) {
				UIManager.Instance.ShowNextRule();
				ShownRule2 = true;
			}
			if(fCountDownTimer <= StartingCountDownTime - fInterval2 && !ShownRule3) {
				UIManager.Instance.ShowNextRule();
				ShownRule3 = true;
			}
			if (fCountDownTimer <= 0.0f) 
			{
				fCountDownTimer = 0.0f;
				UIManager.Instance.EndGame();
				StopTimer();
			}			
			int minutes = Mathf.FloorToInt( fCountDownTimer / 60);
			int seconds = Mathf.FloorToInt (fCountDownTimer % 60);
			timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
		}
		if(!bTimerRunning) {
			timerLabel.color = new Color(((Mathf.Sin(Time.time * fFlashTime) + 1.0f)/2.0f), 0f,0f,1f);
		}
	}

	public void StopTimer () {
		if(bTimerRunning) {
			AudioSource.PlayClipAtPoint(ringing,transform.position);
			bTimerRunning = false;
		}
	}
}
