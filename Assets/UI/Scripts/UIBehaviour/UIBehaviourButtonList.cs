using UnityEngine;
using System.Collections;

public class UIBehaviourButtonList : MonoBehaviour {
	
	public UILabel ButtonLabel=null;
	public CommandType command;
	public string [] ListValues;
	
	int currentIndex=-1;
	
	// Use this for initialization
	void Start () {
		 if(ButtonLabel == null)
			Debug.LogError("Button label not found!");
		 if(ListValues==null || ListValues.Length<2)
			Debug.LogError("List Values needs a least 2 items");
		 string currentItem = (string)Commands.Instance.Command(command.ToString(),"");
		 ButtonLabel.text = currentItem;
		 for(int i=0;i<ListValues.Length;i++)
			if(ListValues[i].ToUpper() == currentItem.ToUpper())
		    {
			     currentIndex=i;
		    }
		 if(currentIndex==-1)
		   Debug.Log(currentItem+" not found on list!");
		 
	}
	
	void OnClick()
	{
		currentIndex=(currentIndex+1)%ListValues.Length;
		ButtonLabel.text = (string)Commands.Instance.Command(command.ToString(),ListValues[currentIndex]);
		
	}
	
	
	
	// Update is called once per frame
	void Update () {
	     
	}
}
