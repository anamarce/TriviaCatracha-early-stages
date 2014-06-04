using UnityEngine;
using System.Collections;

namespace x16
{

	public class ActionShowAchievements : Action
    {

       

        public override void ActionPerformed()
        {

			Managers.Social.ShowAchievementsUI();

        }

    }
}
