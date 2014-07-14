
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using x16;

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
    
    private int CurrentConsecutiveAnswers = 0;
	private bool FirstTimeRegisterDelegates;

    void Start () {
		FirstTimeRegisterDelegates = true;
		PlayGamesPlatform.Activate();
		Debug.Log("SocialManager:Start:After Activating");

		PlayGamesPlatform.DebugLogEnabled = false;
       
        // try login in silent mode
        
		this.SignIn(true);
		Debug.Log("SocialManager:Start:After calling silent signin");

		
		
	}

    void Update()
    {
        if (mIncomingMatch != null)
        {
            Debug.Log("Update, mIncomingMatch !=null");
            ShowIncomingMatchUi();
          
        }
      
    }
	public void SignIn(bool silent=false)
	{
		
		((PlayGamesPlatform)Social.Active).Authenticate
			(
				(bool success) => 
				{
				    if(success)
				    {
						userAuthenticated = true;
					    if (FirstTimeRegisterDelegates)
					    {
							Debug.Log("SocialManager:Sigin:Succesful , before registering delegates");
							PlayGamesPlatform.Instance.TurnBased.RegisterMatchDelegate(OnGotMatch);
							PlayGamesPlatform.Instance.RegisterInvitationDelegate(OnGotInvitation);
							Debug.Log("SocialManager:Sigin:Succesful , after registering delegates");
						    FirstTimeRegisterDelegates = false;
					    }
						Managers.Trivia.LoadTopicsStats();
						Debug.Log("SocialManager:Sigin:Succesful , After Loading Topics Call");

				        Managers.SceneManager.LoadLevel("MainScene");

						
					 }
			       else
				    {
					   userAuthenticated = false;
					   Debug.Log("SocialManager:Sigin:Failded ....");
				    }
			   }
			,silent);
		
		
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

    void ShowIncomingMatchUi()
    {
        TurnBasedMatch match = mIncomingMatch;
        mIncomingMatch = null;
        OnMatchStarted(true, match);
    
    }
    void OnApplicationFocus(bool focusStatus)
    {
       // Debug.Log("App Foucs, delegates ");
       // RegisterDelegates();
    }
    void OnApplicationPause(bool pauseStatus)
    {
       
    }

    

	protected void OnGotMatch(TurnBasedMatch match, bool shouldAutoLaunch) {

        Debug.Log("OnGotMatch a secas");
		if (shouldAutoLaunch) {
            Debug.Log("OnGotMatch IF");
			OnMatchStarted(true, match);
		} 
		else 
		{
            Debug.Log("OnGotMatch Else");
			mIncomingMatch = match;
		}
	}

	public void OnMatchStarted(bool success, TurnBasedMatch match) 
    { 
       Debug.Log("OnMatchStarted ...");
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
                
                mMatchData.SetInitialMatchData(mMatch, matchLanguage, Globals.Constants.MaxAnswers);
            }
            else
            {
                if (mMatch.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn)
                {
                    if (mMatchData.geeks[mMatchData.IndexCurrentPlayer].id == "")
                    {
                        mMatchData.geeks[mMatchData.IndexCurrentPlayer].id = mMatch.SelfParticipantId;
                        mMatchData.CurrentPlayer = mMatch.SelfParticipantId;
                    }
                }
            }
          
            
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
            mFinalMessage= "Su turno : id: " + mMatch.SelfParticipantId + " Status:" +mMatch.Status.ToString();
        }
        else
        {
            mFinalMessage = ExplainWhyICantPlay();
       
        }
        //Application.LoadLevel("MatchLobbyScene");
        Managers.SceneManager.LoadLevel("MatchLobby");

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
            Debug.Log("OnGotInvitation Else");
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

	public void ShowLeaderboardUI() {
		if (userAuthenticated) {
			//Social.ShowLeaderboardUI();
			((PlayGamesPlatform) Social.Active).ShowLeaderboardUI("CgkIwdz54IMaEAIQAg");
		}
	
	}

	public void ShowAchievementsUI() {
		if (userAuthenticated) {
			Social.ShowAchievementsUI();
		}
		
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

    public int GetCurrentMatchScore()
    {
        if (mMatch != null)
            return GetCurrentMatchScoreParticipantID(mMatch.SelfParticipantId);
        else
            return 0;
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
            if (mMatch.AvailableAutomatchSlots == 0)
            {
                int nextOne = (mMatchData.IndexCurrentPlayer + 1)%mMatchData.numberplayers;
                mMatchData.IndexCurrentPlayer = nextOne;
                mMatchData.CurrentPlayer = mMatchData.geeks[nextOne].id;


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
            else // is a quick match
            {
                    
                    mMatchData.IndexCurrentPlayer = 1;
                    mMatchData.CurrentPlayer = "";
                      PlayGamesPlatform.Instance.TurnBased.TakeTurn
                    (mMatch.MatchId, mMatchData.ToBytes(),null
                         ,
                        (bool success) =>
                        {
                            mFinalMessage = success ? "Done for now!" : "ERROR sending turn.";
                        }
                    );
                Debug.Log(mFinalMessage);
               
                    
            }

                
            
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

    public bool CheckWinner()
    {
        if (mMatch != null)
        {
            if (mMatchData != null)
            {
                int currentScore = mMatchData.GetScoreParticipantID(mMatch.SelfParticipantId);
                if (currentScore == mMatchData.topanswers)
                    return true;
                else
                    return false;

            }
        }
        return false;
    }

    public void FinishMatch()
    {
        // define the match's outcome
        MatchOutcome outcome = new MatchOutcome();
        outcome.SetParticipantResult(mMatch.SelfParticipantId, MatchOutcome.ParticipantResult.Win);
        mMatchData.GeekIdWon = mMatch.SelfParticipantId;
        foreach (Geek geek in mMatchData.geeks)
        {
            if (geek.id != mMatch.SelfParticipantId)
            {
                outcome.SetParticipantResult(geek.id,MatchOutcome.ParticipantResult.Loss);
            }
        }


        PlayGamesPlatform.Instance.TurnBased.Finish(mMatch.MatchId, mMatchData.ToBytes(),
                   outcome, (bool success) =>
                  {
                      if (success)
                          mFinalMessage = "xx YOU WON!!" ;
                      else
                      {
                          mFinalMessage = "Error winning";
                      }
                  });
        Debug.Log(mFinalMessage);
    }

    public int GetCurrentTotalAnswers()
    {
        if (mMatchData != null)
            return mMatchData.topanswers;

        return 0;
    }

    public void ResetCurrentConsecutiveAnswers()
    {
       
        CurrentConsecutiveAnswers = 0;
    }

    public int GetCurrentConsecutiveAnswers()
    {
        return CurrentConsecutiveAnswers;
    }

    public void IncrementCurrentConsecutiveAnswers(int cant)
    {
        CurrentConsecutiveAnswers += cant;
    }

    public void TriggerMyTurnAgain()
    {
        if (mMatch != null)
        {
           
            PlayGamesPlatform.Instance.TurnBased.TakeTurn
                (mMatch.MatchId, mMatchData.ToBytes(),
                    mMatch.SelfParticipantId,
                    (bool success) =>
                    {
                        mFinalMessage = success ? "Again!" : "ERROR sending turn.";
                    }
                );
            Debug.Log(mFinalMessage);

        }
    }
}
