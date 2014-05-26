using UnityEngine;
using System.Collections;

public class ActionTrigger : MonoBehaviour {
	
	
	
	
	public Action [] actions;
	
	
	
	void Awake () {
	    actions = this.transform.GetComponentsInChildren<Action>();
	}
	
	public void ExecuteAllActions()
	{
		foreach(var action in actions)
		{
			action.ActionTriggered();
		}
	}
	
	
}
