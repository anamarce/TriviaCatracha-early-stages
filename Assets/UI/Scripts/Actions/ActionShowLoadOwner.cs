using UnityEngine;
using System.Collections;

public class ActionShowLoadOwner : Action {
	
     public GameObject Target;
    public UIPanel PanelTarget;
	
	public override void ActionPerformed ()
	{
		
		if(Target!=null)
			Target.SetActive(true);
		else
			Debug.LogWarning("No owner was specified");

	    if (PanelTarget != null)
	    {
	        
	        NGUITools.SetActive(PanelTarget.gameObject,true);
	    }
	}
	
}
