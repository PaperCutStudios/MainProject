using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System;

public class UIManager : Singleton<UIManager> {
	private GameObject MainMenu;
	private GameObject EndScreen;
	private GameObject PlayerInfo;

	private PlayerManager gameManager;

	private Text StatusDisplay;
	private Button PlayButton;
	private Button JoinButton;
	private Button[] PlayerButtons;
	private Button activeButton;

	private List<DayAndTimeButton> dayAndTimeButtons = new List<DayAndTimeButton>();
	private List<ActivityButton> ActivityButtons = new List<ActivityButton> ();

//	public GameObject AvTime0;
//	public GameObject AvTime1;
//	public GameObject AvTime2;
//	public GameObject AvTime3;
//	public GameObject AvTime4;

	public int iCurrentDisplayedRules = 0;

	Text[] Activities;
	Text[] Availabilities;
	Text[] Rules;

	void Start() {
		if (gameManager == null) {
			gameManager = FindObjectOfType<PlayerManager>();
		}

//		-------------Player Info Screen initialisation----------------
		PlayerInfo = GameObject.FindWithTag("PlayerInfo");
		Activities = FindTextsWithTag("ActivityText");
		Availabilities = FindTextsWithTag("AvailText");
		Rules = FindTextsWithTag("RulesText");
		foreach(Text element in Rules) {
			element.text = "";
		}
		PlayerInfo.SetActive(false);

//		--------------Main Menu UI initialisation---------------------
		MainMenu = GameObject.FindWithTag("MainMenu");
		JoinButton = MainMenu.transform.FindChild("JoinButton").gameObject.GetComponent<Button>();
		JoinButton.onClick.AddListener(() => ConnectionSuccess());
//		JoinButton.onClick.AddListener(() => TCPClientManager.Instance.AttempToJoin());
		JoinButton.gameObject.SetActive(true);

		PlayButton = MainMenu.transform.FindChild("PlayButton").gameObject.GetComponent<Button>();
		PlayButton.onClick.AddListener(() => MainMenuPlay());
		PlayButton.gameObject.SetActive(false);

		//will be removed when networking is working
		PlayerButtons = new Button[] { 	MainMenu.transform.FindChild("Player1Button").GetComponent<Button>(), 
										MainMenu.transform.FindChild("Player2Button").GetComponent<Button>(),
										MainMenu.transform.FindChild("Player3Button").GetComponent<Button>(),
										MainMenu.transform.FindChild("Player4Button").GetComponent<Button>()
		};
		activeButton = PlayerButtons[0];

		for(int i = 0; i<PlayerButtons.Length;i++) {
			int captured = i;
			PlayerButtons[i].onClick.AddListener(()=> gameManager.SetPlayerNum(captured+1));
			PlayerButtons[i].onClick.AddListener(()=> SetActivePlayerButton(PlayerButtons[captured]));

		}

		StatusDisplay = MainMenu.transform.FindChild("StatusDisplay").gameObject.GetComponent<Text>();
		
//		--------------End Screen UI initialisation---------------------
		EndScreen = GameObject.FindWithTag ("EndScreen");
		EndScreen.SetActive (false);
	}


	void SetEndMenuStuff() {

		//Setting of each Day and Time button in the scene
		Button[] tempButtons = FindButtonsWithTag ("DayButton");
		Debug.Log (tempButtons.Length.ToString ());
		for(int i = 0; i < tempButtons.Length; i++) {
			// Set up the Day Button with a listener and its text
			dayAndTimeButtons.Add(new DayAndTimeButton(tempButtons[i]));
			dayAndTimeButtons[i].dayButton.GetComponentInChildren<Text>().text = gameManager.ptActiveTable.Availabilities[i].sDay;
			//when iterating through, we gotta capture the value of the iterator before we pass it to the listener otherwise it will pass through the final value of the loop
			int cap = i;
			dayAndTimeButtons[i].dayButton.onClick.AddListener(() => DayButtonClick(dayAndTimeButtons[cap],gameManager.ptActiveTable.Availabilities[cap].baseValues[0]));

			// Set up each time button for this day button.
			// Starts at one because getcomponents in children returns the button this is called on as well as the child buttons
			Button[] childButtons = dayAndTimeButtons[i].dayButton.GetComponentsInChildren<Button>();
			dayAndTimeButtons[i].timeButtons = childButtons;
			for (int j = 1; j < childButtons.Length; j++) {

				//set the text on each button, based on the time it relates to. "j-1" is used as Available hours is only 4 long
				int time = gameManager.ptActiveTable.Availabilities[i].iAvailableHours[j-1];
				Text timeButtonText = dayAndTimeButtons[i].timeButtons[j].GetComponentInChildren<Text>();
				if(time> 12){
					timeButtonText.text = (time-12).ToString() + " pm";
				}else if (time == 12){
					timeButtonText.text = time.ToString() + " pm";
				}else if (time < 12) {
					timeButtonText.text = time.ToString() + " am";
				}else if (time == 0) {
					timeButtonText.text = "12 am";
				}

				// Set the listener on each time buttons, again capturing the value of the iterator
				// this is slightly different in that, when sending the answers back and forth, we need to tell each player at what time exactly each person has gone somewhere
				// eg. player one goes to the Bar at 1pm. player 2 goes to the bar at 2pm. Seeing as though they both went at different times, they wont be shown arriving at the same time (the goal of the game)
				int captime = j;
				dayAndTimeButtons[i].timeButtons[j].onClick.AddListener(()=> TimeButtonClick(dayAndTimeButtons[cap], captime, time));
			}
		}
	
		//Setting of activity buttons
		Button[] tempbuttons = FindButtonsWithTag ("ActivityButton");
		for (int i = 0; i < tempbuttons.Length; i++){
			ActivityButtons.Add(new ActivityButton(tempbuttons[i]));
			ActivityButtons[i].activityButton.GetComponentInChildren<Text>().text = gameManager.ptActiveTable.Activities[i].EventName;
			int cap = i;
			ActivityButtons[i].activityButton.onClick.AddListener(() => ActivityButtonClick(ActivityButtons[cap],gameManager.ptActiveTable.Activities[cap].baseValues[0]));
		}
	}

	void SetActivePlayerButton(Button pressed) {
		activeButton.image.color = Color.white;
		activeButton = pressed;
		activeButton.image.color = Color.grey;
	}

	Button[] FindButtonsWithTag(string searchTag) {
		GameObject[] FoundWithTag = GameObject.FindGameObjectsWithTag(searchTag);
		Button[] returnArray = new Button[FoundWithTag.Length];
		Array.Sort(FoundWithTag, CompareObNames);
		for (int i = 0; i < FoundWithTag.Length; i++) {
			returnArray[i] = FoundWithTag[i].GetComponent<Button>();
		}
		return returnArray;
	}

	Text[] FindTextsWithTag(string searchTag) {
		GameObject[] FoundWithTag = GameObject.FindGameObjectsWithTag(searchTag);
		Text[] returnArray = new Text[FoundWithTag.Length];
		Array.Sort(FoundWithTag, CompareObNames);
		for (int i = 0; i < FoundWithTag.Length; i++) {
			returnArray[i] = FoundWithTag[i].GetComponent<Text>();
		}
		return returnArray;
	}

	int CompareObNames( GameObject x, GameObject y) {
		return x.name.CompareTo(y.name);
	}

	public void ShowNextRule () {
		Rules[iCurrentDisplayedRules].text = gameManager.GetRuleString(iCurrentDisplayedRules);
		iCurrentDisplayedRules++;
	}

	public void EndGame() {
		PlayerInfo.SetActive(false);
		EndScreen.SetActive (true);
		//ensure that the time buttons are all hidden away when opening the end screen
		foreach(DayAndTimeButton dtb in dayAndTimeButtons) {
			for (int i = 1; i < dtb.timeButtons.Length; i++) {
				dtb.timeButtons[i].gameObject.SetActive(false);
			}
		}
	}

	public void MainMenuPlay() {
		MainMenu.SetActive(false);
		PlayerInfo.SetActive(true);
		EndScreen.SetActive (true);
		gameManager.SetUpPlayerInformation();
		SetEndMenuStuff ();
		EndScreen.SetActive (false);
		SetInfoScreenText();
		ShowNextRule();
	}

	public void ConnectionSuccess() {
		PlayButton.gameObject.SetActive(true);
		JoinButton.gameObject.SetActive(false);
	}

	void ActivityButtonClick(ActivityButton ab, int actID) {
		foreach(ActivityButton abb in ActivityButtons) {
			abb.SetSelected(false);
		}
		ab.SetSelected(true);
		gameManager.SetAnswerActivity(actID);
	}

	//Set any previously selected button to unselected, then select the passed through button
	void DayButtonClick(DayAndTimeButton dtb, int dayID) {
		for(int i = 0; i < dayAndTimeButtons.Count; i++) {
			dayAndTimeButtons[i].SetDaySelected(false);
		}
		dtb.SetDaySelected(true);
		gameManager.SetAnswerDay(dayID);
	}

	void TimeButtonClick(DayAndTimeButton dtb, int buttonIndex, int timeID) {
		dtb.SetTimeSelected(buttonIndex);
		gameManager.SetAnswerTime(timeID);
	}

	public void Quit() {
		Application.Quit();
	}

	public void OpenMainMenu() {
		Application.LoadLevel(0);
	}

	void SetInfoScreenText() {
		for(int i = 0; i < Activities.Length; i++) {
			Activities[i].text = gameManager.GetActivityString(i);
			Availabilities[i].text = gameManager.GetAvailabilityString(i);
		}
	}
}
