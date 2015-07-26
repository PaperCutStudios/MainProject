using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XMLEditorC;


public class GameManager : MonoBehaviour {

	PlayerTable ptPlayer1;
	PlayerTable ptPlayer2;
	PlayerTable ptPlayer3;
	PlayerTable ptPlayer4;

	PlayerTable activeTable;

	public int iActiveTable;

	public int iBracketTime = 3;

	public Text[] Activities;
	public Text[] Availabilities;
	public Text[] Items;
	public Text[] Rules;

	// Use this for initialization
	void Start () {
		Debug.Log( XmlManager.Instance.name);
		initialiseEverything();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void initialiseEverything () {
		
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
		
		for(int i = 0; i < Activities.Length; i++) {
			Activities[i].text = activeTable.Activities[i].GetAsString();
		}
		for(int i = 0; i < Availabilities.Length; i++) {
			Availabilities[i].text = activeTable.Availabilities[i].GetAsString();
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
