using UnityEngine;
using System.Collections;

public class UIActionCommand : UIAction {
	public CommandType Command;
	public string Argument;
 
	
	
	public override void ActionPerformed ()
	{
		Commands.Instance.Command(Command.ToString(),Argument);
	    
	}
	
}
