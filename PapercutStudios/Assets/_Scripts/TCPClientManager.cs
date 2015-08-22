using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

//Collects necessary data from other classes and sends it as bytes to the node
//Also reads data back from the node and acts on it
/*
 *	 
 */
public class TCPClientManager : Singleton<TCPClientManager> {

	PlayerManager gameManager;
	TcpClient tcpClient;
	NetworkStream stm;
	ASCIIEncoding asciiEncoding = new ASCIIEncoding();



	// Use this for initialization
	void Start () {
		if(gameManager == null) {
			gameManager = FindObjectOfType<PlayerManager>();
		}
		tcpClient = new TcpClient();
	}

	//should probably put this stuff into a tcp.listen call or something, just testing atm
	void Update () {
		if(stm != null) {
			if(stm.CanRead && stm.DataAvailable) {
				byte[] IncomingBytes;
				StringBuilder completemessage = new StringBuilder();
				IncomingBytes = new byte[100];
				int numberofBytesRead = 0;
				do{
					numberofBytesRead = stm.Read(IncomingBytes, 0, IncomingBytes.Length);
					
					completemessage.AppendFormat("{0}", asciiEncoding.GetString(IncomingBytes, 0, numberofBytesRead));
					
				}
				while(stm.DataAvailable);
				ActOnDataString(completemessage.ToString());
				Debug.Log(completemessage);
				
			}
		}

//		switch(stm) {
//			//if we receive that our connection is a success, get our assigned playernum from stream and send that to the game manager, tell ui manager that we have sucessfully connected
////		case 1 :
////			gameManager.SetPlayerNum(1);
////			break;
//		default:
//			gameManager.SetPlayerNum(1);
//			break;
//		}
	}

	public void Disconnect() {
		byte[] ba;
		ba = asciiEncoding.GetBytes(gameManager.GetPlayerNum().ToString() + "0");
		stm.Write(ba,0,ba.Length);
	}

	void ActOnDataString(string dataAsString) {
		//switch on the first character of the received data
		switch(dataAsString[0]) {
		case '0':
			//If we've received a 0 it means there are already 4 players connected to the device
			UIManager.Instance.ConnectionFailed(false);
			break;
		case '1':
		{
			//if the first character is a '1' it means we have sucessfully connected and the node has sent us our player number as the second character
			//so we must save this number as our player number for the game
			UIManager.Instance.ConnectionSuccess();
			gameManager.SetPlayerNum(ParseChar(dataAsString[1]));
			gameManager.Difficulty = ParseChar(dataAsString[2]);
			char[] timeChars = {dataAsString[3], dataAsString[4],dataAsString[5]};
			string timeString = new string(timeChars);
			gameManager.GameTime = int.Parse(timeString);

			//once we've received the default game values (player number, difficulty and time) request the seed for the randomiser
			byte[] ba;
			ba = asciiEncoding.GetBytes(gameManager.GetPlayerNum().ToString() + "1");
			stm.Write(ba,0,ba.Length);
			break;
		}
		case '2':
			StringBuilder seedAsString = new StringBuilder();
			for (int i = 0; i<ParseChar(dataAsString[1]); i++) {
				seedAsString.Append(dataAsString[2+i]);;
			}
			int seed = int.Parse(seedAsString.ToString());
			gameManager.RandomSeed = seed;
			break;
		case '3':
			//Begin playing the game
			UIManager.Instance.StartGameplay();
			break;
		case '4':
			UIManager.Instance.EndGame();
			break;
		case '5':
			gameManager.Difficulty = ParseChar(dataAsString[1]);
			break;
		case '6':
		{
			char[] timeChars = {dataAsString[1], dataAsString[2],dataAsString[3]};
			string timeString = new string(timeChars);
			gameManager.GameTime = int.Parse(timeString);
			break;
		}
		case '7':
		{
			UIManager.Instance.WaitingOnAnswers();
			break;
		}
		case '8':
		{
			gameManager.ReceiveAnswer(ParseChar(dataAsString[1]));
			break;
		}
		default:
			UIManager.Instance.ConnectionFailed(true);
			Debug.LogWarning("Trying to act on empty data!",this);
			break;
		}

	}

	public void AttempToJoin() {
		try {
			tcpClient = new TcpClient ();
			tcpClient.Connect("192.168.0.1",80);
			stm = tcpClient.GetStream();
			byte[] ba;
			ba = asciiEncoding.GetBytes("0");
			stm.Write(ba,0,ba.Length);
			
		}
		catch {
			UIManager.Instance.ConnectionFailed(true);
			Debug.LogWarning("Could not connect");
		}
	}

	public void SendAnswerToNode(string answer) {
		byte[] ba;
		ba = asciiEncoding.GetBytes(gameManager.GetPlayerNum().ToString() +"2"+ answer);
		stm.Write(ba,0,ba.Length);
	}

	int ParseChar(char c) {
		int num;
		bool result = int.TryParse(c.ToString(), out num);
		if(result) {
			return num;
		}
		else {
			Debug.LogWarning("Converstion of Playernumber Failed", this);
			return 1;
		}
	}
}
