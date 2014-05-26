using UnityEngine;
using System.Collections;

public class MyActionQuitGame : MyAction {

	
	
	public override void ActionPerformed ()
	{
		print ("QuitGame");
		Application.Quit();
	}
}
