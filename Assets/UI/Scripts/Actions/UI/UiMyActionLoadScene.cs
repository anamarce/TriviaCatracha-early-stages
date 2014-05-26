using UnityEngine;
using System.Collections;

public  class UiMyActionLoadScene : UiMyAction {
	
	public string SceneName;
	
	
	public override void ActionPerformed ()
	{
		
		    Application.LoadLevel(SceneName);
		
	}
	
	
	
}
