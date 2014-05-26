using UnityEngine;
using System.Collections;

public class InitMyActionLoadLevel : InitMyAction {
	
	public string SceneName;
	
	
	
	public override void ActionPerformed ()
	{
		
		    Application.LoadLevel(SceneName);
		
	}
	
}
