
using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class SocialManager : MonoBehaviour {

	// Use this for initialization

	private bool userAuthenticated =false;
	// the match the player is being offered to play right now
	TurnBasedMatch mIncomingMatch = null;
	Invitation mIncomingInvite = null;
    private string matchLanguage = "EN";
	private string mErrorMessage = null;

	void Start () {
		GooglePlayGames.PlayGamesPlatform.Activate();
		//TODO para deploy quitar la linea
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Instance.TurnBased.RegisterMatchDelegate(OnGotMatch);
		PlayGamesPlatform.Instance.RegisterInvitationDelegate(OnGotInvitation);
	}
	protected void OnGotMatch(TurnBasedMatch match, bool shouldAutoLaunch) {
		if (shouldAutoLaunch) {
			OnMatchStarted(true, match);
		} 
		else 
		{
			mIncomingMatch = match;
		}
	}

	protected void OnMatchStarted(bool success, TurnBasedMatch match) {
		//EndStandBy();
		if (!success) {

			mErrorMessage = "There was a problem setting up the match.\nPlease try again.";
			Debug.Log(mErrorMessage);
            return;
		}
		
        Debug.Log(match.ToString());
		//LaunchMatch(match);
	}

	protected void OnGotInvitation(Invitation invitation, bool shouldAutoAccept) {
		if (invitation.InvitationType != Invitation.InvType.TurnBased) {
		     return;
		}
		
		if (shouldAutoAccept) {
			PlayGamesPlatform.Instance.TurnBased.AcceptInvitation(invitation.InvitationId, OnMatchStarted);
		} 
		else 
		{
			mIncomingInvite = invitation;
		}
	}

    public void SetMatchLanguage(string language)
    {
        matchLanguage = language;

    }

    public string GetMatchLanguage()
    {
        return matchLanguage;
    }
	public void InitializeSocialManager()
	{

	   ((PlayGamesPlatform)Social.Active).Authenticate
        (
              (bool success) => 
              {
				userAuthenticated = true;
	    	  }
         ,false);

		
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

    public void CreateQuickMatch(int MinOpponents, int MaxOpponents)
    {
        PlayGamesPlatform.Instance.TurnBased.CreateQuickMatch(MinOpponents, MaxOpponents,
                   0, OnMatchStarted);

    }
    public void CreateMatch(int MinOpponents, int MaxOpponents)
    {
        PlayGamesPlatform.Instance.TurnBased.CreateWithInvitationScreen(MinOpponents, MaxOpponents,
                   0, OnMatchStarted);

    }

    public void ShowAndAcceptMatchInvitations()
    {

        PlayGamesPlatform.Instance.TurnBased.AcceptFromInbox(OnMatchStarted);
    }
}
