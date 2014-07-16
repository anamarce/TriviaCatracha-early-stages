using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
using System.Collections;
using x16;

public class PlayScript : MonoBehaviour {

	// Use this for initialization


    public TurnBasedMatch mMatch = null;
    public MatchData mMatchData = null;
    public bool CanPlayCurrentPlayer = false;
    public string mFinalMessage = null;

    private string mErrorMessage = null;
    private string matchLanguage = "EN";
    private int CurrentConsecutiveAnswers = 0;

    private const float MaxTurnTime = 8.0f;
    private float mEndTurnCountdown = MaxTurnTime;
    private bool mEndingTurn = false;

    private void Reset()
    {
        mMatch = null;
        mMatchData = null;
        mFinalMessage = null;
        mEndingTurn = false;
        mEndTurnCountdown = MaxTurnTime;
       
    }
    public void LaunchMatch(TurnBasedMatch match)
    {
        Debug.Log("Iniciando LaunchMatch");
        Reset();
        mMatch = match;
      

        if (mMatch == null)
        {
            Debug.Log("mMatch es null");
            throw new System.Exception("Can't be started without a match!");

        }
        try
        {
            // Note that mMatch.Data might be null (when we are starting a new match).
            // MatchData.MatchData() correctly deals with that and initializes a
            // brand-new match in that case.

            Debug.Log("mMatchData se crea");
            mMatchData = new MatchData(mMatch.Data);
        }
        catch (MatchData.UnsupportedMatchFormatException ex)
        {
            mFinalMessage = "Your game is out of date. Please update your game\n" +
                             "in order to play this match.";
       
            Debug.LogWarning("Failed to parse match data: " + ex.Message);
            return;
        }




        bool canPlay = (mMatch.Status == TurnBasedMatch.MatchStatus.Active &&
                mMatch.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn);

        
        // if the match is in the completed state, acknowledge it
        if (mMatch.Status == TurnBasedMatch.MatchStatus.Complete)
        {
            PlayGamesPlatform.Instance.TurnBased.AcknowledgeFinished(mMatch.MatchId,
                    (bool success) =>
                    {
                        if (!success)
                        {
                            Debug.LogError("Error acknowledging match finish.");
                        }
                    });
        }

        SetupObjects(canPlay);
    }

    public void SetupObjects(bool canPlay)
    {
        CanPlayCurrentPlayer = canPlay;
        mFinalMessage = ExplainWhyICantPlay();
        Managers.SceneManager.LoadLevel("MatchLobby");

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

                    return mMatchData.PlayerWon == mMatch.SelfParticipantId
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

}
