using UnityEngine;
using System.Collections;
using System;
using x16;

[Serializable]
public class Preferences
{


  
   
    public string CurrentLanguage;
	public bool EnableSound;


    public Preferences()
    {
		CurrentLanguage = Globals.Constants.Languages[0];
		EnableSound = true;
    }

    public void LoadPreferences()
    {
        //Sound
        if (PlayerPrefs.HasKey("EnableSound"))
        {
            if (PlayerPrefs.GetString("EnableSound") == "ON")
                EnableSound = true;
            else
                EnableSound = false;
        }
        else
            EnableSound = true;
        
		// Language
        if (PlayerPrefs.HasKey("Language"))
        {
			CurrentLanguage = PlayerPrefs.GetString("Language");
		}

         

    }

    public void SavePreferences()
    {
        if (EnableSound)
            PlayerPrefs.SetString("EnableSound","ON");
        else
            PlayerPrefs.SetString("EnableSound", "OFF");

      
        PlayerPrefs.SetString("Language", CurrentLanguage);
        PlayerPrefs.Save();
    }

    public string GetLanguagePrefix()
    {
        if (CurrentLanguage == Globals.Constants.Languages[0])
            return "EN";
        else
        
            return "ES";
        
    }
}
