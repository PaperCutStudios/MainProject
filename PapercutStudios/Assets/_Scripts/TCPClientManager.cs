using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Text;


public class TCPClientManager : MonoBehaviour {

	TcpClient tcpClient;
	Stream stm;

	// Use this for initialization
	void Start () {
		tcpClient = new TcpClient();
		try {
			tcpClient.Connect("192.168.0.1",80);
			stm = tcpClient.GetStream();

		}
		catch {
			Debug.Log("Could not connect");
		}

	}



	// Update is called once per frame
	void Update () {
	
	}
}
