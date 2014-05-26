using UnityEngine;
using System.Collections;

public  class UIActionLoadScene : UIAction {
	
	public string SceneName;
	
	
	public override void ActionPerformed ()
	{
		
		    Application.LoadLevel(SceneName);
		
	}
	
	
	
}
