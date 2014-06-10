
using System.Collections.Generic;
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
    public  TurnBasedMatch mMatch = null;
    public MatchData mMatchData = null;
    public  string mFinalMessage = null;
	
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

	protected void OnMatchStarted(bool success, TurnBasedMatch match) 
    {
	   if (!success) {

			mErrorMessage = "There was a problem setting up the match.\nPlease try again.";
			Debug.Log(mErrorMessage);
            return;
		}
		
      LaunchMatch(match);
	}

    protected void Reset()
    {
        mMatch = null;
        mMatchData = null;
      
    }

    public List<Participant> GetCurrentMatchParticipants()
    {
        if (mMatch != null)
            return mMatch.Participants;
        else
        {
            return null;
        }

    }
    protected void LaunchMatch(TurnBasedMatch match) {
        Reset();
        mMatch = match;
        //MakeActive();

        if (mMatch == null) {
            throw new System.Exception("Can't be started without a match!");
        }
        try {
            // Note that mMatch.Data might be null (when we are starting a new match).
            // MatchData.MatchData() correctly deals with that and initializes a
            // brand-new match in that case.
            mMatchData = new MatchData(mMatch.Data);
        } catch (MatchData.UnsupportedMatchFormatException ex) {
           
            Debug.LogWarning("Failed to parse match data: " + ex.Message);
            return;
        }

     
      //  mMyMark = mMatchData.GetMyMark(match.SelfParticipantId);

        bool canPlay = (mMatch.Status == TurnBasedMatch.MatchStatus.Active &&
                mMatch.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn);

      
        // if the match is in the completed state, acknowledge it
        if (mMatch.Status == TurnBasedMatch.MatchStatus.Complete) {
            PlayGamesPlatform.Instance.TurnBased.AcknowledgeFinished(mMatch.MatchId,
                    (bool success) => {
                if (!success) {
                    Debug.LogError("Error acknowledging match finish.");
                }
            });
        }

        // set up the objects to show the match to the player
        ShowMatch(canPlay);
    }
    private string ExplainWhyICantPlay()
    {
        switch (mMatch.Status)
        {
            case TurnBasedMatch.MatchStatus.Active:
                break;
            case TurnBasedMatch.MatchStatus.Complete:
                
                return mMatchData.GeekIdWon == mMatch.SelfParticipantId ? "Match finished. YOU WIN!" :
                        "Match finished. YOU LOST!";
                break;
            case TurnBasedMatch.MatchStatus.Cancelled:
            case TurnBasedMatch.MatchStatus.Expired:
                return "This match was cancelled.";
            case TurnBasedMatch.MatchStatus.AutoMatching:
                return "This match is awaiting players.";
            default:
                return "This match can't continue due to an error.";
        }

        if (mMatch.TurnStatus != TurnBasedMatch.MatchTurnStatus.MyTurn)
        {
            return "It's not your turn yet!";
        }

        return "Error";
    }
    protected void ShowMatch(bool canPlay)
    {
        if (canPlay)
        {
            Debug.Log("Su turno : id: " + mMatch.SelfParticipantId + " Status:" +mMatch.Status.ToString());
        }
        else
        {
            mFinalMessage = ExplainWhyICantPlay();
            Debug.Log(mFinalMessage);
            Debug.Log("Not Yet id: " + mMatch.SelfParticipantId + " Status:" + mMatch.Status.ToString());
     
        }
        Application.LoadLevel("MatchLobbyScene");

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
