using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
	private GameObject MainMenu;
	private GameObject EndScreen;
	private GameObject PlayerInfo;
	private GameObject ResultsScreen;

	private PlayerManager gameManager;

	private Text StatusDisplay;
	private Button PlayButton;
	private Button JoinButton;
	private Button[] PlayerButtons;
	private Button activeButton;

	private Image ResultsImage;
	public Sprite[] ActivitySprites;
	public Sprite[] PlayerSilhouettes;

	private List<DayAndTimeButton> dayAndTimeButtons = new List<DayAndTimeButton>();
	private List<ActivityButton> ActivityButtons = new List<ActivityButton> ();
	public Image SelectedImage;


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
//		JoinButton.onClick.AddListener(() => AttemptToJoin());
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
		StatusDisplay.text = "Waiting for Command...";
		
//		--------------End Screen UI initialisation---------------------
		EndScreen = GameObject.FindWithTag ("EndScreen");
		EndScreen.SetActive (false);
		//SelectedImage = EndScreen.transform.FindChild ("SelectedImage").gameObject.GetComponent<Image> ();

//		--------------Results Screen UI initialisation---------------------
		ResultsScreen = GameObject.FindWithTag ("ResultsScreen");
		ResultsScreen.SetActive (false);
	}

	#region Initiation related Functions
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
		System.Array.Sort(FoundWithTag, CompareObNames);
		for (int i = 0; i < FoundWithTag.Length; i++) {
			returnArray[i] = FoundWithTag[i].GetComponent<Button>();
		}
		return returnArray;
	}

	Text[] FindTextsWithTag(string searchTag) {
		GameObject[] FoundWithTag = GameObject.FindGameObjectsWithTag(searchTag);
		Text[] returnArray = new Text[FoundWithTag.Length];
		System.Array.Sort(FoundWithTag, CompareObNames);
		for (int i = 0; i < FoundWithTag.Length; i++) {
			returnArray[i] = FoundWithTag[i].GetComponent<Text>();
		}
		return returnArray;
	}

	int CompareObNames( GameObject x, GameObject y) {
		return x.name.CompareTo(y.name);
	}
	#endregion

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
	#region Main Menu Button Functions
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

	public void Quit() {
		Application.Quit();
	}

	void SetInfoScreenText() {
		for(int i = 0; i < Activities.Length; i++) {
			Activities[i].text = gameManager.GetActivityString(i);
			Availabilities[i].text = gameManager.GetAvailabilityString(i);
		}
	}

	void AttemptToJoin() {
		TCPClientManager.Instance.AttempToJoin();
		JoinButton.gameObject.SetActive(false);
	}

	public void ConnectionSuccess() {
		JoinButton.gameObject.SetActive(false);

		PlayButton.gameObject.SetActive(true);
	}

	public void ConnectionFailed(bool b) {
		JoinButton.gameObject.SetActive(true);
		if(b) {
			StatusDisplay.text = "Connection Failed!\nCould not connect to the Node Device";
		}
		else {
			StatusDisplay.text = "Connection Failed!\n4 Maximum Players ";
		}
	}
	#endregion
	#region Answer Screen Button Functions
	void ActivityButtonClick(ActivityButton ab, int actID) {
		foreach(ActivityButton abb in ActivityButtons) {
			abb.SetSelected(false);
//			SelectedImage.gameObject.SetActive(false);
		}
		ab.SetSelected(true);
		gameManager.SetAnswerActivity(actID);

		Image Selected = Instantiate (SelectedImage, new Vector2 (ab.activityButton.transform.position.x, ab.activityButton.transform.position.y), Quaternion.Euler(Vector3.zero)) as Image;
		Selected.transform.SetParent(GameObject.Find("Canvas").transform, true);
		
//		Selected.transform.position = ab.activityButton.transform.position;
//		Selected.transform.SetParent (ab.activityButton.gameObject.transform);
	}

	//Set any previously selected button to unselected, then select the passed through button
	void DayButtonClick(DayAndTimeButton dtb, int dayID) {
		for(int i = 0; i < dayAndTimeButtons.Count; i++) {
			dayAndTimeButtons[i].SetDaySelected(false);
		}
		dtb.SetDaySelected(true);
		gameManager.SetAnswerDay(dayID);

		Image Selected = Instantiate (SelectedImage, new Vector2 (dtb.dayButton.transform.position.x, dtb.dayButton.transform.position.y), Quaternion.Euler(Vector3.zero)) as Image;
		Selected.transform.SetParent(GameObject.Find("Canvas").transform, true);
	}

	void TimeButtonClick(DayAndTimeButton dtb, int buttonIndex, int timeID) {
		dtb.SetTimeSelected(buttonIndex);
		gameManager.SetAnswerTime(timeID);

		Image Selected = Instantiate (SelectedImage, new Vector2 (dtb.dayButton.transform.position.x, dtb.dayButton.transform.position.y), Quaternion.Euler(Vector3.zero)) as Image;
		Selected.transform.SetParent(GameObject.Find("Canvas").transform, true);
	}
	#endregion


	public void ShowEndResult(int numPlayers) {
		//read from gamemanager.answerIDs
		//compare answerIDs[0] to the strings stored on the xml, perform a switch on that statement(eg. case "ThemePark": prefab.image = ThemePark.jpg
		//for(inti=0;i<numplayers;i++) {person[i].gameobject.enabled  }
		//resultsscreen.setactive(true)
		ResultsScreen.SetActive (true);
		EndScreen.SetActive (false);

		ResultsImage = GameObject.FindWithTag ("ResultsImage").GetComponent<Image>();
		string answerActivity = XmlManager.Instance.GetActivityPiece (gameManager.GetAnswers()[0]);
		switch (answerActivity) {
		case "Arcade":
			ResultsImage.sprite = ActivitySprites[0];
			break;
		case "Aquarium":
			ResultsImage.sprite = ActivitySprites[1];
			break;
		case "Beach":
			ResultsImage.sprite = ActivitySprites[2];
			break;
		case "Cinema":
			ResultsImage.sprite = ActivitySprites[3];
			break;
		case "Pub":
			ResultsImage.sprite = ActivitySprites[4];
			break;
		case "Theme Park":
			ResultsImage.sprite = ActivitySprites[5];
			break;
		case "The Zoo":
			ResultsImage.sprite = ActivitySprites[6];
			break;
		default:
			Debug.Log("Default Case: you broke it....");
			break;
		}

		Image[] people = ResultsImage.GetComponentsInChildren<Image>();

		for (int i = 0; i < numPlayers; i++) 
		{
			int randomIndex = Random.Range(0,PlayerSilhouettes.Length);
			people[i+1].sprite = PlayerSilhouettes[randomIndex];
		}

		//gameManager.AnswerIDs [0] = xmlManager.actsFromXML [0];
	}


	public void OpenMainMenu() {
		Application.LoadLevel(0);
	}


}
