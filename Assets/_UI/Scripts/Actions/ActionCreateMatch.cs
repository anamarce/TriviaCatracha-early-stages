using UnityEngine;
using System.Collections;

namespace x16
{

    public class ActionCreateMatch : Action
    {

     
    
        public override void ActionPerformed()
        {
            if (!Managers.Social.IsAuthenticated()) return;

             int maxopponents = 1;
           
          
            if (Managers.Social.GetMatchLanguage()!="")
               Managers.Social.CreateMatch(1, maxopponents);
            else
                Debug.Log("Match language Not set");

        }

    }
}
