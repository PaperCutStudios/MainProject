using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTable {

	public List<Activity> Activities = new List<Activity>();
	public List<Availability> Availabilities = new List<Availability>();

	public PlayerTable (int[,] ActArray, int[,] AvailArray, int bracket) {
		for (int i = 0; i <ActArray.GetLength(0); i++) {
			Activities.Add(new Activity(ActArray[i,0],ActArray[i,1],ActArray[i,2],ActArray[i,3],bracket));
		}
		for (int i = 0; i <AvailArray.GetLength(0); i++) {
			Availabilities.Add(new Availability(AvailArray[i,0],AvailArray[i,1], bracket));
		}
	}
}
