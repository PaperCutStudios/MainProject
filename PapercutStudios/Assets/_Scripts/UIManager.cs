using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
	private GameObject MainMenu;
	private GameObject EndScreen;
	private GameObject PlayerInfo;

	private PlayerManager gameManager;

	private Text StatusDisplay;
	private Button PlayButton;
	private Button JoinButton;

	private Button[] EndMenuDayButtons;
	private Button[] EndMenuTimesDay1Buttons;
	private Button[] EndMenuTimesDay2Buttons;
	private Button[] EndMenuTimesDay3Buttons;
	private Button[] EndMenuTimesDay4Buttons;
	private Button[] EndMenuTimesDay5Buttons;

	int iCurrentDisplayedRules = 0;

	Text[] Activities;
	Text[] Availabilities;
	Text[] Rules;

	void Start() {
		if (gameManager == null) {
			gameManager = FindObjectOfType<PlayerManager>();
		}

//		-------------Player Info Screen initialisation----------------
		PlayerInfo = GameObject.FindWithTag("PlayerInfo");
		Activities = FindTextWithTag("ActivityText");
		Availabilities = FindTextWithTag("AvailText");
		Rules = FindTextWithTag("RulesText");
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

		StatusDisplay = MainMenu.transform.FindChild("StatusDisplay").gameObject.GetComponent<Text>();
		
//		--------------End Screen UI initialisation---------------------
		EndScreen = GameObject.FindWithTag ("EndScreen");

		EndMenuDayButtons = FindButtonsWithTag("DayButton");


		EndScreen.SetActive (false);
	}

	void SetEndMenuStuff() {
		for(int i = 0; i < EndMenuDayButtons.Length; i++){ 
			EndMenuDayButtons[i].onClick.AddListener(() => AvailabilityButton(gameManager.ptActiveTable.Availabilities[i].baseValues[0]));
			Debug.Log(gameManager.ptActiveTable.Availabilities[i].sDay);
			Text buttonText = EndMenuDayButtons[i].transform.FindChild("text").GetComponent<Text>();
			buttonText.text = "FUCK";
//			buttonText.text = gameManager.ptActiveTable.Availabilities[i].sDay;
			
			switch(i) {
			case 0:
				EndMenuTimesDay1Buttons = EndMenuDayButtons[i].GetComponentsInChildren<Button>();
				SetTimebuttonStrings(EndMenuTimesDay1Buttons);
				break;
			case 1:
				EndMenuTimesDay2Buttons = EndMenuDayButtons[i].GetComponentsInChildren<Button>();
				SetTimebuttonStrings(EndMenuTimesDay2Buttons);
				break;
			case 2:
				EndMenuTimesDay3Buttons = EndMenuDayButtons[i].GetComponentsInChildren<Button>();
				SetTimebuttonStrings(EndMenuTimesDay3Buttons);
				break;
			case 3:
				EndMenuTimesDay4Buttons = EndMenuDayButtons[i].GetComponentsInChildren<Button>();
				SetTimebuttonStrings(EndMenuTimesDay4Buttons);
				break;
			case 4:
				EndMenuTimesDay5Buttons = EndMenuDayButtons[i].GetComponentsInChildren<Button>();
				SetTimebuttonStrings(EndMenuTimesDay5Buttons);
				break;
			default:
				Debug.Log("error");
				break;
			}
		}
	}

	void SetTimebuttonStrings(Button[] buttons) {
		for(int i = 1; i <buttons.Length; i++) {
				buttons[i].onClick.AddListener(() => TimeButton(gameManager.ptActiveTable.Availabilities[i].baseValues[1]));
				if(gameManager.ptActiveTable.Availabilities[i].iAvailableHours[i] >= 12){
					buttons[i].GetComponentInChildren<Text>().text = gameManager.ptActiveTable.Availabilities[i].iAvailableHours[i].ToString() + "pm.";
				} else {
					buttons[i].GetComponentInChildren<Text>().text = gameManager.ptActiveTable.Availabilities[i].iAvailableHours[i].ToString() + "am.";
				}
			}
	}

	Text[] FindTextWithTag(string searchTag) {
		GameObject[] FoundWithTag = GameObject.FindGameObjectsWithTag(searchTag);
		Text[] returnArray = new Text[FoundWithTag.Length];
		Array.Sort(FoundWithTag, CompareObNames);
		for (int i = 0; i < FoundWithTag.Length; i++) {
			returnArray[i] = FoundWithTag[i].GetComponent<Text>();
		}
		return returnArray;
	}

	Button[] FindButtonsWithTag(string searchTag) {
		GameObject[] FoundWithTag = GameObject.FindGameObjectsWithTag(searchTag);
		Button[] returnArray = new Button[FoundWithTag.Length];
		Array.Sort(FoundWithTag, CompareObNames);
		for (int i = 0; i < FoundWithTag.Length; i++) {
			returnArray[i] = FoundWithTag[i].GetComponent<Button>();
		}

		if(returnArray.Length <= 0) {
			Debug.Log("ZERO!!!");
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
		SetEndMenuStuff();
		EndScreen.SetActive (true);
	}

	public void MainMenuPlay() {
		MainMenu.SetActive(false);
		PlayerInfo.SetActive(true);
	}

	public void ConnectionSuccess() {
		PlayButton.gameObject.SetActive(true);
		JoinButton.gameObject.SetActive(false);
		gameManager.SetUpPlayerInformation();
		SetInfoScreenText();
		ShowNextRule();
	}

	void AvailabilityButton(int DayIndex) {
		gameManager.AnswerIDs[0] = DayIndex;

	}

	void ActivityButton(int ActIndex) {
		gameManager.AnswerIDs[1] = ActIndex;

	}

	void TimeButton(int TimeIndex) {
		gameManager.AnswerIDs[2] = TimeIndex;

	}

	public void Quit() {
		Application.Quit();
	}

	void SetInfoScreenText() {
		for(int i = 0; i < Activities.Length; i++) {
			Activities[i].text = gameManager.GetActivityString(i);
			Availabilities[i].text = gameManager.GetAvailabilityString(i);
		}
	}
}

