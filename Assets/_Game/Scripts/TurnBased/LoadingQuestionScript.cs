using System;
using UnityEngine;
using System.Collections;
using Parse;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public enum QUESTIONSTATUS
{
    LOADING,
    LOADEDSUCCESFULL,
    LOADEDFAIL,
    LOADEDTIMEOUT

};
public class LoadingQuestionScript : MonoBehaviour {

	// Use this for initialization
    public UILabel labelError;
    public UIButton ButtonGoBack;

    public float TimeToTimeOut = 25F;
    
    private float endTime;
    private int timeLeft;

    private TriviaQuestion CachedQuestion;
    private QUESTIONSTATUS qstatus;
	void Start () {

        endTime = Time.time + TimeToTimeOut;
        timeLeft = (int)TimeToTimeOut;

        CachedQuestion = new TriviaQuestion();
	    qstatus = QUESTIONSTATUS.LOADING;
        if (ButtonGoBack != null)
        {
            NGUITools.SetActive(ButtonGoBack.gameObject, false);
        }
         string languagecode = Managers.Game.preferences.GetLanguagePrefix();
        GetAndCachedQuestion(Managers.Social.GetCurrentMatchID(),
                             Managers.Trivia.CurrentTopicIndexSelected,
                             languagecode);
	}
    public void GetAndCachedQuestion(string matchID, int currentTopicIndex, string LanguageCode)
    {
        try
        {
            CachedQuestion = new TriviaQuestion();
            Managers.Trivia.SetCachedQuestion(CachedQuestion);

            string ParseObjectID = LanguageCode + Managers.Trivia.GetTopicName(currentTopicIndex);
            int CountQuestions = 0;
            CountQuestions = Managers.Trivia.GetCountQuestion(ParseObjectID);

        

            if (CountQuestions == 0)
            {
                qstatus = QUESTIONSTATUS.LOADEDSUCCESFULL;
                return;
            }
            
            int idwhichQuestion = Random.Range(0, CountQuestions);
            var query = ParseObject.GetQuery(ParseObjectID)
                        .WhereEqualTo("IdPregunta", idwhichQuestion);
                    
            query.FirstAsync().ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                        ParseObject obj = t.Result;

                        CachedQuestion.IdQuestion = obj.Get<int>("IdPregunta");
                        CachedQuestion.ObjectId = obj.ObjectId;
                        CachedQuestion.Question = obj.Get<string>("Pregunta");
                        CachedQuestion.indexAnswer = obj.Get<int>("indexanswer");
                        IList<object> opciones = obj.Get<List<object>>("opciones");

                        for (int i = 0; i < opciones.Count; i++)
                        {
                            CachedQuestion.options[i] = opciones[i].ToString();
                        }
                        qstatus = QUESTIONSTATUS.LOADEDSUCCESFULL;
                        Managers.Trivia.SetCachedQuestion(CachedQuestion);
                        return;
                }
                if (t.IsCanceled || t.IsFaulted)
                            qstatus = QUESTIONSTATUS.LOADEDFAIL;

                 

             });

        }
        catch (Exception ex)
        {
            qstatus= QUESTIONSTATUS.LOADEDFAIL;
            Debug.Log("Exception " + ex.Message);
        }
    }

    void RefreshGameTime()
    {
        timeLeft = (int)(endTime - Time.time);
        if (timeLeft <= 0)
        {
          timeLeft = 0;
        }
      
    }
	// Update is called once per frame
	void Update () {
	    if (qstatus == QUESTIONSTATUS.LOADEDSUCCESFULL && qstatus!=QUESTIONSTATUS.LOADEDTIMEOUT)
	    {
	        Application.LoadLevel("MatchPlayScene");
	    }
        if (qstatus == QUESTIONSTATUS.LOADEDFAIL || qstatus == QUESTIONSTATUS.LOADEDTIMEOUT)
	    {
	        if (labelError != null)
	        {
	            labelError.text = Localization.Localize("errorloadingquestion");
            }
	        if (ButtonGoBack != null)
	        {
                NGUITools.SetActive(ButtonGoBack.gameObject, true);
	        }
	    }
	    if (qstatus == QUESTIONSTATUS.LOADING)
	    {
            if (timeLeft > 0)
                RefreshGameTime();
            else
            {
                qstatus = QUESTIONSTATUS.LOADEDTIMEOUT;


            }


	    }


	}
}
