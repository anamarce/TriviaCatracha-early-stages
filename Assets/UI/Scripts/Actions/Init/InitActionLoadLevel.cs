using UnityEngine;
using System.Collections;

public class InitActionLoadLevel : InitAction {
	
	public string SceneName;
	
	
	
	public override void ActionPerformed ()
	{
		
		    Application.LoadLevel(SceneName);
		
	}
	
}
