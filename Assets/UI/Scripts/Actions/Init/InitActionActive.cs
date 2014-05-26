using UnityEngine;
using System.Collections;

public class UiInitMyActive : InitMyAction {
	
	public bool Active=true;
	
	
	public override void ActionPerformed ()
	{
		gameObject.SetActive(Active);
	}
	
	
}
