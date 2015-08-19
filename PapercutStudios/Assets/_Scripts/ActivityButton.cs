using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivityButton{

	public Button activityButton;
	public bool isSelected = false;

	public ActivityButton (Button ActButton){
		activityButton = ActButton;
		}

	public void SetSelected (bool b) {
		isSelected = b;
		if(isSelected) {
			activityButton.image.sprite = UIManager.Instance.SelectedImage;
		}
		else {
			activityButton.image.sprite = UIManager.Instance.DeSelecectedImage;
		}

	}
}
