using GooglePlayGames;
using UnityEngine;
using System.Collections;

public class SocialManager : MonoBehaviour {

	// Use this for initialization

	public bool userAuthenticated =false;
	void Start () {

	}

	public void InitializeSocialManager()
	{
		GooglePlayGames.PlayGamesPlatform.Activate();
	//	if (!Social.localUser.authenticated) 
	//	{
			// Authenticate
        
		// Social.localUser.Authenticate
        ((PlayGamesPlatform)Social.Active).Authenticate
        (
              (bool success) => 
              {
				userAuthenticated = true;
	    	  }
         ,false);

		//}
	}

}
