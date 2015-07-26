using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
	private GameObject MainMenu;
	private GameObject PlayerInfo;

	private GameManager gameManager;

	private Text StatusDisplay;
	private Button PlayButton;
	private Button JoinButton;

	void Start() {
		if (gameManager == null) {
			gameManager = FindObjectOfType<GameManager>();
		}

		MainMenu = GameObject.FindWithTag("MainMenu");
		PlayerInfo = GameObject.FindWithTag("PlayerInfo");
		PlayerInfo.SetActive(false);

		JoinButton = MainMenu.transform.FindChild("JoinButton").gameObject.GetComponent<Button>();
		JoinButton.onClick.AddListener(() => ConnectionSuccess());
//		JoinButton.onClick.AddListener(() => TCPClientManager.Instance.AttempToJoin());
		JoinButton.gameObject.SetActive(true);

		PlayButton = MainMenu.transform.FindChild("PlayButton").gameObject.GetComponent<Button>();
		PlayButton.onClick.AddListener(() => MainMenuPlay());
		PlayButton.gameObject.SetActive(false);

		StatusDisplay = MainMenu.transform.FindChild("StatusDisplay").gameObject.GetComponent<Text>();


	}

	public void MainMenuPlay() {
		MainMenu.SetActive(false);
		PlayerInfo.SetActive(true);
	}

	public void ConnectionSuccess() {
		PlayButton.gameObject.SetActive(true);
		JoinButton.gameObject.SetActive(false);
		gameManager.SetUpPlayerInformation();


	}

	public void Quit() {
		Application.Quit();
	}
}

