using UnityEngine;
using System.Collections;

public class XMLInformationReader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string GetActivityPiece(int i) {
		//compare int i to activities in information xml
		return "Activity";
	}

	public string GetDayPiece(int i) {
		return "Day";
	}

	public string GetTimePiece(int i) {
		return "15";
	}
}
