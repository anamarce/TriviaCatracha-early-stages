
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
            Debug.Log("True AutoLaunch");
			OnMatchStarted(true, match);
		} 
		else 
		{
            Debug.Log("False autolaunch");
           // OnMatchStarted(true, match);
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

   
    protected void LaunchMatch(TurnBasedMatch match) {
        Reset();
        mMatch = match;
      

        if (mMatch == null) {
            throw new System.Exception("Can't be started without a match!");
        }
        try {
            // Note that mMatch.Data might be null (when we are starting a new match).
            // MatchData.MatchData() correctly deals with that and initializes a
            // brand-new match in that case.
              
            mMatchData = new MatchData(mMatch.Data);
            if (mMatch.Data == null)
            {
                Debug.Log("MAtch Data es null, Set initial Data");
                mMatchData.SetInitialMatchData(mMatch, matchLanguage, 20);
            }
            Debug.Log("x2" + mMatchData.ToString());

           
            
        } catch (MatchData.UnsupportedMatchFormatException ex) {
           
            Debug.LogWarning("Failed to parse match data: " + ex.Message);
            return;
        }

     
    

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

        // Go to the Match lobby
        ShowMatch(canPlay);
    }

    public bool CanIPlayCurrentMatch()
    {
        bool canPlay = false;
        if (mMatch != null)
        {
            canPlay = (mMatch.Status == TurnBasedMatch.MatchStatus.Active &&
                            mMatch.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn);
        }
        return canPlay;
    }
    public string ExplainWhyICantPlay()
    {
        if (mMatch != null)
        {
            switch (mMatch.Status)
            {
                case TurnBasedMatch.MatchStatus.Active:
                    break;
                case TurnBasedMatch.MatchStatus.Complete:

                    return mMatchData.GeekIdWon == mMatch.SelfParticipantId
                        ? "matchfinishedwon"
                        : "matchfinishedlost";
                    break;
                case TurnBasedMatch.MatchStatus.Cancelled:
                case TurnBasedMatch.MatchStatus.Expired:
                    return "matchcancelled";
                case TurnBasedMatch.MatchStatus.AutoMatching:
                    return "matchawaiting";
                default:
                    return "matcherror";
            }

            if (mMatch.TurnStatus != TurnBasedMatch.MatchTurnStatus.MyTurn)
            {
                return "matchnotyourturn";
            }
        }
        return "";
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
       
        }
        Application.LoadLevel("MatchLobbyScene");

    }

	protected void OnGotInvitation(Invitation invitation, bool shouldAutoAccept) {
        Debug.Log("Got invitation " + shouldAutoAccept.ToString());
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
			//Social.ShowLeaderboardUI();
			((PlayGamesPlatform) Social.Active).ShowLeaderboardUI("CgkIwdz54IMaEAIQAg");
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

    public List<Participant> GetCurrentMatchParticipants()
    {
        if (mMatch != null)
            return mMatch.Participants;
        else
        {
            return null;
        }

    }
    public string GetCurrentMatchStatus()
    {
        if (mMatch != null)
            return mMatch.Status.ToString();
        else
        {
            return "";
        }
    }

    public TurnBasedMatch.MatchTurnStatus GetCurrentMatchTurnStatus()
    {
        if (mMatch != null)
        {
            return mMatch.TurnStatus;
        }
        else
        {
            return TurnBasedMatch.MatchTurnStatus.Unknown;
        }
    }
    public string GetCurrentStringMatchTurnStatus()
    {
        string stringStatus = "";
        if (mMatch != null)
        {

            switch (mMatch.TurnStatus)
            {
                    case TurnBasedMatch.MatchTurnStatus.MyTurn:
                           stringStatus = Localization.Localize("myturn");

                        break;
                    case TurnBasedMatch.MatchTurnStatus.TheirTurn:
                        stringStatus = Localization.Localize("theirturn");
                        break;
                    case TurnBasedMatch.MatchTurnStatus.Invited:
                        stringStatus = Localization.Localize("invitedtomatch");
                        break;
                    case TurnBasedMatch.MatchTurnStatus.Complete:
                           string completestatus = GetCurrentCompleteStatusMessage();
                           stringStatus = Localization.Localize(completestatus);
                        break;
                    case TurnBasedMatch.MatchTurnStatus.Unknown:
                    
                        break;
            }
        }
        return stringStatus;
    }

    private string GetCurrentCompleteStatusMessage()
    {
        if (mMatch != null)
        {
            
        }
        return "";
    }

    public string GetCurrentMatchParticipantID()
    {
        if (mMatch != null)
            return mMatch.SelfParticipantId;
        else
        {
            return "";
        }
    }

    public int  GetCurrentMatchScoreParticipantID(string participantId)
    {
        if (mMatchData != null)
            return mMatchData.GetScoreParticipantID(participantId);
        else
        {
            return 0;
        }
    }

    public void TriggerNextTurn()
    {
        if (mMatch != null)
        {
            Debug.Log("TriggerNextTurn:" + mMatchData.IndexCurrentPlayer);
            int nextOne = (mMatchData.IndexCurrentPlayer + 1)%mMatchData.numberplayers;
            mMatchData.IndexCurrentPlayer = nextOne;
            mMatchData.CurrentPlayer = mMatchData.geeks[nextOne].id;
           
            Debug.Log("PlayerNext Turn:" + mMatchData.CurrentPlayer);
            PlayGamesPlatform.Instance.TurnBased.TakeTurn
                (mMatch.MatchId, mMatchData.ToBytes(),
                     mMatchData.CurrentPlayer,
                    (bool success) =>
                    {
                        mFinalMessage = success ? "Done for now!" : "ERROR sending turn.";
                    }
                );
            Debug.Log(mFinalMessage);
            
        }
    }

    public string GetCurrentMatchID()
    {

        if (mMatch != null)
            return mMatch.MatchId;

        return "";
    }

    public void IncrementCorrectAnswers()
    {

        if (mMatch != null)
        {
            if (mMatchData != null)
            {
                mMatchData.AddScoreParticipantID(mMatch.SelfParticipantId,1);

            }

        }
    }
}
