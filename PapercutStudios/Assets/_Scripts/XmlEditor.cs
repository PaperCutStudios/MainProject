 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class XmlEditor : MonoBehaviour {

	//public TextAsset taGameAsset;
	//public Text XmlText;
	//string XmlPath = "Assets/Resources/_Resources/GameDataXml.xml";

	void Start()
	{
		GetInformation ();
	}

	public void GetInformation()
	{

		//TextAsset textXML = (TextAsset)Resources.LoadAssetAtPath(XmlPath, typeof(TextAsset));

		XmlDocument xml = new XmlDocument();
		xml.Load (Application.dataPath + "/Resources/GameDataXml.xml");

		//xml.LoadXml(textXML.text);


		XmlNodeList PlayerList = xml.GetElementsByTagName("Player1");

		foreach(XmlNode PlayerInfo in PlayerList)
		{
			XmlNodeList player1info = PlayerInfo.ChildNodes;

			foreach(XmlNode PlayerActivity in player1info)
			{

				if (PlayerActivity.Name == "Activity")
				{
					//XmlText.text = PlayerActivity.InnerText;
					//XmlText.text = (node.InnerText);

					Debug.Log(PlayerInfo.InnerText);
				}
			}

		}
//		
	}
}
