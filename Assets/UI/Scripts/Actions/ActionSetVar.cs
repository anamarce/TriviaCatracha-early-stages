using UnityEngine;
using System.Collections;

public class ActionSetVar : Action {
	
	public Vars Vars;
	public string Key;
	public string Value;
	
	public override void ActionPerformed ()
	{
		Vars.Values[Key] = Value;
	}
}
