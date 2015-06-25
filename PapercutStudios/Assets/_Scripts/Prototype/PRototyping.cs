using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Player
{
	public string[] Activities;
	public string[] Availabilities;
	public string[] Items;
	public string[] Rules;
}

[System.Serializable]
public class Game 
{
	public Player Player1;
	public Player Player2;
	public Player Player3;
	public Player Player4;
}

public class PRototyping : MonoBehaviour {

	public GameObject MainMenu;
	public GameObject PlayerInfo;

	public Game Game1;
	public Game Game2;
	
	public Text[] Activities;
	public Text[] Availabilities;
	public Text[] Items;
	public Text[] Rules;

	private Player SelectedPlayer;
	private Game ActiveGame;

	public void ButtonGame1() {
		ActiveGame = Game1;
	}

	public void ButtonGame2() {
		ActiveGame = Game2;
	}

	public void ButtonPlayer1() {
		if(ActiveGame == null){
			ActiveGame = Game1;
		}
		SelectedPlayer = ActiveGame.Player1;

	}

	public void ButtonPlayer2() {
		if(ActiveGame == null){
			ActiveGame = Game1;
		}
		SelectedPlayer = ActiveGame.Player2;
	}

	public void ButtonPlayer3() {
		if(ActiveGame == null){
			ActiveGame = Game1;
		}
		SelectedPlayer = ActiveGame.Player3;
	}

	public void ButtonPlayer4() {
		if(ActiveGame == null){
			ActiveGame = Game1;
		}
		SelectedPlayer = ActiveGame.Player4;
	}

	public void SetPlayerScreen() {
		MainMenu.SetActive (false);
		for(int i=0; i<Activities.Length; i++) {
			Activities[i].text = SelectedPlayer.Activities[i];
		}

		for(int i=0; i<Availabilities.Length; i++) {
			Availabilities[i].text = SelectedPlayer.Availabilities[i];
		}

		for(int i=0; i<Items.Length; i++) {
			Items[i].text = SelectedPlayer.Items[i];
		}

		for(int i=0; i<Rules.Length; i++) {
			Rules[i].text = SelectedPlayer.Rules[i];
		}

		PlayerInfo.SetActive (true);
	}
}
