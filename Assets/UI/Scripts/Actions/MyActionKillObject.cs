using UnityEngine;
using System.Collections;

public class MyActionKillObject : MyAction {

	public GameObject Target;
	
	public override void ActionPerformed ()
	{
	   if(Target!=null)
		   GameObject.Destroy(Target);
	}
}
