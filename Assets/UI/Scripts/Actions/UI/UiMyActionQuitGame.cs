using UnityEngine;
using System.Collections;

public class UiMyActionQuitGame : UiMyAction {
	
	public override void ActionPerformed ()
	{
		Application.Quit();
	}
		
}
