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
    public ButtonOptionScript [] OptionButtons;

    public float TimeToAnswer = 25F;
    public AudioClip tickSound;
    private float endTime;
    private int timeLeft;
    private PlAYSTATUS CurrentStatus = PlAYSTATUS.NOTANSWER;
    private AudioSource audiosource=null;

	void Start ()
	{

        Messenger.Cleanup();
        Messenger.AddListener<int>("CorrectOptionPressed", CorrectOptionHandler);
        Messenger.AddListener<int>("WrongOptionPressed", WrongOptionHandler);
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
        DisableOptions();
        if (LabelAnswer != null)
        {
            LabelAnswer.color = Color.blue;
            LabelAnswer.text = Localization.Localize("correctanswer");
           CurrentStatus = PlAYSTATUS.CORRECT;
        }
    }
    void WrongOptionHandler(int index)
    {
        DisableOptions();
        if (LabelAnswer != null)
        {
            LabelAnswer.color = Color.red;
            LabelAnswer.text = Localization.Localize("wronganswer");
            CurrentStatus = PlAYSTATUS.WRONG;
        }
    }

    void TimeOutHandler()
    {
        CurrentStatus= PlAYSTATUS.TIMEOUT;

        if (LabelAnswer != null)
        {
            LabelAnswer.color = Color.red;
            LabelAnswer.text = Localization.Localize("timeout");

        }
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
	           
	          
	            if (audiosource != null)
	            {
	                audiosource.Stop();
	                Destroy(audiosource.gameObject);
	                audiosource = null;
	            }
                TimeOutHandler();
	        }
	    }
	}
}
