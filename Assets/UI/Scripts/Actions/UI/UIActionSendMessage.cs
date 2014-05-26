using UnityEngine;
using System.Collections;

public class UIActionSendMessage : UIAction {
	
	public GameObject Target;
	public string TargetMethod;
	public string [] Arguments; 
	
	
	public override void ActionPerformed ()
	{
	    if(Target==null)
			Debug.LogError("Target is not specified");
		if(TargetMethod==null ||TargetMethod=="")
			Debug.LogError("TargetMethod is not specified");
		
	    Target.SendMessage(TargetMethod,Arguments,SendMessageOptions.RequireReceiver);	
	}
	
}
