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

	private Button AvailButton0;
	private Button AvailButton1;
	private Button AvailButton2;
	private Button AvailButton3;
	private Button AvailButton4;

	public Text ActText0;
	public Text ActText1;
	public Text ActText2;
	public Text ActText3;
	public Text ActText4;

	public GameObject AvTime0;
	public GameObject AvTime1;
	public GameObject AvTime2;
	public GameObject AvTime3;
	public GameObject AvTime4;

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


//		AvTime0 = EndScreen.transform.FindChild ("AvTime0").gameObject.GetComponent<Button>();
//		AvTime1 = EndScreen.transform.FindChild ("AvTime1").gameObject.GetComponent<Button>();
//		AvTime2 = EndScreen.transform.FindChild ("AvTime2").gameObject.GetComponent<Button>();
//		AvTime3 = EndScreen.transform.FindChild ("AvTime3").gameObject.GetComponent<Button>();
//		AvTime4 = EndScreen.transform.FindChild ("AvTime4").gameObject.GetComponent<Button>();

		//ActText0.text = gameManager.ptActiveTable.Availabilities[i].iAvailableHours[0].toString();
		//ActText0.text = "Worked";
		EndScreen.SetActive (false);
	}

	void SetEndMenuStuff() {
		Button[] tempButtons = FindButtonsWithTag ("DayButton");
		Debug.Log (tempButtons.Length.ToString ());
		for(int i = 0; i < tempButtons.Length; i++) {
			dayAndTimeButtons.Add(new DayAndTimeButton(tempButtons[i]));
			dayAndTimeButtons[i].dayButton.GetComponentInChildren<Text>().text = gameManager.ptActiveTable.Availabilities[i].sDay;
			Button[] childButtons = dayAndTimeButtons[i].dayButton.GetComponentsInChildren<Button>();
			dayAndTimeButtons[i].timeButtons = childButtons;
			for (int j = 1; j < childButtons.Length; j++) {
				dayAndTimeButtons[i].timeButtons[j].GetComponentInChildren<Text>().text = gameManager.ptActiveTable.Availabilities[i].iAvailableHours[j-1].ToString();
				if(gameManager.ptActiveTable.Availabilities[i].iAvailableHours[j-1]> 12){
					dayAndTimeButtons[i].timeButtons[j].GetComponentInChildren<Text>().text = (gameManager.ptActiveTable.Availabilities[i].iAvailableHours[j-1]-12).ToString() + " pm";
				}else if (gameManager.ptActiveTable.Availabilities[i].iAvailableHours[j-1] == 12){
					dayAndTimeButtons[i].timeButtons[j].GetComponentInChildren<Text>().text = gameManager.ptActiveTable.Availabilities[i].iAvailableHours[j-1].ToString() + " pm";
				}else if (gameManager.ptActiveTable.Availabilities[i].iAvailableHours[j-1] < 12) {
					dayAndTimeButtons[i].timeButtons[j].GetComponentInChildren<Text>().text = gameManager.ptActiveTable.Availabilities[i].iAvailableHours[j-1].ToString() + " am";
				}
			}
		}
	}

	void SetActivePlayerButton(Button pressed) {
		activeButton.image.color = pressed.image.color;
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

	Text[] FindTextGUIObjs(string searchTag) {
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

