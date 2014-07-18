using UnityEngine;
using System.Collections;

namespace x16
{

    public class ActionEmitAdMessage : Action
    {

       

        public override void ActionPerformed()
        {
            if (Managers.Game.Adcounting == Globals.Constants.Adinterval)
            {
                Managers.Game.Adcounting = 0;
                Messenger.Broadcast("ShowAd");
            }
           
        }
    }
}
