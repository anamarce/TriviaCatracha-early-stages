using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
using System.Collections;

public class MainSceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}


    protected void OnGotMatch(TurnBasedMatch match, bool shouldAutoLaunch)
    {

        Debug.Log("OnGotMatch a secas");
        if (shouldAutoLaunch)
        {
            Debug.Log("OnGotMatch IF");
            Managers.Social.OnMatchStarted(true, match);
        }
        else
        {
            Debug.Log("OnGotMatch Else");
           // mIncomingMatch = match;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
