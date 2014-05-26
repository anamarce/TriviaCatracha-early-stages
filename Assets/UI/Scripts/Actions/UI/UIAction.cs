using UnityEngine;
using System.Collections;


public abstract class UIAction : Action {

	public enum UIActionEvent{OnClick,OnHoverIn,OnHoverOut,OnPressDown,OnPressUp};
	public UIActionEvent EventTrigger;
		
	void OnClick()
	{
		if(EventTrigger==UIActionEvent.OnClick)
			EventTriggered();
	}
	
    void EventTriggered()
	{
		if(Delay>0.0f)
			Invoke("ActionPerformed",Delay);
		else
			ActionPerformed();
	}
	
	
}
