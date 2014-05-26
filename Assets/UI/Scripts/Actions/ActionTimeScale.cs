using UnityEngine;
using System.Collections;

public class ActionTimeScale : Action {

	public float TimeScale;
	
	public override void ActionPerformed ()
	{
		Time.timeScale = TimeScale;
	}
}
