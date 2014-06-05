using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using Parse;

public class TestParse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SaveBigObject()
	{

		int number = 42;
		string str = "the number is " + number;
		DateTime date = DateTime.Now;
		byte[] data = System.Text.Encoding.UTF8.GetBytes("foo");
		IList<object> list = new List<object> { str, number };
		IDictionary<string, object> dictionary = new Dictionary<string, object>
		{
			{ "number", number },
			{ "string", str }
		};
		
		var bigObject = new ParseObject("BigObject");
		bigObject["myNumber"] = number;
		bigObject["myString"] = str;
		bigObject["myDate"] = date;
		bigObject["myData"] = data;
		bigObject["myList"] = list;
		bigObject["myDictionary"] = dictionary;
		Task saveTask = bigObject.SaveAsync();
	}

	void OnGUI()
	{
			
		if (GUI.Button(new Rect(10, 70, 200, 50), "Create a BigObject in Parse"))
		{
			SaveBigObject();
			Debug.Log("Clicked the button with text");

		}

		if (GUI.Button(new Rect(10, 270, 200, 50), "Test AdBuddiz Ads"))
		{
			Application.LoadLevel("AdBuddizExample");

		}

		if (GUI.Button(new Rect(10, 470, 200, 50), "Show Facebook Console"))
		{
			Application.LoadLevel("InteractiveConsole");
			
		}

	}
}
