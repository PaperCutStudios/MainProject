using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using XMLEditorC;


public class PlayerManager : MonoBehaviour {

	PlayerTable ptPlayer1;
	PlayerTable ptPlayer2;
	PlayerTable ptPlayer3;
	PlayerTable ptPlayer4;


	public PlayerTable ptActiveTable;

	List<Rule> l_rRules = new List<Rule>();


	int iPlayerNum;
	public int iTotalRules;

	public int iBracketTime = 3;

	private int[] AnswerIDs = new int[3];

	// Use this for initialization
	void Start () {
//		Debug.Log( XmlManager.Instance.name);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetUpPlayerInformation () {
		
		//yes, its very hard-coded at this point, but it would be fairly simple to write a tool to edit these values or even read them from XML/CSV, they're currently based on what was written in TestAnswer.sheet on the PapercutStudios Google Drive
		ptPlayer1 = new PlayerTable(WriteActivityInfo (2,0,3,1,0,1,5,0,6,3,6,14,8,2,3,10,1,1,4,0),
		                            WriteAvailabilityInfo (5,11,2,4,0,0,4,17,1,1),
		                            iBracketTime);
		
		ptPlayer2 = new PlayerTable(WriteActivityInfo (1,1,4,0,2,0,3,1,0,1,5,0,4,5,6,16,8,0,4,9),
		                            WriteAvailabilityInfo (0,0,5,12,2,5,1,1,6,3),
		                            iBracketTime);
		
		ptPlayer3 = new PlayerTable(WriteActivityInfo (0,1,5,0,5,0,6,8,1,1,4,0,2,0,3,1,3,2,4,7),
		                            WriteAvailabilityInfo (1,4,3,9,2,2,6,11,0,0),
		                            iBracketTime);
		
		ptPlayer4 = new PlayerTable(WriteActivityInfo (3,4,5,11,6,6,2,5,1,1,4,0,2,0,3,1,0,1,5,0),
		                            WriteAvailabilityInfo (4,7,2,3,0,0,5,12,1,1),
		                            iBracketTime);

		//depending on the playernumber we recieve from TCPManager, set our playerNumber and, therefore, the table we use, will be moved to the "Recieve connection info" function TBD
		switch(iPlayerNum) {
		case 1 :
			ptActiveTable = ptPlayer1;
			break;
		case 2 :
			ptActiveTable = ptPlayer2;
			break;
		case 3 :
			ptActiveTable = ptPlayer3;
			break;
		case 4 :
			ptActiveTable = ptPlayer4;
			break;
		default :
			ptActiveTable = ptPlayer1;
			Debug.Log("Default Case");
			break;
		}

		for (int i = 0; i< iTotalRules; i++) {
			l_rRules.Add(GetNewRule(l_rRules));
		}

	}

	public void SetPlayerNum (int playerNum ) {
		iPlayerNum = playerNum;
		Debug.Log(iPlayerNum.ToString());
	}

	public string GetRuleString(int index) {
		return l_rRules[index].RuleText;
	}

	public string GetActivityString(int index) {
		return ptActiveTable.Activities[index].GetAsString();
	}

	public string GetAvailabilityString(int index) {
		return ptActiveTable.Availabilities[index].GetAsString();
	}

	public void SetAnswerActivity(int actID) {
		AnswerIDs[0] = actID;
		Debug.Log("Set AnsActivity ID to: " + actID + "\nThis ID correlates to: " + XmlManager.Instance.GetActivityPiece(actID));
	}

	public void SetAnswerDay(int dayID) {
		AnswerIDs[1] = dayID;
		Debug.Log("Set AnsDay ID to: " + dayID + "\nThis ID correlates to: " + XmlManager.Instance.GetDayPiece(dayID));
	}

	public void SetAnswerTime(int timeNum) {
		AnswerIDs[2] = timeNum;
		Debug.Log("Set AnsTime to: " + timeNum);
	}
	Rule GetNewRule (List<Rule> currentRules) {
		Rule newRule;
		//if we have no rules yet, grab simple rule
		if(currentRules == null) {
			newRule = XmlManager.Instance.GetNextRule();
			return newRule;
		}
		//if we have rules, build a list of current clashes to send to the XmlEditor
		else {
			List<int> ruleClashes = new List<int>();
			for(int i = 0; i<currentRules.Count; i++) {
				for(int j = 0; j<currentRules[i].l_ClashIDs.Count; j++) {
					ruleClashes.Add(currentRules[i].l_ClashIDs[j]);
				}
			}
			newRule = XmlManager.Instance.GetNextRule(ruleClashes);
			return newRule;
		}
	}


	#region Raw Playertable Definitions
	int[,] WriteActivityInfo( int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k, int l, int m, int n, int o, int p, int q, int r, int s, int t)
	{
		int[,] Output = new int[5, 4];
		Output [0, 0] = a;
		Output [0, 1] = b;
		Output [0, 2] = c;
		Output [0, 3] = d;
		Output [1, 0] = e;
		Output [1, 1] = f;
		Output [1, 2] = g;
		Output [1, 3] = h;
		Output [2, 0] = i;
		Output [2, 1] = j;
		Output [2, 2] = k;
		Output [2, 3] = l;
		Output [3, 0] = m;
		Output [3, 1] = n;
		Output [3, 2] = o;
		Output [3, 3] = p;
		Output [4, 0] = q;
		Output [4, 1] = r;
		Output [4, 2] = s;
		Output [4, 3] = t;

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
