using UnityEngine;
using System.Collections;

public class ActionQuitGame : Action {

	
	
	public override void ActionPerformed ()
	{
		print ("QuitGame");
		Application.Quit();
	}
}
