using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Agregar el namespace de parse
using Parse;
// PAra queries en parse https://parse.com/docs/unity_guide#queries

public class TriviaQuestion
{
	public string ObjectId;
	public int IdQuestion;
	public string Question;
	public int indexAnswer ;
	public string [] options = {"","","",""} ;  // siempre sera un array de 4 strings

    public TriviaQuestion()
    {
        // Just for testing
         ObjectId="1";
	     IdQuestion=0;
	     Question="What is the  name of this Game?";
	     indexAnswer=1 ;
        options[0] = "No name";
        options[1] = "Trivia Geek Match";
        options[2] = "Tetris";
        options[3] = "Who cares";
         
    }

    public void Shuffle()
    {
        //Metodo para Shuffle de las opciones y actualizar correctamente el index
        return;
    }
}

// Lo hace singleton
public class TriviaApi : MonoBehaviour {
	public   string[] TopicsParseKey = {"Anime","Comics","ComputerScience","ComputerSystems","Movies",
	                                          "Technology","TvSeries","VideoGames", "Books"};

	public  string[] LangParseCode = {"EN", "ES"};


    public int CurrentTopicIndexSelected = -1;
    public string CurrentTopicKey = "";

    private TriviaQuestion CachedQuestion=null;

	public TriviaQuestion GetCachedQuestion()
	{
	    if (CachedQuestion != null)
	    {
	        if (CachedQuestion.IdQuestion >= 0)
	        {
	            return CachedQuestion;
	        }

	    }
	    return null;

	}


  

    public string GetTopicName(int currentTopicIndex)
    {
        return TopicsParseKey[currentTopicIndex];
    }

    public void GetAndCachedQuestion(string matchID, int currentTopicIndex,string LanguageCode)
    {
        CachedQuestion = new TriviaQuestion();
        // Get it from PARSE


    }
    
}
