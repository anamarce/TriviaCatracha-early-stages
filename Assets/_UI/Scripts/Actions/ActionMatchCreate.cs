using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

namespace x16
{

    public class ActionMatchCreate : Action
    {

        

        public override void ActionPerformed()
        {
			const int MinOpponents = 1;
			const int MaxOpponents = 1;
			const int Variant = 0;  // default

			PlayGamesPlatform.Instance.TurnBased.CreateWithInvitationScreen(MinOpponents, MaxOpponents,
			                                                                Variant, OnMatchStarted);
			

        }
		// Callback:

		void OnMatchStarted(bool success, TurnBasedMatch match) {
			if (success) {
				Debug.Log("Game creado");
			} else {
				Debug.Log ("Error al crear game");
				// show error message
			}
		}
    }
}
