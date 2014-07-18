using System;
using UnityEngine;



    public  class GameManager : MonoBehaviour {

	    public Preferences preferences = new Preferences();

        public int Adcounting = 0; 	
        void Start()
        {
     
           
            Adcounting = 0;
		    preferences.LoadPreferences();
		    Localization.instance.currentLanguage = preferences.CurrentLanguage;
     
        }

   
	
	
    }
