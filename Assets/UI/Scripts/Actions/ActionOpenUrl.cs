using UnityEngine;
using System.Collections;

public class ActionOpenUrl : Action {

	public string url;
	
	public override void ActionPerformed ()
	{
	   if(url!=string.Empty)
		  Application.OpenURL(url);
	}
}
