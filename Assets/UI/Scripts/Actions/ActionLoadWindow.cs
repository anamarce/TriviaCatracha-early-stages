using UnityEngine;
using System.Collections;

public class ActionLoadWindow : Action {
	
	public string WindowPrefabName;
	
	public Transform WindowParent;
	
	
	public override void ActionPerformed ()
	{
		WindowLoader.Show(WindowPrefabName,WindowParent);
	}
	
}
