using UnityEngine;
using System.Collections;

public class Availability{

	public int[] baseValues = new int[2];
	public int[] iAvailableHours;
	public string sDay;
	
	public Availability(int dayId, int rootTimeId, int bracket) 
	{
		baseValues[0] = dayId;
		baseValues[1] = rootTimeId;

		iAvailableHours = CalculateAvailabilityHours(XmlManager.Instance.GetTimePiece(baseValues[1]), bracket);
		sDay = XmlManager.Instance.GetDayPiece(baseValues[0]);

	}

	int[] CalculateAvailabilityHours(int root, int bracket) {
		int[] Availabilities = new int[4];
		
		Availabilities[0] = Random.Range(root - bracket, root+1);
		Availabilities[3] = Availabilities[0] + bracket;
		Availabilities[1] = Availabilities[0] + 1;
		Availabilities[2] = Availabilities[1] + 1;
		
		return Availabilities;
	}

	public string GetAsString() 
	{
		string returnString;
		returnString = sDay +"\t" +HoursToString(iAvailableHours); 
		return returnString;
	}

	string HoursToString(int[] hours)
	{
		string returnString;
		if(hours[0] > 12) {
			returnString = (hours[0] - 12).ToString()+ "pm til " + (hours[3] -12).ToString() + "pm.";
		}
		else if (hours[0] == 12) {
			returnString = hours[0].ToString() + "pm til " + (hours[3] -12).ToString() + "pm.";
		}
		else if (hours[3] > 12) {
			returnString = hours[0].ToString() + "am til " + (hours[3] - 12).ToString() + "pm.";
		}
		else if (hours[3] == 12) {
			returnString = hours[0].ToString() + "am til " + hours[3].ToString() + "pm.";
		}
		else {
			returnString = hours[0].ToString() + "am til " + hours[3].ToString() + "am.";
		}
		return returnString;
	}
}
