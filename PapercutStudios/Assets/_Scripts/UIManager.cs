using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
	private GameObject MainMenu;
	private GameObject ConnectedMenu;
	private GameObject GameScreen;
	private GameObject ResultsScreen;

	private PlayerManager gameManager;

	private Text StatusDisplay;
	private Button PlayButton;
	private Button ReconnectButton;
	private Text TimeLimitDisplay;
	private Image DifficultyImage;

	public Button DebugPlayButton;

	private List<ActivityButton> ActivityButtons = new List<ActivityButton> ();
	private Dictionary<string,DayAndTimeButton> AvailabilityInfoButtons = new Dictionary<string,DayAndTimeButton>();
	private Text[] Rules;
	private Text RulesTitle;

	private Button submitAnswersButton;

	private Image ResultsImage;
	private GameObject[] ResultsPeople;
	private Text ResultsText;

	public Sprite SelectedImage;
	public Sprite DeSelecectedImage;
	public Sprite[] HappyResultsPeople;
	public Sprite[] SadResultsPeople;
	public Sprite[] DifficultySprites;

	public int iCurrentDisplayedRules = 0;

	void Start() {
		if (gameManager == null) {
			gameManager = FindObjectOfType<PlayerManager>();
		}
		
		//		--------------Main Menu UI initialisation---------------------
		MainMenu = GameObject.FindWithTag("MainMenu");
		PlayButton = MainMenu.transform.FindChild("PlayButton").gameObject.GetComponent<Button>();
		PlayButton.onClick.AddListener(() => MainMenuConnect());

//		-------------Main Menu 2 initialisation-----------------------
		ConnectedMenu = GameObject.FindWithTag("MainMenu2");
		
		ReconnectButton = ConnectedMenu.transform.FindChild("ConnectionStatus/StatusTitle/StatusText/ConnectButton").gameObject.GetComponent<Button>();
		ReconnectButton.onClick.AddListener(() => AttemptToJoin());

		TimeLimitDisplay = ConnectedMenu.transform.FindChild("Phone Time/Phone Time/TimeLimitText").gameObject.GetComponent<Text>();
		DifficultyImage = ConnectedMenu.transform.FindChild("DifficultyPostit").gameObject.GetComponent<Image>();
		
		StatusDisplay = ConnectedMenu.transform.FindChild("ConnectionStatus/StatusTitle/StatusText").gameObject.GetComponent<Text>();
		StatusDisplay.text = "";


//		-------------Player Info Screen initialisation----------------
		GameScreen = GameObject.FindWithTag("GameScreen");
		Rules = FindTextsWithTag("RulesText");
		foreach(Text element in Rules) {
			element.text = "";
		}
		RulesTitle = GameScreen.transform.FindChild("Rules").GetComponent<Text>();
		submitAnswersButton = GameScreen.transform.FindChild("SubmitAnswers").GetComponent<Button>();
		submitAnswersButton.onClick.AddListener(() => SubmitAnswersButtonClick());
		submitAnswersButton.gameObject.SetActive(false);
		GameScreen.SetActive(false);

//		--------------Results Screen UI initialisation---------------------
		ResultsScreen = GameObject.FindWithTag ("ResultsScreen");
		ResultsImage = ResultsScreen.transform.FindChild("ResultsPhoto").GetComponent<Image>();
		ResultsPeople = GameObject.FindGameObjectsWithTag("ResultsPeople");
		ResultsText =  ResultsScreen.transform.FindChild("Diary/Text").GetComponent<Text>();
		ResultsScreen.SetActive (false);

		if(DebugPlayButton != null) {
			DebugPlayButton.onClick.AddListener(()=>StartGameplay());
		}
	}

	#region Initiation related Functions
	void SetInfoScreenDisplay() {
		for(int i = 0; i < gameManager.ptActiveTable.Availabilities.Count;i++) {
			string day = gameManager.ptActiveTable.Availabilities[i].sDay;
			int cap = i;
			AvailabilityInfoButtons.Add(day, new DayAndTimeButton(GameScreen.transform.Find("Days/Button"+day).gameObject.GetComponent<Button> (), gameManager.ptActiveTable.Availabilities[cap].baseValues[0]));
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
		}
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
		if(SystemInfo.deviceType == DeviceType.Handheld) {
			Handheld.Vibrate();
			Handheld.Vibrate();
			Handheld.Vibrate();
		}
		GameObject timer = GameScreen.transform.FindChild("timer/Timer").gameObject;
		timer.GetComponent<Timer>().StopTimer();
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
		for (int i = 0; i < ActivityButtons.Count; i++) {
			int cap = i;
			ActivityButtons[i].activityButton.onClick.AddListener(() => ActivityButtonClick(ActivityButtons[cap],gameManager.ptActiveTable.Activities[cap]));
		}

		RulesTitle.text = "<b>Time is up!</b>";
		RulesTitle.alignment = TextAnchor.UpperCenter;
		foreach(Text txt in Rules) {
			txt.text = "";
		}
		Rules[0].text = "Without talking to your fellow players, select the Activity, Day and Time you will try and meet with everyone at.\nOnce all three are selected, a button to submit your answer will appear.";
	}
	#region Menues Button Functions

	void MainMenuConnect() {
		MainMenu.SetActive(false);
		AttemptToJoin();
		UpdateTimeLimit();
		UpdateDifficulty();
	}

	public void StartGameplay() {
		ConnectedMenu.SetActive(false);
		GameScreen.SetActive(true);
		gameManager.SetUpPlayerInformation();
		SetInfoScreenDisplay ();
		ShowNextRule();
	}

	void AttemptToJoin() {
		TCPClientManager.Instance.AttempToJoin();
		ReconnectButton.gameObject.SetActive(false);
		StatusDisplay.text = "Attempting to connect to Conversity game";
	}

	public void ConnectionSuccess() {
		ReconnectButton.gameObject.SetActive(false);
		StatusDisplay.text = "<b>Connection Success!</b>\nPress Play Button on Node Device to begin!";
	}

	public void ConnectionFailed(bool b) {
		ReconnectButton.gameObject.SetActive(true);
		if(b) {
			StatusDisplay.text = "<b>Connection Failed!</b>\nPlease ensure that you are connected to the Conversity WiFi.";
		}
		else {
			StatusDisplay.text = "<b>Connection Failed!</b>\nMaximum of 4 players reached!";
		}
	}

	public void UpdateTimeLimit() {
		int minutes = Mathf.FloorToInt( gameManager.GameTime / 60);
		int seconds = Mathf.FloorToInt (gameManager.GameTime % 60);
		TimeLimitDisplay.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
	}

	public void UpdateDifficulty() {
		DifficultyImage.sprite = DifficultySprites[gameManager.Difficulty];
	}

	#endregion
	#region Answer Screen Button Functions
	void ActivityButtonClick(ActivityButton ab, Activity act) {
		foreach(ActivityButton abb in ActivityButtons) {
			abb.SetSelected(false);
//			SelectedImage.gameObject.SetActive(false);
		}
		ab.SetSelected(true);
		bool showSubmit = gameManager.SetAnswerActivity(act);
		if(showSubmit){
			submitAnswersButton.gameObject.SetActive(true);
		}
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
	}

	void SubmitAnswersButtonClick() {
		gameManager.SendAnswer();
		submitAnswersButton.gameObject.SetActive(false);
	}

	public void WaitingOnAnswers() {
		Rules[0].text = "Answer has been sent!\nWaiting on all answers to be entered!";
	}
	#endregion


	public void ShowEndResult(int numPlayers, string answerActivity, string diaryText, bool happy) {
		GameScreen.SetActive (false);
		ResultsScreen.SetActive (true);
		ResultsImage.sprite = Resources.Load <Sprite> (("ActivitySprites/"+answerActivity));
		ResultsText.text = diaryText;
		if( answerActivity != "Closed") {
			if(happy) {
				for (int i = 0; i < numPlayers; i++) 
				{
					int randomSilhouetteIndex = Random.Range(0,HappyResultsPeople.Length);
					ResultsPeople[i].GetComponent<Image>().sprite = HappyResultsPeople[randomSilhouetteIndex];
				}
			}
			else {
				for (int i = 0; i < numPlayers; i++) 
				{
					int randomSilhouetteIndex = Random.Range(0,HappyResultsPeople.Length);
					ResultsPeople[i].GetComponent<Image>().sprite = SadResultsPeople[randomSilhouetteIndex];
				}
			}
		}
	}

	public void OpenMainMenu() {
		TCPClientManager.Instance.Disconnect();
		Application.LoadLevel(0);
	}

	public void Quit() {
		Application.Quit();
	}


}
