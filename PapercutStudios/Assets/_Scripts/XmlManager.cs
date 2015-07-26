using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace XMLEditorC {
	public class XmlManager : Singleton<XmlManager> {
		private XmlDocument xmlInfo = new XmlDocument();
		private XmlDocument xmlRules = new XmlDocument();
		private TextAsset textAssetInfo;
		private TextAsset textAssetRules;

		protected XmlManager() {
			textAssetInfo = (TextAsset)Resources.Load("GameDataSet",typeof(TextAsset));
			textAssetRules = (TextAsset)Resources.Load("Rules",typeof(TextAsset));
			xmlInfo.LoadXml(textAssetInfo.text);
			xmlRules.LoadXml(textAssetRules.text);
			XmlNode elem = xmlInfo.DocumentElement.FirstChild;
			Debug.Log(elem.InnerText);
		}

//		private  List<string> l_sActivities = new List<string>();
//		private  List<string> l_sDays = new List<string>();
//		private  List<int> l_iTimes = new List<int>();

		private void CreateXMLFile(string inpath){
			string sFullPath = Application.dataPath + inpath;
			
			if (File.Exists(sFullPath)) {
				File.Delete(sFullPath);
			}
			
		}
//
//		 void GetInformation()
//		{
//			
//			//TextAsset textXML = (TextAsset)Resources.LoadAssetAtPath(XmlPath, typeof(TextAsset));
//			
//			XmlDocument xml = new XmlDocument();
//			xml.Load (Application.dataPath + "/_Resources/GameDataXml.xml");
//			
//			//xml.LoadXml(textXML.text);
//
//			XmlNodeList AccessedNodes = xml.GetElementsByTagName("Activity");
//
//			for (int i = 0; i < AccessedNodes.Count; i++) {
//				l_sActivities.Add(AccessedNodes[i].Attributes["name"].Value);
//			}
//			AccessedNodes = xml.GetElementsByTagName("Day");
//
//			for (int i = 0; i < AccessedNodes.Count; i++) {
//				l_sDays.Add(AccessedNodes[i].Attributes["name"].Value);
//			}
//
//			AccessedNodes = xml.GetElementsByTagName("Time");
//			
//			for (int i = 0; i < AccessedNodes.Count; i++) {
//				l_iTimes.Add(int.Parse(AccessedNodes[i].Attributes["root"].Value));
//			}
//		}

		public string GetActivityPiece(int i) {
			//compare int i to activities in information xml
			XmlNodeList nodes = xmlInfo.DocumentElement.GetElementsByTagName("Activity");
			if(nodes.Count > 0) {
				return nodes.Item(i).InnerText;
//				return nodes[i].Attributes["name"].Value;
			}
			else {
				return "Paint Ball";
			}
		}

		public string GetDayPiece(int i) {
			XmlNodeList nodes = xmlInfo.DocumentElement.GetElementsByTagName("Day");
			if(nodes.Count > 0) {
				return nodes.Item(i).InnerText;
//				return nodes[i].Attributes["name"].Value;
			}
			else {
				return "Monday";
			}
		}

		public int GetTimePiece(int i) {
			XmlNodeList nodes = xmlInfo.DocumentElement.GetElementsByTagName("Time");
			if(nodes.Count > 0) {
				return int.Parse(nodes.Item(i).InnerText);
//				return int.Parse(nodes[i].Attributes["root"].Value);
			}
			else {
				return 7;
			}
		}
	}
}
