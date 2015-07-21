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

	public void GetInformation()
	{
		TextAsset textXML = (TextAsset)Resources.Load("GameDataXml.xml", typeof(TextAsset));
		XmlDocument xml = new XmlDocument();
		xml.LoadXml(textXML.text);


		XmlNodeList PlayerList = xml.GetElementsByTagName("Player1");

		foreach(XmlNode PlayerInfo in PlayerList)
		{
			XmlNodeList playercontent = PlayerInfo.ChildNodes;

			foreach(XmlNode PlayerActivity in playercontent)
			{

				if (PlayerActivity.Name == "Activity")
				{
					XmlText.text = PlayerActivity.InnerText;
					//XmlText.text = (node.InnerText);

					Debug.Log(PlayerInfo.InnerText);
				}
			}

		}
//		
	}
}
