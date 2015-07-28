﻿using UnityEngine;
using System.Collections;
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

	private Button AvailButton0;
	private Button AvailButton1;
	private Button AvailButton2;
	private Button AvailButton3;
	private Button AvailButton4;

	public GameObject AvTime0;
	public GameObject AvTime1;
	public GameObject AvTime2;
	public GameObject AvTime3;
	public GameObject AvTime4;

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
		
//		--------------End Screen UI initialisation---------------------
		EndScreen = GameObject.FindWithTag ("EndScreen");
		AvailButton0 = EndScreen.transform.FindChild ("AvailButton0").gameObject.GetComponent<Button>();
		AvailButton0.onClick.AddListener (() => AvailButtonActive0 ());
		AvailButton1 = EndScreen.transform.FindChild ("AvailButton1").gameObject.GetComponent<Button>();
		AvailButton1.onClick.AddListener (() => AvailButtonActive1 ());
		AvailButton2 = EndScreen.transform.FindChild ("AvailButton2").gameObject.GetComponent<Button>();
		AvailButton2.onClick.AddListener (() => AvailButtonActive2 ());
		AvailButton3 = EndScreen.transform.FindChild ("AvailButton3").gameObject.GetComponent<Button>();
		AvailButton3.onClick.AddListener (() => AvailButtonActive3 ());
		AvailButton4 = EndScreen.transform.FindChild ("AvailButton4").gameObject.GetComponent<Button>();
		AvailButton4.onClick.AddListener (() => AvailButtonActive4 ());

//		AvTime0 = EndScreen.transform.FindChild ("AvTime0").gameObject.GetComponent<Button>();
//		AvTime1 = EndScreen.transform.FindChild ("AvTime1").gameObject.GetComponent<Button>();
//		AvTime2 = EndScreen.transform.FindChild ("AvTime2").gameObject.GetComponent<Button>();
//		AvTime3 = EndScreen.transform.FindChild ("AvTime3").gameObject.GetComponent<Button>();
//		AvTime4 = EndScreen.transform.FindChild ("AvTime4").gameObject.GetComponent<Button>();

		AvTime0.SetActive (false);
		AvTime1.SetActive (false);
		AvTime2.SetActive (false);
		AvTime3.SetActive (false);
		AvTime4.SetActive (false);
		EndScreen.SetActive (false);
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


	public void AvailButtonActive0() {
		AvTime0.SetActive (true);

		AvTime1.SetActive (false);
		AvTime2.SetActive (false);
		AvTime3.SetActive (false);
		AvTime4.SetActive (false);
	}
	public void AvailButtonActive1() {
		AvTime1.SetActive (true);

		AvTime0.SetActive (false);
		AvTime2.SetActive (false);
		AvTime3.SetActive (false);
		AvTime4.SetActive (false);
	}
	public void AvailButtonActive2() {
		AvTime2.SetActive (true);

		AvTime0.SetActive (false);
		AvTime1.SetActive (false);
		AvTime3.SetActive (false);
		AvTime4.SetActive (false);
		
	}
	public void AvailButtonActive3() {
		AvTime3.SetActive (true);

		AvTime0.SetActive (false);
		AvTime1.SetActive (false);
		AvTime2.SetActive (false);
		AvTime4.SetActive (false);
	}
	public void AvailButtonActive4() {
		AvTime4.SetActive (true);

		AvTime0.SetActive (false);
		AvTime1.SetActive (false);
		AvTime2.SetActive (false);
		AvTime3.SetActive (false);
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

