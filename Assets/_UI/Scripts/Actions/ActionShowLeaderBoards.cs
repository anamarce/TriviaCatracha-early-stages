using UnityEngine;
using System.Collections;

namespace x16
{

	public class ActionShowLeaderBoards : Action
    {

       

        public override void ActionPerformed()
        {
            if (!Managers.Social.IsAuthenticated()) return;
			Managers.Social.ShowLeaderboardUI();

        }

    }
}
