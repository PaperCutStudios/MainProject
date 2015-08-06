using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;


public class TCPClientManager : Singleton<TCPClientManager> {

	PlayerManager gameManager;
	TcpClient tcpClient;
	NetworkStream stm;
	ASCIIEncoding asciiEncoding = new ASCIIEncoding();
	byte[] IncomingBytes;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<PlayerManager>();
		tcpClient = new TcpClient();

	}

	//should probably put this stuff into a tcp.listen call or something, just testing atm
	void Update () {
		if(stm != null) {
			if(stm.CanRead && stm.DataAvailable) {
				StringBuilder completemessage = new StringBuilder();
				IncomingBytes = new byte[100];
				int numberofBytesRead = 0;
				do{
					numberofBytesRead = stm.Read(IncomingBytes, 0, IncomingBytes.Length);
					
					completemessage.AppendFormat("{0}", Encoding.ASCII.GetString(IncomingBytes, 0, numberofBytesRead));
					
				}
				while(stm.DataAvailable);
				
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

	void ActOnDataString(string dataAsString) {
		if(dataAsString == "Connection Initiated") {
			UIManager.Instance.ConnectionSuccess();

		}
	}

	public void AttempToJoin() {
		try {
			tcpClient.Connect("192.168.0.1",80);
			stm = tcpClient.GetStream();
			byte[] ba;
			Debug.Log(tcpClient.Client.LocalEndPoint.ToString().Remove(11));
			ba = asciiEncoding.GetBytes(gameManager.GetPlayerNum());
			stm.Write(ba,0,ba.Length);
			
		}
		catch {
			Debug.Log("Could not connect");
		}
	}
}
