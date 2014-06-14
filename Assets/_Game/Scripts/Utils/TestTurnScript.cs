using UnityEngine;
using System.Collections;

namespace x16
{

    public class TestTurnScript : Action
    {

        public override void ActionPerformed()
        {
            Managers.Social.TriggerNextTurn();
            Application.LoadLevel("MainScene");

        }
    }
}