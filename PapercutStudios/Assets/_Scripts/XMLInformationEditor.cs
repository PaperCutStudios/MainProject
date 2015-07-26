using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace XMLEditorC {
	public sealed class XMLInformationEditor {
		private static XMLInformationEditor instance = null;
		private static object padlock = new object();
		private static XmlDocument xml = new XmlDocument();

		XMLInformationEditor() {
			xml = new XmlDocument();
			xml.Load(Application.dataPath +"/_Resources/GameDataXml.xml");
		}

//
//		{
////			GetInformation();
////			XmlDocument xml = new XmlDocument();
//			xml.Load (Application.dataPath + "/_Resources/GameDataXml.xml");
//
//		}

		public static XMLInformationEditor Instance
		{
			get
			{
				lock (padlock)
				{
					if(instance == null)
					{
						instance = new XMLInformationEditor();
					}
					return instance;
				}
			}
		}

//		private static List<string> l_sActivities = new List<string>();
//		private static List<string> l_sDays = new List<string>();
//		private static List<int> l_iTimes = new List<int>();

		private static void CreateXMLFile(string inpath){
			string sFullPath = Application.dataPath + inpath;
			
			if (File.Exists(sFullPath)) {
				File.Delete(sFullPath);
			}
			
		}
//
//		static void GetInformation()
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

		public static string GetActivityPiece(int i) {
			//compare int i to activities in information xml
			XmlNodeList nodes = xml.GetElementsByTagName("Activity");
			return nodes[i].Attributes["name"].Value;
		}

		public static string GetDayPiece(int i) {
			XmlNodeList nodes = xml.GetElementsByTagName("Days");
			return nodes[i].Attributes["name"].Value;
		}

		public static int GetTimePiece(int i) {
			XmlNodeList nodes = xml.GetElementsByTagName("Times");
			return int.Parse(nodes[i].Attributes["root"].Value);
		}
	}
}
