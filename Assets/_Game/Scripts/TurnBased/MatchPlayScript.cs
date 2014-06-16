using UnityEngine;
using System.Collections;

public class MatchPlayScript : MonoBehaviour {

	// Use this for initialization
    public UILabel LabelTime;
    public UILabel LabelTopic;
    public UILabel LabelQuestion;
    public UIButton[] OptionButtons;

    public float TimeToAnswer = 25F;
    public AudioClip tickSound;
    private float endTime;
    private int timeLeft;

    private AudioSource audiosource=null;

	void Start ()
	{
	    endTime = Time.time + TimeToAnswer;
        timeLeft = (int)TimeToAnswer;
        ShowInitialInfo();

	    if (tickSound != null)
	        audiosource = Managers.Audio.Play(tickSound, transform.position,TimeToAnswer,true);
	}

    void ShowInitialInfo()
    {
        if (LabelQuestion != null)
        {
            TriviaQuestion q = Managers.Trivia.GetCachedQuestion();
            LabelQuestion.text = q.Question;
            int i = 0;
            foreach (string option in q.options)
            {
                //TODO setear los botones de options
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
        if (timeLeft > 0)
            RefreshGameTime();
        else
        {
            if (audiosource != null)
            {
                audiosource.Stop();
                Destroy(audiosource.gameObject);
            }
        }
	}
}
