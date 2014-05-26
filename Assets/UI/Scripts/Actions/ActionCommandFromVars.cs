using UnityEngine;
using System.Collections;

public class ActionCommandFromVars : Action {
	
	public CommandType Command;
	public Vars Variables;
	public string Key;
	
	
	public override void ActionPerformed ()
	{
	  
	//	print (Command.ToString());
		Commands.Instance.Command(Command.ToString(),Variables.Values[Key]);
		
	}
	
}
