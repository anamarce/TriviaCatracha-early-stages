using UnityEngine;
using System.Collections;

public class LanguageSet : MonoBehaviour {

	// Use this for initialization
	public UIPopupList popuplist;
    public SoundSet soundSetList;
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
        Debug.Log(val);
	    if (val != "English")
	        val = "Espanol";
		Managers.Game.preferences.CurrentLanguage = val;
		Managers.Game.preferences.SavePreferences();
		Localization.instance.currentLanguage = Managers.Game.preferences.CurrentLanguage;
	    if (soundSetList!=null)
            soundSetList.RefreshOptions();
    }

    void Update()
    {
        print("UN update dentro");
    }
}
