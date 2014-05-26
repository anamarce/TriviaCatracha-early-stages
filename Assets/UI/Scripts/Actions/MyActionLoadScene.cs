using UnityEngine;
using System.Collections;

public class MyActionLoadScene : MyAction {

	public string SceneName;
	
	public override void ActionPerformed ()
	{
       
		    Application.LoadLevel(SceneName);
		 
	}
	
}
