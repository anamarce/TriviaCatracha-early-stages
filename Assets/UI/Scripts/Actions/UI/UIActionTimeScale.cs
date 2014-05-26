using UnityEngine;
using System.Collections;

public class UIActionTimeScale : UIAction {

    public float TimeScale;
	
	public override void ActionPerformed ()
	{
	  Time.timeScale = TimeScale;	
	}
	
	
}
