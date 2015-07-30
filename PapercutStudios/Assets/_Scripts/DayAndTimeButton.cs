using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayAndTimeButton{
	public Button dayButton;
	public Button[] timeButtons = new Button[4];
	public bool dayIsSelected = false;

	public DayAndTimeButton (Button daybutton) {
		dayButton = daybutton;
	}

	public void SetDaySelected(bool b) {
		dayIsSelected = b;
		if(dayIsSelected) {
			for(int i = 1; i< timeButtons.Length; i++) {
				timeButtons[i].gameObject.SetActive(true);
				dayButton.image.color = Color.gray;
			}
		}
		else {
			for(int i = 1; i< timeButtons.Length; i++) {
				timeButtons[i].gameObject.SetActive(false);
				dayButton.image.color = Color.white;
			}
		}
	}

	public void SetTimeSelected(int selectIndex) {
		foreach(Button bu in timeButtons) {
			bu.image.color = Color.white;
		}
		timeButtons[selectIndex].image.color = Color.gray;
	}
}
