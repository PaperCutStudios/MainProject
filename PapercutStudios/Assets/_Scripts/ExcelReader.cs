using UnityEngine;
using System.Collections;
using System; 
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel; 


public class ExcelReader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ReadXLSX (Application.dataPath + "/Excel/TestAnswer.xlsx");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ReadXLSX(string filetoread)
	{

	}
}
