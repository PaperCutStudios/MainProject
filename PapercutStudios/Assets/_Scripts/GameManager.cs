using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	PlayerTable ptPlayer1 = new PlayerTable();
	PlayerTable ptPlayer2 = new PlayerTable();
	PlayerTable ptPlayer3 = new PlayerTable();
	PlayerTable ptPlayer4 = new PlayerTable();

	PlayerTable activeTable = new PlayerTable();

	public int iActiveTable;

	public int iBracketTime = 3;

	public Text[] Activities;
	public Text[] Availabilities;
	public Text[] Items;
	public Text[] Rules;

	XMLInformationReader xmlInformationReader;

	// Use this for initialization
	void Start () {
		ptPlayer1.ActivityInfo = WriteActivityInfo (2, 0, 6, 8, 1, 0, 1, 3, 2, 1, 3, 5, 6, 3, 4, 1, 0, 14, 6, 0);
		ptPlayer2.ActivityInfo = WriteActivityInfo (1, 2, 0, 4, 8, 1, 0, 1, 5, 0, 4, 3, 5, 6, 4, 0, 1, 0, 16, 9);
		ptPlayer3.ActivityInfo = WriteActivityInfo (0, 5, 1, 2, 3, 1, 0, 1, 0, 2, 5, 6, 4, 3, 4, 0, 8, 0, 1, 7);
		ptPlayer4.ActivityInfo = WriteActivityInfo (3, 6, 1, 2, 0, 4, 6, 1, 0, 1, 5, 2, 4, 3, 5, 11, 5, 0, 1, 0);

		ptPlayer1.AvailabilityInfo = WriteAvailabilityInfo (5, 2, 0, 4, 1, 8, 4, 0, 17, 1);
		ptPlayer2.AvailabilityInfo = WriteAvailabilityInfo (0, 5, 2, 1, 6, 0, 12, 5, 1, 3);
		ptPlayer3.AvailabilityInfo = WriteAvailabilityInfo (1, 3, 2, 6, 0, 4, 9, 2, 11, 0);
		ptPlayer4.AvailabilityInfo = WriteAvailabilityInfo (4, 2, 0, 5, 1, 7, 3, 0, 12, 1);

		switch(iActiveTable) {
		case 1 :
			activeTable = ptPlayer1;
			break;
		case 2 :
			activeTable = ptPlayer2;
			break;
		case 3 :
			activeTable = ptPlayer3;
			break;
		case 4 :
			activeTable = ptPlayer4;
			break;
		default :
			activeTable = ptPlayer1;
			Debug.Log("Default Case");
			break;
		}



		for (int i = 0; i<activeTable.ActivityInfo.GetLength(1); i++) {
			if(activeTable.ActivityInfo[0,i] = 0) {
				Activities[i].text = xmlInformationReader.GetActivityPiece(activeTable.ActivityInfo[0,i]) + " " +
								xmlInformationReader.GetDayPiece(activeTable.ActivityInfo[1,i]) + " " +
								xmlInformationReader.GetDayPiece(activeTable.ActivityInfo[2,i]) + " " +
						        CalculateOpenHours(xmlInformationReader.GetTimePiece(activeTable.ActivityInfo[3,i]), true);
			}
			else {
				Activities[i].text = xmlInformationReader.GetActivityPiece(activeTable.ActivityInfo[0,i]) + " " +
								xmlInformationReader.GetDayPiece(activeTable.ActivityInfo[1,i]) + " " +
								xmlInformationReader.GetDayPiece(activeTable.ActivityInfo[2,i]) + " " +
						        CalculateOpenHours(xmlInformationReader.GetTimePiece(activeTable.ActivityInfo[3,i]), false);
			}
		}
		for (int i = 0; i<activeTable.AvailabilityInfo.GetLength(1); i++) {
			Availabilities[i].text = xmlInformationReader.GetDayPiece(activeTable.AvailabilityInfo[0,i]) + " " +
									CalculateAvailabilityHours(xmlInformationReader.GetTimePiece(activeTable.AvailabilityInfo[1,i]));
		}

	}
	
	// Update is called once per frame
	void Update () {
		Activities[0].text =  xmlInformationReader.GetActivityPiece(activeTable.ActivityInfo[0,0]);
	
	}

	string CalculateAvailabilityHours(string input) {
		string sOutputString;
		int iRootNum;
		int iBegin;
		int iEnd;
		Random rand = new Random();

		iEnd = rand.Next(iRootNum, iRootNum + iBracketTime);
		iBegin = iEnd-iBracketTime;
		sOutputString = iBegin.ToString() + " til " + iEnd.ToString()
	}

	string CalculateOpenHours(string input, bool IsAnswer) {
		string OutputString;
		string sOpen;
		string sClosed;
		int iRootNum;
		if(int.TryParse(input, out iRootNum)) {
			if(iRootNum <12 && IsAnswer) {
				sOpen = (iRootNum - iBracketTime).ToString();
				sClosed = ((iRootNum - iBracketTime) + 8).ToString();
				OutputString = sOpen + " - " + sClosed;
			}
			else if (iRootNum <12 && !IsAnswer) {
				sOpen = (iRootNum + iBracketTime + 1).ToString();
				sClosed = (iRootNum + iBracketTime + 1 + 8).ToString();
				OutputString = sOpen + " - " + sClosed;
			}
			else if (iRootNum >= 12 && IsAnswer) {
				sClosed = (iRootNum + iBracketTime).ToString();
				sOpen = ((iRootNum + iBracketTime) - 8).ToString();
				OutputString = sOpen + " - " + sClosed;
			}
			else if (iRootNum >= 12 && !IsAnswer) {
				sClosed = (iRootNum - iBracketTime).ToString();
				sOpen = ((iRootNum - iBracketTime) - 8).ToString();
				OutputString = sOpen + " - " + sClosed;
			}
			else {
				OutputString = "Wrong Times";
				Debug.Log("Oh Shit, something slipped through Opening Hours");
			}
		}
		else {
			OutputString = "Couldn't Parse";
			Debug.Log("Could not parse Open Hours");
		}
		return OutputString;
	}


	#region
	int[,] WriteActivityInfo( int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k, int l, int m, int n, int o, int p, int q, int r, int s, int t)
	{
		int[,] Output = new int[4, 5];
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

		return Output;

	}
	int [,] WriteAvailabilityInfo(int a, int b, int c, int d, int e, int f, int g, int h, int i, int j)
	{
		int [,] Output = new int[2,5];
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

		return Output;
	}
	#endregion
}
