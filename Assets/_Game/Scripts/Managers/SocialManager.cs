using GooglePlayGames;
using UnityEngine;
using System.Collections;

public class SocialManager : MonoBehaviour {

	// Use this for initialization

	public bool userAuthenticated =false;
	void Start () {
		GooglePlayGames.PlayGamesPlatform.Activate();
	}

	public void InitializeSocialManager()
	{

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
	public void SignIn()
	{
		if (!Social.localUser.authenticated) 
		{
			((PlayGamesPlatform)Social.Active).Authenticate
				(
					(bool success) => 
					{
					userAuthenticated = true;
				}
				,false);
		}

	}
	public bool IsAuthenticated()
	{
		return Social.localUser.authenticated;
	}
	public void SignOut()
	{
		if (Social.localUser.authenticated) 
		{
			((PlayGamesPlatform) Social.Active).SignOut();
			userAuthenticated = false;
		}
	}

	public void ShowLeaderboardUI() {
		if (userAuthenticated) {
			Social.ShowLeaderboardUI();
		}
		else
			SignIn();
	}

	public void ShowAchievementsUI() {
		if (userAuthenticated) {
			Social.ShowAchievementsUI();
		}
		else
			SignIn();
	}

}
