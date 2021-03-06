using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour {

	public PlayerTable ptActiveTable;
	private List<Rule> l_rRules = new List<Rule>();
	private int iPlayerNum =0;

	private int difficulty = 1;
	private float gametime = 270;
	private int randomseed;

	public int iTotalRules;
	public int iBracketTime = 3;
	public int RandomSeed {
		get
		{
			return randomseed;
		}
		set
		{
			randomseed = value;
			Random.seed = randomseed;
		}
	}
	public int Difficulty {
		get
		{
			return difficulty;
		}
		set 
		{
			difficulty = value;
			UIManager.Instance.UpdateDifficulty(); 
		} 
	}
	public float GameTime { 
		get
		{
			return gametime;
		}
		set 
		{
			gametime = value;
			UIManager.Instance.UpdateTimeLimit();
		}
	}
	private int[] AnswerIDs = new int[3];
	public Activity AnsweredActivity;

	// Use this for initialization
	void Start () {
//		Debug.Log( XmlManager.Instance.name);
		if(GameTime == 0f) {
			GameTime = 270f;
		}
	}

	void OnApplicationQuit() {
		TCPClientManager.Instance.Disconnect();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public void SetUpPlayerInformation () {
	
		//depending on the playernumber we recieve from TCPManager, set our playerNumber and, therefore, the table we use, will be moved to the "Recieve connection info" function TBD
		//These numbers are based on the 
		switch(iPlayerNum) {
		case 1 :
			ptActiveTable = new PlayerTable(WriteActivityInfo (2,0,0,3,1,
			                                                   0,1,1,5,0,
			                                                   5,0,3,6,14,
			                                                   6,1,2,3,10,
			                                                   1,0,1,4,0),
			                                WriteAvailabilityInfo (5,11,2,4,0,0,4,17,1,1),
			                                iBracketTime);
			break;
		case 2 :
			ptActiveTable = new PlayerTable(WriteActivityInfo (1,1,1,4,0,
			                                                   2,0,0,3,1,
			                                                   0,0,1,5,0,
			                                                   4,1,5,6,16,
			                                                   6,0,0,4,9),
			                                WriteAvailabilityInfo (0,0,5,12,2,5,1,1,6,3),
			                                iBracketTime);
			break;
		case 3 :
			ptActiveTable = new PlayerTable(WriteActivityInfo (0,0,1,5,0,
			                                                   4,1,0,6,8,
			                                                   1,0,1,4,0,
			                                                   2,1,0,3,1,
			                                                   3,0,2,4,7),
			                                WriteAvailabilityInfo (1,4,3,9,2,2,6,11,0,0),
			                                iBracketTime);
			break;
		case 4 :
			ptActiveTable = new PlayerTable(WriteActivityInfo (3,1,4,5,11,
			                                                   5,1,6,2,5,
			                                                   1,0,1,4,0,
			                                                   2,0,0,3,1,
			                                                   0,0,1,5,0),
			                                 WriteAvailabilityInfo (4,7,2,3,0,0,5,12,1,1),
			                                 iBracketTime);
			break;
		default :
			ptActiveTable = new PlayerTable(WriteActivityInfo (2,0,0,3,1,
			                                                   0,1,1,5,0,
			                                                   5,0,3,6,14,
			                                                   6,1,2,3,10,
			                                                   1,0,1,4,0),
			                                WriteAvailabilityInfo (5,11,2,4,0,0,4,17,1,1),
			                                iBracketTime);
			Debug.Log("Default Case");
			break;
		}

		for (int i = 0; i< iTotalRules; i++) {
			l_rRules.Add(GetNewRule(l_rRules));
		}

	}

	public void SetPlayerNum (int playerNum ) {
		iPlayerNum = playerNum;
	}
	public int GetPlayerNum () {
		return iPlayerNum;
	}

	public string GetRuleString(int index) {
		if(SystemInfo.deviceType == DeviceType.Handheld) {
			Handheld.Vibrate();
		}
		return l_rRules[index].RuleText;
	}

	public string GetActivityString(int index) {
		return ptActiveTable.Activities[index].GetAsString();
	}

	public string GetAvailabilityString(int index) {
		return ptActiveTable.Availabilities[index].GetAsString();
	}

	public int[] GetAnswers() {
		return AnswerIDs;
	}

	public bool SetAnswerActivity(Activity act) {
		AnswerIDs[0] = act.baseValues[0]+1;
		AnsweredActivity = act;
		Debug.Log("Set AnsActivity ID to: " + (AnswerIDs[0]-1) + "\nThis ID correlates to: " + XmlManager.Instance.GetActivityPiece((AnswerIDs[0]-1)));
		return CheckForAllAnswers();
	}

	public bool SetAnswerDay(int dayID) {
		AnswerIDs[1] = dayID+1;
		Debug.Log("Set AnsDay ID to: " + (dayID) + "\nThis ID correlates to: " + XmlManager.Instance.GetDayPiece(dayID));
		return CheckForAllAnswers();
	}

	public void SetAnswerTime(int timeNum) {
		AnswerIDs[2] = timeNum+1;
		Debug.Log("Set AnsTime to: " + timeNum);
	}

	private bool CheckForAllAnswers() {
		bool allAnswered = true;
		foreach(int i in AnswerIDs){
			if(i == 0) {
				allAnswered = false;
				break;
			}
		}
		return allAnswered;
	}

	public void SendAnswer() {
		string answerstring = "";
		foreach(int i in AnswerIDs) {
			answerstring += (i-1).ToString();
		}
		TCPClientManager.Instance.SendAnswerToNode(answerstring);
	}

	public void ReceiveAnswer(int playersAtLocation) {
		string answerActivity;
		string resultsText;
		bool happy = false;

		if(AnsweredActivity.IsOpenOnDay(AnswerIDs[1]-1)) {
			if(AnsweredActivity.IsOpenAtTime(AnswerIDs[2]-1)) {
				answerActivity = AnsweredActivity.EventName;
				switch (playersAtLocation) {
				case 1:
					resultsText = ("Dear Diary,\nToday I went to meet up with my friends at the " + answerActivity + " but I think I went to the wrong place or something because I couldn't find anyone there.");
					break;
				case 2:
					resultsText = ("Dear Diary,\nToday I went to the " + answerActivity + " to meet up with some friends. Only one other person was there at the time, maybe the others went to the wrong place?");
					break;
				case 3:
					resultsText = ("Dear Diary,\nToday I went to the " + answerActivity + " to meet up with some friends. Two of them made it there at the same time as me and we had a pretty good time. I wonder what happened to the other guy?");
					happy = true;
					break;
				case 4:
					resultsText = ("Dear Diary,\nToday I went to the " + answerActivity + " with my best mates. We all made it there without a hitch and had a ball of a day!");
					happy = true;
					break;
				default:
					resultsText = ("Broken Text");
					break;
				}
			} 
			else {
				answerActivity = "Closed";
				resultsText = "Dear Diary,\nToday " + playersAtLocation.ToString() + " of us went to  " + AnsweredActivity.EventName + " but apparently its closed at " + GetTimeAsString(AnswerIDs[2]-1) + " today.";
			}
		} 
		else {
			answerActivity = "Closed";
			resultsText = "Dear Diary,\nToday " + playersAtLocation.ToString() + " of us went to  " + AnsweredActivity.EventName + " but apparently its closed today.";
		}

		UIManager.Instance.ShowEndResult(playersAtLocation,answerActivity,resultsText, happy);
	}

	Rule GetNewRule (List<Rule> currentRules) {
		Rule newRule;
		List<int> ruleClashes = new List<int>();
		for(int i = 0; i<currentRules.Count; i++) {
			for(int j = 0; j<currentRules[i].l_ClashIDs.Count; j++) {
				ruleClashes.Add(currentRules[i].l_ClashIDs[j]);
			}
		}
		newRule = XmlManager.Instance.GetNextRule(ruleClashes,Difficulty);
		return newRule;

	}

	string GetTimeAsString(int time) {
		string returnString = "";
		if(time> 12){
			returnString = (time-12).ToString() + " pm";
		}
		else if (time == 12){
			returnString = time.ToString() + " pm";
		}
		else if (time == 0) {
			returnString = "12 am";
		}
		else if (time < 12) {
			returnString = time.ToString() + " am";
		}
		return returnString;
	}

	#region Raw Playertable Definitions
	int[,] WriteActivityInfo( int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k, int l, int m, int n, int o, int p, int q, int r, int s, int t, int u, int v, int w,int x,int y)
	{
		int[,] Output = new int[5, 5];
		Output [0, 0] = a;
		Output [0, 1] = b;
		Output [0, 2] = c;
		Output [0, 3] = d;
		Output [0, 4] = e;

		Output [1, 0] = f;
		Output [1, 1] = g;
		Output [1, 2] = h;
		Output [1, 3] = i;
		Output [1, 4] = j;

		Output [2, 0] = k;
		Output [2, 1] = l;
		Output [2, 2] = m;
		Output [2, 3] = n;
		Output [2, 4] = o;

		Output [3, 0] = p;
		Output [3, 1] = q;
		Output [3, 2] = r;
		Output [3, 3] = s;
		Output [3, 4] = t;

		Output [4, 0] = u;
		Output [4, 1] = v;
		Output [4, 2] = w;
		Output [4, 3] = x;
		Output [4, 4] = y;

		return Output;

	}
	int [,] WriteAvailabilityInfo(int a, int b, int c, int d, int e, int f, int g, int h, int i, int j)
	{
		int [,] Output = new int[5,2];
		Output [0,0] = a;
		Output [0,1] = b;
		Output [1,0] = c;
		Output [1,1] = d;
		Output [2,0] = e;
		Output [2,1] = f;
		Output [3,0] = g;
		Output [3,1] = h;
		Output [4,0] = i;
		Output [4,1] = j;

		return Output;
	}
	#endregion
}
