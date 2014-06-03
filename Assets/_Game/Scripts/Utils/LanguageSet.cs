using UnityEngine;
using System.Collections;

public class LanguageSet : MonoBehaviour {

	// Use this for initialization
	public UIPopupList popuplist;
	void Start () {
		if (popuplist!=null)
		{
			Debug.Log( Managers.Game.preferences.CurrentLanguage);
			popuplist.selection = Managers.Game.preferences.CurrentLanguage;
			//popuplist.textLabel.text = Managers.Game.preferences.CurrentLanguage;
		}
	
	}
	
	void OnSelectionChange (string val)
	{
		Managers.Game.preferences.CurrentLanguage = val;
		Managers.Game.preferences.SavePreferences();
		Localization.instance.currentLanguage = Managers.Game.preferences.CurrentLanguage;
	}
}
