using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class XmlManager : Singleton<XmlManager> {
//	private XmlNodeList dayNodes;
//	private XmlNodeList actNodes;
//	private XmlNodeList timeNodes;
	private XmlNodeList ruleNodes;
	private PlayerManager gameManager;

	private string[] actsFromXML;
	private string[] daysFromXML;
	private string[] timesFromXML;

	void Start() {
		if (gameManager == null) {
			gameManager = FindObjectOfType<PlayerManager>();
		}
	}

	protected XmlManager() {

		XmlDocument xmlInfo = new XmlDocument();
		XmlDocument xmlRules = new XmlDocument();
		TextAsset textAssetInfo = (TextAsset)Resources.Load("GameDataSet",typeof(TextAsset));
		TextAsset textAssetRules = (TextAsset)Resources.Load("Rules",typeof(TextAsset));
		xmlInfo.LoadXml(textAssetInfo.text);
		xmlRules.LoadXml(textAssetRules.text);	

		XmlNodeList dayNodes = xmlInfo.DocumentElement.GetElementsByTagName("Day");
		XmlNodeList actNodes = xmlInfo.DocumentElement.GetElementsByTagName("Activity");
		XmlNodeList timeNodes = xmlInfo.DocumentElement.GetElementsByTagName("Time");
		ruleNodes = xmlRules.DocumentElement.GetElementsByTagName("Rule");


		actsFromXML = new string[actNodes.Count];
		daysFromXML = new string[dayNodes.Count];
		timesFromXML = new string[timeNodes.Count];

		for(int i = 0; i <dayNodes.Count; i++) {
			daysFromXML[i] = dayNodes.Item(i).InnerText;
		}
		for(int i = 0; i <actNodes.Count; i++) {
			actsFromXML[i] = actNodes.Item(i).InnerText;

		}
		for(int i = 0; i <timeNodes.Count; i++) {
			timesFromXML[i] = timeNodes.Item(i).InnerText;
		}

		actsFromXML = shuffleStrings (actsFromXML);
		daysFromXML = shuffleStrings (daysFromXML);
		timesFromXML = shuffleStrings (timesFromXML);
//		shuffleNodeList(dayNodes);
//		shuffleNodeList(actNodes);
//		shuffleNodeList(timeNodes);
//		shuffleNodeList(ruleNodes);
	}

	string[] shuffleStrings(string[] toBeShuffled) {
		string[] shuffled = toBeShuffled;
		for(int i = 0; i <shuffled.Length; i++) {
			string temp = shuffled[i];

			int randomIndex = Random.Range(i,shuffled.Length);
			shuffled[i] = shuffled[randomIndex];
			shuffled[randomIndex] = temp;
		}

		return shuffled;
	}
//	XmlNodeList shuffleNodeList(XmlNodeList nodelist) {
//		XmlNodeList shuffledXmlList = nodelist;
//		for(int i = 0; i < shuffledXmlList.Count; i++) {
//			XmlNode temp = shuffledXmlList.Item(i);
//			int randomIndex = Random.Range(i,shuffledXmlList.Count);
//			shuffledXmlList[i] = shuffledXmlList.Item(randomIndex);
//		}
//		return shuffledXmlList;
//	}

	#region Read Data From XML

	public string GetActivityPiece(int i) {
		//compare int i to activities in information xml
		if(actsFromXML.Length > 0) {
			return actsFromXML[i];
		}
		else {
			//This is mostly just for debugging purposes; if for whatever reason the xmlInfo doesn't actually 
			return "ACT_ERROR";
		}
	}

	public string GetDayPiece(int i) {
		if(daysFromXML.Length > 0) {
			return daysFromXML[i];
//				return nodes[i].Attributes["name"].Value;
		}
		else {
			return "DAY_ERROR";
		}
	}

	public int GetTimePiece(int i) {
		if(timesFromXML.Length > 0) {
			return int.Parse(timesFromXML[i]);
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
//		if(SystemInfo.deviceType == DeviceType.Handheld) {
//			Random.seed = Mathf.FloorToInt(Input.acceleration.x);
//		}
//		if(SystemInfo.deviceType == DeviceType.Desktop) {
//			Random.seed = (int)System.DateTime.Now.Ticks;
//		}
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
	#endregion
}

