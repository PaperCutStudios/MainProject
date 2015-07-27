using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Text;


public class TCPClientManager : Singleton<TCPClientManager> {

	PlayerManager gameManager;
	TcpClient tcpClient;
	Stream stm;
	ASCIIEncoding asciiEncoding = new ASCIIEncoding();
	byte[] IncomingBytes;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<PlayerManager>();
		tcpClient = new TcpClient();
		try {
			tcpClient.Connect("192.168.0.1",80);
			stm = tcpClient.GetStream();
			Debug.Log(tcpClient.Client.RemoteEndPoint.ToString());


		}
		catch {
			Debug.Log("Could not connect");
		}

	}

	//should probably put this stuff into a tcp.listen call or something, just testing atm
	void Update () {
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

	public void AttempToJoin() {

	}
}
