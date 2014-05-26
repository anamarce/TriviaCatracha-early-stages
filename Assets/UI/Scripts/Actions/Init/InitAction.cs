using UnityEngine;
using System.Collections;

public abstract class InitAction : Action {

	// Use this for initialization
	void Start () {
	     Invoke("ActionTrigger",Delay);
	}
	
	void ActionTrigger()
	{
		ActionPerformed();
	}
	
	
}
