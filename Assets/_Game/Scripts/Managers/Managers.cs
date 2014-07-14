
using UnityEngine;
using System;
using System.Collections;


    public class Managers : MonoBehaviour
    {

	   private static SocialManager socialManager;
		public static SocialManager Social
		{
			get { Validate();return socialManager; }
		}

        private static GameManager gameManager;
        public static GameManager Game
        {
            get { Validate();return gameManager; }
        }
	
        private static AudioManager audioManager;
        public static AudioManager Audio
        {
            get {Validate(); return audioManager; }
        }
	
        private static EffectsManager effectsManager;
        public static EffectsManager Effects
        {
            get { Validate();return effectsManager; }
        }
	
        private static DataManager dataManager;
        public static DataManager Data
        {
            get {Validate(); return dataManager; }
        }

        private static PlatformManager platformManager;
        public static PlatformManager Platform
        {
            get { Validate(); return platformManager; }
        }

        private static TriviaApi triviaManager;
        public static TriviaApi Trivia
        {
            get { Validate(); return triviaManager; }
        }
	   private static PanelManager panelManager;
	   public static PanelManager SceneManager
 	   {
		get { Validate(); return panelManager; }
	   }
        static void Validate()
        {
            if(gameManager==null || audioManager ==null  || effectsManager==null
		   || dataManager == null || platformManager == null || socialManager == null  
                || triviaManager ==null)
            {
                Application.LoadLevel("Xtudio16StartScene");
		    
            }
        }
	
	
        void Start ()
        {
	    
		
            //Find the references
            socialManager = GetComponent<SocialManager>();
            gameManager = GetComponent<GameManager>();
            audioManager = GetComponent<AudioManager>();
            dataManager = GetComponent<DataManager>();
            effectsManager = GetComponent<EffectsManager>();
            platformManager = GetComponent<PlatformManager>();
            triviaManager = GetComponent<TriviaApi>();
		    panelManager = GetComponent<PanelManager>();
	  
            DontDestroyOnLoad(this);
	
		
        }





    }
