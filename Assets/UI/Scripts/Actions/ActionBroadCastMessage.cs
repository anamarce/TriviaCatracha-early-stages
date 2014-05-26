
using UnityEngine;
using System.Collections;

public class ActionBroadCastMessage : Action {
	
	
	
	public string Message;
	
	
	public override void ActionPerformed ()
	{
        if(Message!=string.Empty)
             Messenger.Broadcast(Message);
	    	
	}
	
}
