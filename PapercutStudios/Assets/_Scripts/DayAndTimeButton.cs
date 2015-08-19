using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DayAndTimeButton{
	public Button dayButton;
	public int dayID;
	public List<Button> timeButtons = new List<Button> ();
	public bool dayIsSelected = false;

	public DayAndTimeButton (Button daybutton, int dID) {
		dayButton = daybutton;
		dayID = dID;
	}

	public void SetDaySelected(bool b) {
		dayIsSelected = b;
		if(dayIsSelected) {
			for(int i = 1; i< timeButtons.Count; i++) {
				dayButton.image.sprite = UIManager.Instance.SelectedImage;
			}
		}
		else {
			for(int i = 1; i< timeButtons.Count; i++) {
				dayButton.image.sprite = UIManager.Instance.DeSelecectedImage;
				foreach(Button bu in timeButtons) {
					bu.image.sprite = UIManager.Instance.DeSelecectedImage;
				}
			}
		}
	}

	public void DeselectTimes() {
		foreach(Button bu in timeButtons) {
			bu.image.sprite = UIManager.Instance.DeSelecectedImage;
		}
	}

	public void SetTimeSelected(int selectIndex) {
		foreach(Button bu in timeButtons) {
			bu.image.sprite = UIManager.Instance.DeSelecectedImage;
		}
		timeButtons[selectIndex].image.sprite = UIManager.Instance.SelectedImage;
	}
}
