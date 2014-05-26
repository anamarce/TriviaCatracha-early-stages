
using UnityEngine;
using System.Collections;

public class MyActionBroadCastMessage : MyAction {
	
	
	
	public string Message;
	
	
	public override void ActionPerformed ()
	{
        if(Message!=string.Empty)
             Messenger.Broadcast(Message);
	    	
	}
	
}
