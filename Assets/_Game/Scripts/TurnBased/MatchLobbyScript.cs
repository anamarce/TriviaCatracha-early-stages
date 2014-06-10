using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
using System.Collections;

public class MatchLobbyScript : MonoBehaviour {

	// Use this for initialization
    public List<Participant> participants=null;

	void Start ()
	{
	    if (!Managers.Social.IsAuthenticated()) return;

	    participants = Managers.Social.GetCurrentMatchParticipants();
	    foreach (Participant participant in participants)
	    {
	        Debug.Log(participant.DisplayName + "-" + participant.ParticipantId + "-" + participant.Status.ToString());
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
