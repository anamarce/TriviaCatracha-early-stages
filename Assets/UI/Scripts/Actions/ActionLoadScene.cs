using UnityEngine;
using System.Collections;

public class ActionLoadScene : Action {

	public string SceneName;
	
	public override void ActionPerformed ()
	{
       
		    Application.LoadLevel(SceneName);
		 
	}
	
}
