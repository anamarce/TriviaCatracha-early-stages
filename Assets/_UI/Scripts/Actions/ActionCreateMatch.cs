using UnityEngine;
using System.Collections;

namespace x16
{

    public class ActionCreateMatch : Action
    {

        public UIPopupList numberList;
    
        public override void ActionPerformed()
        {
            if (!Managers.Social.IsAuthenticated()) return;

            int maxopponents = 1;
            if (numberList != null)
            {
               
                maxopponents = System.Convert.ToInt32(numberList.selection);
            }
          
            if (Managers.Social.GetMatchLanguage()!="")
               Managers.Social.CreateMatch(1, maxopponents);
            else
                Debug.Log("Match language Not set");

        }

    }
}