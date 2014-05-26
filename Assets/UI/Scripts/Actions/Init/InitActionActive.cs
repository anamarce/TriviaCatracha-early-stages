using UnityEngine;
using System.Collections;

public class UIInitActive : InitAction {
	
	public bool Active=true;
	
	
	public override void ActionPerformed ()
	{
		gameObject.SetActive(Active);
	}
	
	
}
