using UnityEngine;
using System.Collections;

public class UiMyActionCommand : UiMyAction {
	public CommandType Command;
	public string Argument;
 
	
	
	public override void ActionPerformed ()
	{
		Commands.Instance.Command(Command.ToString(),Argument);
	    
	}
	
}
