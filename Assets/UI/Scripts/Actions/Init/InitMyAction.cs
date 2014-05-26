using UnityEngine;
using System.Collections;

public abstract class InitMyAction : MyAction {

	// Use this for initialization
	void Start () {
	     Invoke("ActionTrigger",Delay);
	}
	
	void ActionTrigger()
	{
		ActionPerformed();
	}
	
	
}
