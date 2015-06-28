using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public Text timerLabel;
	
	public float CountDownTime;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		CountDownTime -= Time.deltaTime;
		
		var minutes = CountDownTime / 60;
		var seconds = CountDownTime % 60;
		//var fraction = (CountDownTime * 100) % 100;

		timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);

		if (CountDownTime <= 0.0f) 
		{
			Application.LoadLevel(1);
		}
	}
}
