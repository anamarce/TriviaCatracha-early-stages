using UnityEngine;
using System.Collections;
using x16;

public enum PlAYSTATUS
{
    NOTANSWER,
    CORRECT,
    WRONG,
    TIMEOUT
};

    
public class MatchPlayScript : MonoBehaviour {

	// Use this for initialization
    public UILabel LabelTime;
    public UILabel LabelTopic;
    public UILabel LabelQuestion;
    public UILabel LabelAnswer;
    public UIButton ButtonContinue;
    public UIButton ButtonFailed;
    public UIButton ButtonWon;
    public ButtonOptionScript [] OptionButtons;

    public float TimeToAnswer = 25F;
    public AudioClip tickSound;
    private float endTime;
    private int timeLeft;
    private PlAYSTATUS CurrentStatus = PlAYSTATUS.NOTANSWER;
    private AudioSource audiosource=null;
	private int CurrentIndexSelected= -1;

	void Start ()
	{

        Messenger.Cleanup();
        Messenger.AddListener<int>("CorrectOptionPressed", CorrectOptionHandler);
        Messenger.AddListener<int>("WrongOptionPressed", WrongOptionHandler);
        if (ButtonContinue!=null)
           NGUITools.SetActive(ButtonContinue.gameObject,false);
        if (ButtonFailed != null)
            NGUITools.SetActive(ButtonFailed.gameObject, false);
        if (ButtonWon != null)
            NGUITools.SetActive(ButtonWon.gameObject, false);
	    endTime = Time.time + TimeToAnswer;
        timeLeft = (int)TimeToAnswer;
     
        
        
        ShowInitialInfo();

	    if (tickSound != null)
	        audiosource = Managers.Audio.Play(tickSound, transform.position,TimeToAnswer,true);
	}

    void DisableOptions()
    {
        for (int i = 0; i < OptionButtons.Length; i++)
        {
            OptionButtons[i].EnableOption = false;
        }
    }
    void CorrectOptionHandler(int index)
    {
		CurrentStatus = PlAYSTATUS.CORRECT;
        DisableOptions();
		CurrentIndexSelected = index;
        if (LabelAnswer != null)
        {
            Invoke("ChangeColor", 0.21F);
            Debug.Log("Correct Answer");
            LabelAnswer.color = Color.black;
            LabelAnswer.text = Localization.Localize("correctanswer");
    		
        }

        Managers.Social.IncrementCorrectAnswers();
        Managers.Social.IncrementCurrentConsecutiveAnswers(1);


        TurnOffTimerSound();
        bool IsWinner = Managers.Social.CheckWinner();
        if (IsWinner)
        {
            Managers.Social.FinishMatch();

            LabelAnswer.color = Color.black;
            LabelAnswer.text = Localization.Localize("youwon");
            if (ButtonWon != null)
                NGUITools.SetActive(ButtonWon.gameObject, true);
        }
        else
        {
            if (Managers.Social.GetCurrentConsecutiveAnswers() == Globals.Constants.IntervalAnswers)
            {
                Managers.Social.TriggerNextTurn();
                if (ButtonFailed != null)
                    NGUITools.SetActive(ButtonFailed.gameObject, true);
       
                LabelAnswer.color = Color.black;
                LabelAnswer.text = Localization.Localize("givechancetootherplayer");
            }
            else
            {
                if (ButtonContinue != null)
                    NGUITools.SetActive(ButtonContinue.gameObject, true);

            }
        }

    }

    void WrongOptionHandler(int index)
    {
		CurrentStatus = PlAYSTATUS.WRONG;
        DisableOptions();
		CurrentIndexSelected = index;
        if (LabelAnswer != null)
        {
            Invoke("ChangeColor", 0.21F);
            LabelAnswer.color = Color.red;
            LabelAnswer.text = Localization.Localize("wronganswer");
	     }
        TurnOffTimerSound();
        Managers.Social.TriggerNextTurn();

        if (ButtonFailed != null)
            NGUITools.SetActive(ButtonFailed.gameObject, true);
       
      

    }
	void ChangeColor ()
	{
      
		Color C = CurrentStatus==PlAYSTATUS.CORRECT ? Color.green : Color.red ;

		OptionButtons[CurrentIndexSelected].ButtonOption.defaultColor = C;
		OptionButtons[CurrentIndexSelected].SpriteOption.color = C;
	    OptionButtons[CurrentIndexSelected].LabelOption.color = Color.black;
	}

    void TurnOffTimerSound()
    {
        if (audiosource != null)
        {
            audiosource.Stop();
            Destroy(audiosource.gameObject);
            audiosource = null;
        }
    }
    void TimeOutHandler()
    {
        CurrentStatus= PlAYSTATUS.TIMEOUT;
        DisableOptions();
        if (LabelAnswer != null)
        {
            LabelAnswer.color = Color.red;
            LabelAnswer.text = Localization.Localize("timeout");

        }
        TurnOffTimerSound();
        Managers.Social.TriggerNextTurn();

        if (ButtonFailed != null)
            NGUITools.SetActive(ButtonFailed.gameObject, true);
       

    }
    void ShowInitialInfo()
    {
        if (LabelQuestion != null)
        {
            TriviaQuestion q = Managers.Trivia.GetCachedQuestion();
            q.Shuffle();

            LabelQuestion.text = q.Question;
            for (int i=0 ; i < q.options.Length; i++)
            {
                //TODO setear los botones de options
                OptionButtons[i].LabelOption.text = q.options[i];
                OptionButtons[i].IndexAnswer = q.indexAnswer;
                OptionButtons[i].IndexOption = i;
                OptionButtons[i].EnableOption = true;

            }
        }
        if (LabelTopic != null)
        {
            string topickey = Managers.Trivia.GetTopicName(Managers.Trivia.CurrentTopicIndexSelected);
            LabelTopic.text = Localization.Localize(topickey);
        }

        if (LabelTime != null)
            LabelTime.text = timeLeft.ToString();
	    
        
    }
    void RefreshGameTime()
    {
        timeLeft = (int)(endTime - Time.time);


        if (timeLeft <= 0)
        {
         
            timeLeft = 0;

        }
        if (LabelTime != null)
            LabelTime.text = timeLeft.ToString();


    }

	// Update is called once per frame
	void Update () {

	    if (CurrentStatus == PlAYSTATUS.NOTANSWER)
	    {
	        if (timeLeft > 0)
	            RefreshGameTime();
	        else
	        {

               TimeOutHandler();
	        }
	    }
	}
}
