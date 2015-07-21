using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class XmlEditor : MonoBehaviour {

	//public TextAsset taGameAsset;
	public Text XmlText;
	
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


		XmlNode root = xml.ChildNodes;
		foreach(XmlNode node in root.ChildNodes)
		{
			if (node.FirstChild.Name == "Activity")
			{
				XmlText.text = node.InnerText;
				//XmlText.text = (node.InnerText);

				Debug.Log(node.InnerText);
			}

		}
//		
	}
}
