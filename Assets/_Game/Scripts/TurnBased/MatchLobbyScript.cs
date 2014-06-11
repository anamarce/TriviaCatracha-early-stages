using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
using System.Collections;

[Serializable]
public class InfoPlayers
{
    public UILabel PlayerNameLabel;
    public UILabel PlayerStatusLabel;
    public UILabel PlayerScoreLabel;
    public UISprite PlayerSprite;

}

public class MatchLobbyScript : MonoBehaviour {

	// Use this for initialization
    public UILabel MatchStatusLabel;
    public InfoPlayers[] infoPlayers;

    public List<Participant> participants=null;

	void Start ()
	{
	    if (!Managers.Social.IsAuthenticated()) return;

        RenderInfo();

	   
	}

    void RenderInfo()
    {
        if (MatchStatusLabel != null)
            MatchStatusLabel.text = Localization.Localize(Managers.Social.GetCurrentMatchStatus());

        RenderParticipantsInfo();
    }

    private void RenderParticipantsInfo()
    {
        if (infoPlayers != null && infoPlayers.Length > 0)
        {
            int i = 0;
            string MyParticipantID = Managers.Social.GetCurrentMatchParticipantID();
            participants = Managers.Social.GetCurrentMatchParticipants();
            foreach (Participant participant in participants)
            {
                if (infoPlayers[i].PlayerNameLabel != null)
                {
                    if (MyParticipantID == participant.ParticipantId)
                        infoPlayers[i].PlayerNameLabel.text = "=>" + participant.DisplayName;
                    else
                    {
                        infoPlayers[i].PlayerNameLabel.text = participant.DisplayName;
                    }
                }
                if (infoPlayers[i].PlayerStatusLabel != null)
                {
                    infoPlayers[i].PlayerStatusLabel.text = participant.Status.ToString();
                }
                if (infoPlayers[i].PlayerScoreLabel != null)
                {
                    int score = Managers.Social.GetCurrentMatchScoreParticipantID(participant.ParticipantId);
                    infoPlayers[i].PlayerScoreLabel.text = System.Convert.ToString(score);
                }
                i++;
                //Debug.Log(participant.DisplayName + "-" + participant.ParticipantId + "-" + participant.Status.ToString());
            }
        }
    }

    // Update is called once per frame
	void Update () {
	
	}
}
