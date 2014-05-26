using UnityEngine;
using System.Collections;


public class MyActionActive:MyAction
{
	public bool Active=true;
    public GameObject Target;	
	
	public override void ActionPerformed ()
	{
		if(Target!=null)
			Target.SetActive(Active);
		else
		   gameObject.SetActive(Active);
	}
}

