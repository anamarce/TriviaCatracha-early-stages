using UnityEngine;
using System.Collections;

public class UIActionActive : UIAction {
	public bool Active=true;
	
	public GameObject Target;
	
	public override void ActionPerformed ()
	{
		
		if(Target!=null)
		  Target.gameObject.SetActive(Active);
		else
		  this.gameObject.SetActive(Active);
	}
	
	
}
