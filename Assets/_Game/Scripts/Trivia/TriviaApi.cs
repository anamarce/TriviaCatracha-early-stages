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
	     IdQuestion=2;
	     Question="What is the  name of this Game?";
	     indexAnswer=1 ;
        options[0] = "No name";
        options[1] = "Trivia Geek Match";
        options[2] = "Tetris";
        options[3] = "Who cares";
         
    }
}

// Lo hace singleton
public class TriviaApi : MonoBehaviour {
	public   string[] TopicsParseKey = {"Anime","Comics","ComputerScience","ComputerSystems","Movies",
	                                          "Technology","TvSeries","VideoGames", "Books"};

	public  string[] LangParseCode = {"EN", "ES"};


    public int CurrentTopicIndexSelected = -1;
    public string CurrentTopicKey = "";



	public TriviaQuestion GetQuestion(string MatchId, string LangParseCode, string TopicParseKey)
	{
        var question = new TriviaQuestion();
		// Asegurarse de 
		// para un mismo MatchID, NO regresar aun random la misma pregunta, para ello supongo que
		// debera llevar registro interno de que preguntas ha entregado por match para saber cual no repetir
		// Las preguntas como tal tienen un ID correlativo o puede usar el ID generado por parse llamado objectId (string)

		// EL parse object para esta App esta formado po la concatenacion de  LangParseCode + TopicPArseKey
		// Por ej :   LangparseCode = "EN" , ingles y el topic es anime, entonces el object en parse esta
		// como :  ENAnime  y dentro de el estan todas las preguntas de Anime en Ingles.

		return question;
	}


    public string GetRandomTopic()
    {
        return "";
    }

    public string GetTopicName(int currentTopicIndex)
    {
        return TopicsParseKey[currentTopicIndex];
    }
}
