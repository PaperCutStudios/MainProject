using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	PlayerTable ptPlayer1 = new PlayerTable();
	PlayerTable ptPlayer2 = new PlayerTable();
	PlayerTable ptPlayer3 = new PlayerTable();
	PlayerTable ptPlayer4 = new PlayerTable();

	// Use this for initialization
	void Start () {
		ptPlayer1.ActivityInfo[0,0] = 2;
		ptPlayer1.AvailabilityInfo[0,0] = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
