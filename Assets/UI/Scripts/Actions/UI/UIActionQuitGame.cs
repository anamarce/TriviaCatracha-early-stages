using UnityEngine;
using System.Collections;

public class UIActionQuitGame : UIAction {
	
	public override void ActionPerformed ()
	{
		Application.Quit();
	}
		
}
