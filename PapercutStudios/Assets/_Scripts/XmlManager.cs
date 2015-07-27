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
				//This is mostly just for debugging purposes; if for whatever reason the xmlInfo doesn't actually 
				return "ACT_ERROR";
			}
		}

		public string GetDayPiece(int i) {
			XmlNodeList nodes = xmlInfo.DocumentElement.GetElementsByTagName("Day");
			if(nodes.Count > 0) {
				return nodes.Item(i).InnerText;
//				return nodes[i].Attributes["name"].Value;
			}
			else {
				return "DAY_ERROR";
			}
		}

		public int GetTimePiece(int i) {
			XmlNodeList nodes = xmlInfo.DocumentElement.GetElementsByTagName("Time");
			if(nodes.Count > 0) {
				return int.Parse(nodes.Item(i).InnerText);
//				return int.Parse(nodes[i].Attributes["root"].Value);
			}
			else {
				return 3;
			}
		}

		//Here we want to check the player's current rules against a random potential next rule fromt he XML
		//By passing
		public Rule GetNextRule(List<int> clashes) {
			bool foundRule  = false;
			int ruleID = 0;
			XmlNodeList ruleNodes = xmlRules.DocumentElement.GetElementsByTagName("Rule");
			while (!foundRule) {
				bool passRuleTest = true;
				int randRule = Random.Range(0,ruleNodes.Count);
				ruleID = int.Parse(ruleNodes.Item(randRule).Attributes["id"].Value.ToString());
				for(int i = 0; i < clashes.Count; i++) {
					if( clashes[i] == ruleID) {
						passRuleTest = false;
						break;
					}
				}
				if(passRuleTest) {
					foundRule = true;
				}
			}
			return XmlNodeToRule(ruleNodes.Item(ruleID), clashes);
		}

		//if we don't have any rules to check against yet, simply return a random rule
		public Rule GetNextRule() {
			XmlNodeList ruleNodes = xmlRules.DocumentElement.GetElementsByTagName("Rule");
			int randRule = Random.Range(0, ruleNodes.Count);
			return XmlNodeToRule(ruleNodes.Item(randRule));
		}

		private Rule XmlNodeToRule (XmlNode node) {
			Rule returnRule = new Rule();
			returnRule.RuleText = node.Attributes["text"].Value.ToString();
			for (int i = 0; i < node.ChildNodes[1].ChildNodes.Count; i++) {
				returnRule.l_ClashIDs.Add(int.Parse(node.ChildNodes.Item(1).ChildNodes.Item(i).Attributes["id"].Value.ToString()));
			}

			//			Also, add this rule's ID to its clashes so it won't get added to the player's rules a second time
			returnRule.l_ClashIDs.Add(int.Parse(node.Attributes["id"].Value.ToString()));
			return returnRule;
		}

		private Rule XmlNodeToRule (XmlNode node, List<int> clashes) {
			Rule returnRule = new Rule();
			returnRule.RuleText = node.Attributes["text"].Value.ToString();
//			this can be confusing; as the node we're passing is <rule> and the clashes are stored as individual elements und <clashes> (ie. <rule><clashes><clash id=""><clash id = ""><clas... etc.) 
//			we need to get the children of <clashes> to iterate through
			for (int i = 0; i < node.ChildNodes[1].ChildNodes.Count; i++) {
				if(clashes.Contains(int.Parse(node.ChildNodes.Item(1).ChildNodes.Item(i).Attributes["id"].Value.ToString()))) {
					//If our player's clashes already contains the clash ID of this new rule, don't add the ID to its clashes
				} else {
					returnRule.l_ClashIDs.Add(int.Parse(node.ChildNodes.Item(1).ChildNodes.Item(i).Attributes["id"].Value.ToString()));
				}
			}
//			Also, add this rule's ID to its clashes so it won't get added to the player's rules a second time
			returnRule.l_ClashIDs.Add(int.Parse(node.Attributes["id"].Value.ToString()));
			return returnRule;
		}
	}
}
