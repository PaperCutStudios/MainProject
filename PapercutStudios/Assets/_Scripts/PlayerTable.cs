using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTable {

	public List<Activity> Activities = new List<Activity>();
	public List<Availability> Availabilities = new List<Availability>();

	public PlayerTable (int[,] ActArray, int[,] AvailArray, int bracket) {
		for (int i = 0; i <ActArray.GetLength(0); i++) {
			Activities.Add(new Activity(ActArray[i,0],ActArray[i,1],ActArray[i,2],ActArray[i,3],ActArray[i,4],bracket));
		}
		for (int i = 0; i <AvailArray.GetLength(0); i++) {
			Availabilities.Add(new Availability(AvailArray[i,0],AvailArray[i,1], bracket));
		}

		//shuffle the activities list using a Fisher-Yates shuffle
		for (int i = 0; i < Activities.Count; i++) {
			Activity temp = Activities[i];
			int randomIndex = Random.Range(i, Activities.Count);
			Activities[i] = Activities[randomIndex];
			Activities[randomIndex] = temp;
		}

		for (int i = 0; i < Activities.Count; i++) {
			Availability temp = Availabilities[i];
			int randomIndex = Random.Range(i, Availabilities.Count);
			Availabilities[i] = Availabilities[randomIndex];
			Availabilities[randomIndex] = temp;
		}
	}
}
