using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class XmlEditor : MonoBehaviour {

	//public TextAsset taGameAsset;

	List<Dictionary<string,string>> levels = new List<Dictionary<string,string>>();
	Dictionary<string,string> obj;

	
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
			obj = new Dictionary<string,string>(); // Create a object(Dictionary) to collect the both nodes inside the level node and then put into levels[] array.
			
			foreach (XmlNode PlayerItems in PlayerContents) // levels itens nodes.
			{
				if(PlayerItems.Name == "Player")
				{
					obj.Add("Player",PlayerItems.InnerText); // put this in the dictionary.
				}
				
				if(PlayerItems.Name == "Player1")
				{
					obj.Add("Player1",PlayerItems.InnerText); // put this in the dictionary.
				}

			}
			levels.Add(obj); // add whole obj dictionary in the levels[].
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
