using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Player
{
	public int iPlayerNum;
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
	public GameObject EndScreen;
	public GameObject PlayButton;

	public AudioClip Ring;

	public Text Game1ButtonText;
	public Text Game2ButtonText;

	public Game Game1;
	public Game Game2;
	
	public Text[] Activities;
	public Text[] Availabilities;
	public Text[] Items;
	public Text[] Rules;


	private Player SelectedPlayer;
	private Game ActiveGame;
	private int iCurrentRules = 0;
	public Text PlayButtonText;

	void Awake () {
//		PlayButtonText = PlayButton.GetComponentInChildren<Text>();
	}

	void Start () {
		MainMenu.SetActive(true);
		PlayerInfo.SetActive(false);
		EndScreen.SetActive(false);
		PlayButton.SetActive(false);
		Game2.Player1.Rules = Game1.Player1.Rules;
		Game2.Player2.Rules = Game1.Player2.Rules;
		Game2.Player3.Rules = Game1.Player3.Rules;
		Game2.Player4.Rules = Game1.Player4.Rules;
		Game1ButtonText.text = "Game 1 (Selected)";
	}

	#region Main Menu
	public void ButtonGame1() {
		ActiveGame = Game1;
		Game1ButtonText.text = "Game 1 (Selected)";
		Game2ButtonText.text = "Game 2";

	}

	public void ButtonGame2() {
		ActiveGame = Game2;
		
		Game2ButtonText.text = "Game 2 (Selected)";
		Game1ButtonText.text = "Game 1";
	}

	public void ButtonPlayer1() {
		if(ActiveGame == null){
			ActiveGame = Game1;
		}
		SelectedPlayer = ActiveGame.Player1;
		ActiveGame.Player1.iPlayerNum = 1;
		PlayButtonText.text = "Play as Player 1!";
		PlayButton.SetActive(true);

	}

	public void ButtonPlayer2() {
		if(ActiveGame == null){
			ActiveGame = Game1;
		}
		SelectedPlayer = ActiveGame.Player2;
		ActiveGame.Player1.iPlayerNum = 2;
		PlayButtonText.text = "Play as Player 2!";
		PlayButton.SetActive(true);
	}

	public void ButtonPlayer3() {
		if(ActiveGame == null){
			ActiveGame = Game1;
		}
		SelectedPlayer = ActiveGame.Player3;
		ActiveGame.Player1.iPlayerNum = 3;

		PlayButtonText.text = "Play as Player 3!";
		PlayButton.SetActive(true);
	}

	public void ButtonPlayer4() {
		if(ActiveGame == null){
			ActiveGame = Game1;
		}
		SelectedPlayer = ActiveGame.Player4;
		ActiveGame.Player1.iPlayerNum = 4;

		PlayButtonText.text = "Play as Player 4!";
		PlayButton.SetActive(true);
	}
	#endregion
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

		Rules[0].text = SelectedPlayer.Rules[0];

		PlayerInfo.SetActive (true);
	}

	public void ShowNextRule() {
		iCurrentRules++;
		Handheld.Vibrate();
		AudioSource.PlayClipAtPoint(Ring, transform.position);
		Rules[iCurrentRules].text = SelectedPlayer.Rules[iCurrentRules];
	}

	public void EndGame () {
		PlayerInfo.SetActive(false);
		EndScreen.SetActive(true);
	}
}
