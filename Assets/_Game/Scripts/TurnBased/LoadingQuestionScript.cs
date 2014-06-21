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
    LOADEDFAIL

};
public class LoadingQuestionScript : MonoBehaviour {

	// Use this for initialization
    public UILabel labelError;
    public UIButton ButtonGoBack;

    private TriviaQuestion CachedQuestion;
    private QUESTIONSTATUS qstatus;
	void Start () {
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
            string ParseObjectID = LanguageCode + Managers.Trivia.GetTopicName(currentTopicIndex);
            int CountQuestions = 0;


            //TODO: Considerar hacer loading de cuantas preguntas hay en cada topic
            // al inicio de la misma para no estar calculandolos aca nuevamente
            // Revisar tambien lo del timeout, en el update podria ser.
            ParseQuery<ParseObject> query = ParseObject.GetQuery(ParseObjectID)
                .WhereGreaterThan("IdPregunta", -1);


            if (query == null)
            {
                qstatus = QUESTIONSTATUS.LOADEDFAIL;
                return;
            }

            query.CountAsync().ContinueWith
                (t =>
                {

                    CountQuestions = t.Result;
                    if (CountQuestions == 0)
                    {
                        qstatus = QUESTIONSTATUS.LOADEDSUCCESFULL;
                        Managers.Trivia.SetCachedQuestion(CachedQuestion);
                        return;

                    }
                    int idwhichQuestion = Random.Range(0, CountQuestions);
                    var query2 = ParseObject.GetQuery(ParseObjectID)
                        .WhereEqualTo("IdPregunta", idwhichQuestion);
                    query2.FirstAsync().ContinueWith(t2 =>
                    {

                        if (t2.IsCompleted)
                        {
                            ParseObject obj = t2.Result;

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
                        if (t2.IsCanceled || t2.IsFaulted)
                            qstatus = QUESTIONSTATUS.LOADEDFAIL;

                        return;
                        

                    });


                }
                );


        }
        catch (Exception ex)
        {
            qstatus= QUESTIONSTATUS.LOADEDFAIL;
            Debug.Log("Exception " + ex.Message);
        }
    }
	// Update is called once per frame
	void Update () {
	    if (qstatus == QUESTIONSTATUS.LOADEDSUCCESFULL)
	    {
	        Application.LoadLevel("MatchPlayScene");
	    }
	    if (qstatus == QUESTIONSTATUS.LOADEDFAIL)
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
	}
}
