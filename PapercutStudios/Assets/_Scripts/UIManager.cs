using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
	private GameObject MainMenu;
	private GameObject GameScreen;
	private GameObject ResultsScreen;

	private PlayerManager gameManager;

	private Text StatusDisplay;
	private Button PlayButton;
	private Button JoinButton;
	private Button[] PlayerButtons;
	private Button activeButton;
	private Button submitAnswersButton;
	private Button[] DayButtons;
	private Dictionary<string,DayAndTimeButton> AvailabilityInfoButtons = new Dictionary<string,DayAndTimeButton>();


	private Image ResultsImage;
	public Sprite[] ActivitySprites;
	public Sprite[] PlayerSilhouettes;

	private List<DayAndTimeButton> dayAndTimeButtons = new List<DayAndTimeButton>();
	private List<ActivityButton> ActivityButtons = new List<ActivityButton> ();
	public Sprite SelectedImage;
	public Sprite DeSelecectedImage;


	public int iCurrentDisplayedRules = 0;

	Text[] Activities;
	Text[] Availabilities;
	Text[] Rules;

	void Start() {
		if (gameManager == null) {
			gameManager = FindObjectOfType<PlayerManager>();
		}

//		-------------Player Info Screen initialisation----------------
		GameScreen = GameObject.FindWithTag("GameScreen");

//		Activities = FindTextsWithTag("ActivityText");
//		Availabilities = FindTextsWithTag("AvailText");
		Rules = FindTextsWithTag("RulesText");
		foreach(Text element in Rules) {
			element.text = "";
		}
		GameScreen.SetActive(false);

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
//		EndScreen = GameObject.FindWithTag ("EndScreen");
//		SetAvailabilityDisplay ();
//		submitAnswersButton = EndScreen.transform.FindChild("SubmitAnswer").gameObject.GetComponent<Button>();
//		submitAnswersButton.onClick.AddListener(() => SubmitAnswersButtonClick());
//		submitAnswersButton.gameObject.SetActive(false);
//		EndScreen.SetActive (false);
//		//SelectedImage = EndScreen.transform.FindChild ("SelectedImage").gameObject.GetComponent<Image> ();

//		--------------Results Screen UI initialisation---------------------
		ResultsScreen = GameObject.FindWithTag ("ResultsScreen");
		ResultsScreen.SetActive (false);
	}

	#region Initiation related Functions
	void SetAvailabilityDisplay() {
		for(int i = 0; i < gameManager.ptActiveTable.Availabilities.Count;i++) {
			string day = gameManager.ptActiveTable.Availabilities[i].sDay;
			int cap = i;
			AvailabilityInfoButtons.Add(day, new DayAndTimeButton(GameScreen.transform.Find("Days/Button"+day).gameObject.GetComponent<Button> (), gameManager.ptActiveTable.Availabilities[cap].baseValues[0]));
			AvailabilityInfoButtons[day].dayButton.GetComponentInChildren<Text>().text = day;
			AvailabilityInfoButtons[day].timeButtons = new List<Button>(AvailabilityInfoButtons[day].dayButton.GetComponentsInChildren<Button>());
			AvailabilityInfoButtons[day].timeButtons.RemoveAt(0);
			for(int j = 0; j < AvailabilityInfoButtons[day].timeButtons.Count; j++) {
				int time = gameManager.ptActiveTable.Availabilities[i].iAvailableHours[j];
				Text timeButtonText = AvailabilityInfoButtons[day].timeButtons[j].GetComponentInChildren<Text>();
				if(time> 12){
					timeButtonText.text = (time-12).ToString() + " pm";
				}
				else if (time == 12){
					timeButtonText.text = time.ToString() + " pm";
				}
				else if (time == 0) {
					timeButtonText.text = "12 am";
				}
				else if (time < 12) {
					timeButtonText.text = time.ToString() + " am";
				}
			}
		}
		
		Button[] tempbuttons = FindButtonsWithTag ("ActivityButton");
		for (int i = 0; i < tempbuttons.Length; i++){
			ActivityButtons.Add(new ActivityButton(tempbuttons[i]));
			ActivityButtons[i].activityButton.GetComponentInChildren<Text>().text = gameManager.ptActiveTable.Activities[i].GetAsString();
//			int cap = i;
//			ActivityButtons[i].activityButton.onClick.AddListener(() => ActivityButtonClick(ActivityButtons[cap],gameManager.ptActiveTable.Activities[cap].baseValues[0]));
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
//		PlayerInfo.SetActive(false);
		for (int i = 0; i < AvailabilityInfoButtons.Count; i++) {
			var item = AvailabilityInfoButtons.ElementAt(i);
			var itemValue = item.Value;
			itemValue.dayButton.onClick.AddListener(() => DayButtonClick(itemValue));
			for (int j = 0; j < itemValue.timeButtons.Count; j++) {
				int capj = j;
				int time = gameManager.ptActiveTable.Availabilities[i].iAvailableHours[j];
				itemValue.timeButtons[j].onClick.AddListener(() => TimeButtonClick(itemValue,capj, time));
			}
		}
//		EndScreen.SetActive (true);
		//ensure that the time buttons are all hidden away when opening the end screen

//		foreach(KeyValuePair<string,DayAndTimeButton> entry in AvailabilityInfoButtons) {
//			entry.Value.dayButton.onClick.AddListener(()=> DayButtonClick(entry,gameManager.ptActiveTable.Availabilities[cap].baseValues[0]));
//		}
	}
	#region Main Menu Button Functions
	public void MainMenuPlay() {
		MainMenu.SetActive(false);
		GameScreen.SetActive(true);
//		EndScreen.SetActive (true);
		gameManager.SetUpPlayerInformation();
		SetAvailabilityDisplay ();
//		EndScreen.SetActive (false);
		ShowNextRule();
	}

	public void Quit() {
		Application.Quit();
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
		bool showSubmit = gameManager.SetAnswerActivity(actID);
		if(showSubmit){
			submitAnswersButton.gameObject.SetActive(true);
		}

//		Image Selected = Instantiate (SelectedImage, new Vector2 (ab.activityButton.transform.position.x, ab.activityButton.transform.position.y), Quaternion.Euler(Vector3.zero)) as Image;
//		Selected.transform.SetParent(GameObject.Find("Canvas").transform, true);
		
//		Selected.transform.position = ab.activityButton.transform.position;
//		Selected.transform.SetParent (ab.activityButton.gameObject.transform);
	}

	//Set any previously selected button to unselected, then select the passed through button
	void DayButtonClick(DayAndTimeButton dtb) {
		foreach(KeyValuePair<string,DayAndTimeButton> entry in AvailabilityInfoButtons){
			entry.Value.SetDaySelected(false);
		}
		dtb.SetDaySelected(true);
		gameManager.SetAnswerTime (0);
		bool showSubmit = gameManager.SetAnswerDay(dtb.dayID);
		if(showSubmit){
			submitAnswersButton.gameObject.SetActive(true);
		}

//		Image Selected = Instantiate (SelectedImage, new Vector2 (dtb.dayButton.transform.position.x, dtb.dayButton.transform.position.y), Quaternion.Euler(Vector3.zero)) as Image;
//		Selected.transform.SetParent(GameObject.Find("Canvas").transform, true);
	}

	void TimeButtonClick(DayAndTimeButton dtb, int buttonIndex, int timeID) {
		foreach(KeyValuePair<string,DayAndTimeButton> entry in AvailabilityInfoButtons){
			entry.Value.SetDaySelected(false);
		}
		dtb.SetDaySelected (true);
		dtb.SetTimeSelected(buttonIndex);
		gameManager.SetAnswerTime (timeID);
		bool showSubmit = gameManager.SetAnswerDay(dtb.dayID);
		if(showSubmit){
			submitAnswersButton.gameObject.SetActive(true);
		}
//
//		Image Selected = Instantiate (SelectedImage, new Vector2 (dtb.dayButton.transform.position.x, dtb.dayButton.transform.position.y), Quaternion.Euler(Vector3.zero)) as Image;
//		Selected.transform.SetParent(GameObject.Find("Canvas").transform, true);
	}

	void SubmitAnswersButtonClick() {
		gameManager.SendAnswer();
	}

	public void WaitingOnAnswers() {
		submitAnswersButton.GetComponentInChildren<Text>().text = "waiting...";
	}
	#endregion


	public void ShowEndResult(int numPlayers) {
		//read from gamemanager.answerIDs
		//compare answerIDs[0] to the strings stored on the xml, perform a switch on that statement(eg. case "ThemePark": prefab.image = ThemePark.jpg
		//for(inti=0;i<numplayers;i++) {person[i].gameobject.enabled  }
		//resultsscreen.setactive(true)
		GameScreen.SetActive (false);
		ResultsScreen.SetActive (true);
//		EndScreen.SetActive (false);

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
