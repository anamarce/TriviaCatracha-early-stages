using UnityEngine;
using System.Collections;

public class ActionTrigger : MonoBehaviour {
	
	
	
	
	public MyAction [] MyActions;
	
	
	
	void Awake () {
	    MyActions = this.transform.GetComponentsInChildren<MyAction>();
	}
	
	public void ExecuteAllActions()
	{
		foreach(var action in MyActions)
		{
			action.ActionTriggered();
		}
	}
	
	
}
