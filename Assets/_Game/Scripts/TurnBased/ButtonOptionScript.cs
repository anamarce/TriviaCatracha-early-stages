using UnityEngine;
using System.Collections;
using x16;

namespace x16
{



public class ButtonOptionScript : Action
{

    public UIButton ButtonOption;
    public int IndexAnswer =-1;
    public int IndexOption = -1;
    public UILabel LabelOption;
    public UISprite SpriteOption;
    public bool EnableOption = true;
	// Use this for initialization
	void Start () {
	   
	}

    public override void ActionPerformed()
    {
        if (!EnableOption) return;

        if (IndexAnswer != -1 && IndexOption != -1)
        {
          

            if (IndexAnswer == IndexOption)
            {
                SpriteOption.color = Color.green;
                Messenger.Broadcast<int>("CorrectOptionPressed",IndexOption);
            }
            else
            {
                SpriteOption.color = Color.red;
                Messenger.Broadcast<int>("WrongOptionPressed", IndexOption);
            }
           
        }
    }

    // Update is called once per frame
	void Update () {
	
	}
 }

}