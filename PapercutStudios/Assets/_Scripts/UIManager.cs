using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
	private GameObject MainMenu;
	private GameObject PlayerInfo;

	private PlayerManager gameManager;

	private Text StatusDisplay;
	private Button PlayButton;
	private Button JoinButton;

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
		Activities = FindTextGUIObjs("ActivityText");
		Availabilities = FindTextGUIObjs("AvailText");
		Rules = FindTextGUIObjs("RulesText");
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


	}

	Text[] FindTextGUIObjs(string searchTag) {
		GameObject[] FoundWithTag = GameObject.FindGameObjectsWithTag(searchTag);
		Text[] returnArray = new Text[FoundWithTag.Length];
		for (int i = 0; i < FoundWithTag.Length; i++) {
			returnArray[i] = FoundWithTag[i].GetComponent<Text>();
		}
		Array.Sort(returnArray, CompareObNames);
		return returnArray;
	}

	int CompareObNames( Text x, Text y) {
		return x.gameObject.name.CompareTo(y.gameObject.name);
	}

	public void ShowNextRule () {
		Rules[iCurrentDisplayedRules].text = gameManager.GetRuleString(iCurrentDisplayedRules);
		iCurrentDisplayedRules++;
	}

	public void EndGame() {
		PlayerInfo.SetActive(false);
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

