using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class XmlEditor : MonoBehaviour {

	//public TextAsset taGameAsset;

	
	void Start()
	{
		GetInformation ();
	}

	void GetInformation()
	{
		TextAsset textXML = (TextAsset)Resources.Load("GameDataXml.xml", typeof(TextAsset));
		XmlDocument xml = new XmlDocument();
		xml.LoadXml(textXML.text);

		XmlNodeList PlayerList = xml.GetElementsByTagName("Player");


		foreach (XmlNode PlayerInfo in PlayerList)
		{
			XmlNodeList PlayerContents = PlayerInfo.ChildNodes;

			foreach (XmlNode PlayerItems in PlayerContents) // levels itens nodes.
			{
				if(PlayerItems.Name == "Player")
				{

				}
				
				if(PlayerItems.Name == "Player1")
				{

				}

			}

		}
//		XmlNode root = xmlDoc.FirstChild;
//		foreach(XmlNode node in root.ChildNodes)
//		{
//			if (node.FirstChild.NodeType == XmlNodeType.Text)
//			{
//				Debug.Log(node.InnerText);
//				Debug.Log("Works");
//			}
//		}
	}
}
