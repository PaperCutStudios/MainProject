using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rule {
	public string RuleText;
	public List<int> l_ClashIDs;

	public Rule() {
		l_ClashIDs = new List<int>();
	}
}
