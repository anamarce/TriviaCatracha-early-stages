using System;
using UnityEngine;



    public  class GameManager : MonoBehaviour {

	    public Preferences preferences = new Preferences();
	
	
        void Start()
        {
     
            Time.timeScale = 1.0f;
		    preferences.LoadPreferences();
		    Localization.instance.currentLanguage = preferences.CurrentLanguage;
     
        }

   
	
	
    }
