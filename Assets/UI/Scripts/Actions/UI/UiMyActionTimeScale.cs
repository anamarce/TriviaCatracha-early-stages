using UnityEngine;
using System.Collections;

public class UiMyActionTimeScale : UiMyAction {

    public float TimeScale;
	
	public override void ActionPerformed ()
	{
	  Time.timeScale = TimeScale;	
	}
	
	
}
