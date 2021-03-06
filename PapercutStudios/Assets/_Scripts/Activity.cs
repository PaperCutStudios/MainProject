﻿using UnityEngine;
using System.Collections;

public class Activity {

	//not even sure if BaseValues is technically needed, considering that we're getting passed these values on construction
	public int[] baseValues = new int[5];
	public string EventName;
	private string ClosedDay1;
	private string ClosedDay2;
	private int[] OpenHours = new int[2];				//where [0] = open from, [1] = open til (closing)
	private bool isAnswer;

	public Activity(int EventID,int ShownDetails ,int Day1ID, int Day2ID, int RootTimeID, int bracket) 
	{
		baseValues[0] = EventID;
		baseValues[1] = ShownDetails;
		baseValues[2] = Day1ID;
		baseValues[3] = Day2ID;
		baseValues[4] = RootTimeID;

		if(baseValues[2] == 1 && baseValues[0] == 0) {
			isAnswer = true;
		}
		else {
			isAnswer = false;
		}

		SetInterpretedValues(bracket);

		if(isAnswer) {
			Debug.Log(EventName + " is the answer!");
		}

	}

	void SetInterpretedValues (int bracket) {
		EventName =XmlManager.Instance.GetActivityPiece(baseValues[0]);
		ClosedDay1 = XmlManager.Instance.GetDayPiece(baseValues[2]);
		ClosedDay2 = XmlManager.Instance.GetDayPiece(baseValues[3]);
		OpenHours = CalcOpeningHours(XmlManager.Instance.GetTimePiece(baseValues[4]),isAnswer,bracket);
	}

	public string GetAsString() 
	{
		string returnString;

		if(baseValues[1] == 0) {
			returnString = EventName + "\n(Open ???, Closed ???)";
		}
		else if(baseValues[1] == 1) {
			returnString = EventName + "\n(Open " +HoursToString(OpenHours) + ", Closed " + ClosedDay1 +" & "+ ClosedDay2 +")";
		}
		else {
			returnString = "ERROR 100: IncorrectShownDetails";
		}


		return returnString;
	}

	public bool IsOpenOnDay (int DayID) {
		if(DayID == baseValues[2] || DayID == baseValues[3]) {
			return false;
		} else {
			return true;
		}
	}

	public bool IsOpenAtTime (int CheckTime) {
		if(Between(CheckTime, OpenHours[0], OpenHours[1])) {
			return true;
		}
		else {
			return false;
		}
	}

	string HoursToString(int[] hours)
	{
		string returnString;
		if(hours[0] > 12) {
			returnString = (hours[0] - 12).ToString()+ "pm-" + (hours[1] -12).ToString() + "pm";
		}
		else if (hours[0] == 12) {
			returnString = hours[0].ToString() + "pm-" + (hours[1] -12).ToString() + "pm";
		}
		else if (hours[1] > 12) {
			returnString = hours[0].ToString() + "am-" + (hours[1] - 12).ToString() + "pm";
		}
		else if (hours[1] == 12) {
			returnString = hours[0].ToString() + "am-" + hours[1].ToString() + "pm";
		}
		else {
			returnString = hours[0].ToString() + "am-" + hours[1].ToString() + "am";
		}
		return returnString;
	}

	private int[] CalcOpeningHours(int iRootNum, bool IsAnswer, int iBracketTime) 
	{
		int[] hours = new int[2];
		if(iRootNum <12 && IsAnswer) {
			hours[0] = (iRootNum - iBracketTime);
			hours[1] = ((iRootNum - iBracketTime) + 8);
		}
		else if (iRootNum <12 && !IsAnswer) {
			hours[0] = (iRootNum + iBracketTime + 1);
			hours[1] = (iRootNum + iBracketTime + 1 + 8);
		}
		else if (iRootNum >= 12 && IsAnswer) {
			hours[1] = (iRootNum + iBracketTime);
			hours[0] = ((iRootNum + iBracketTime) - 8);
		}
		else if (iRootNum >= 12 && !IsAnswer) {
			hours[1] = (iRootNum - iBracketTime);
			hours[0] = ((iRootNum - iBracketTime) - 8);
		}
		return hours;
	}

	private bool Between(int num, int lower, int upper) {
		if(num >= lower && num <=upper) {
			return true;
		}
		else {
			return false;
		}
	}
}
