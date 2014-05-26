using UnityEngine;
using System.Collections;

public class MyActionTimeScale : MyAction {

	public float TimeScale;
	
	public override void ActionPerformed ()
	{
		Time.timeScale = TimeScale;
	}
}
