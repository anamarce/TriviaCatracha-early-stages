

using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Geek {
	public string id;
	public string name;
	public int correctAnswers;

    public Geek()
    {
        this.id = "";
        this.correctAnswers = 0;
    }
	public Geek(string id, int answers) {

		this.id=id;
		this.correctAnswers = answers;
	}
	public override string ToString () {
		return "[Geek: '" + id + "' " +  name +" , Answers=" + correctAnswers + "]";
	}
};

public class MatchData
{
    private const int Header = 201461; // Para numero de version
    public int numberplayers = 0;
    public int status = 0;
    public int topanswers = 10;
    public string language = "";
    public string GeekIdWon = "";
    public string CurrentPlayer = "";
    public int IndexCurrentPlayer = 0;

    public List<Geek> geeks = new List<Geek>();



    public MatchData()
    {

    }

    public int GetScoreParticipantID(string participantid)
    {
        foreach (Geek geek in geeks)
        {
            if (geek.id == participantid)
                return geek.correctAnswers;
        }
        return 0;
    }

    public MatchData(byte[] b) : this()
    {
        if (b != null)
        {
            ReadFromBytes(b);
            ComputeWinner();
        }
        else
        {
            
            Debug.Log("Game has started, matchDataNull");
        }
    }

    private void ComputeWinner()
    {

    }

    public byte[] ToBytes()
    {
        MemoryStream memStream = new MemoryStream();
        BinaryWriter w = new BinaryWriter(memStream);
        w.Write(Header);
        w.Write(geeks.Count);
        w.Write(this.status);
        w.Write(this.topanswers);
        w.Write(this.language);
        w.Write(this.GeekIdWon);
        w.Write(this.CurrentPlayer);

        foreach (Geek g in geeks)
        {
            w.Write(g.id);
            w.Write(g.correctAnswers);

        }
        w.Close();
        byte[] buf = memStream.GetBuffer();
        memStream.Close();
        return buf;
    }

    private void ReadFromBytes(byte[] b)
    {
        BinaryReader r = new BinaryReader(new MemoryStream(b));
        int header = r.ReadInt32();
        if (header != Header)
        {
            // Wrong header
            throw new UnsupportedMatchFormatException("Match data header " + header +
                                                      " not recognized.");
        }

        this.numberplayers = r.ReadInt32();
        this.status = r.ReadInt32();
        this.topanswers = r.ReadInt32();
        this.language = r.ReadString();
        this.GeekIdWon = r.ReadString();
        this.CurrentPlayer = r.ReadString();

        for (int i = 0; i < this.numberplayers; i++)
        {
            string id = r.ReadString();
            int answers = r.ReadInt32();
            Geek temp = new Geek(id, answers);
            geeks.Add(temp);

        }
        ComputeWinner();


    }


    public class UnsupportedMatchFormatException : System.Exception
    {
        public UnsupportedMatchFormatException(string message) : base(message)
        {
        }
    }

    public void SetInitialMatchData(TurnBasedMatch mMatch, string matchLanguage, int totalanswers)
    {
        geeks.Clear();

        this.numberplayers = mMatch.Participants.Count;
        this.status = 0;
        this.topanswers = totalanswers;
        this.language = matchLanguage;
        this.GeekIdWon = "-1";
        this.CurrentPlayer = mMatch.SelfParticipantId;
        this.IndexCurrentPlayer = 0;
        foreach (Participant participant in mMatch.Participants)
        {
            Geek temp = new Geek(participant.ParticipantId, 0);
            geeks.Add(temp);
        }
        
    }

    public override string ToString()
    {
        /*          public int numberplayers = 0;
    public int status = 0;
    public int topanswers = 10;
    public string language = "";
    public string GeekIdWon = "";
    public string CurrentPlayer = "" ; */
        string theGeeks = "";
        foreach (Geek geek in geeks)
        {
            theGeeks = theGeeks + "[" + geek.id + "-"+ geek.correctAnswers.ToString() + "]";
        }
        return string.Format(@"Players={0},Status={1},TopAnswers={2},language={3},
                              GeekidWon={4},CurrentPlayer={5},Geeks={6}", numberplayers, status,
            topanswers, language, GeekIdWon, CurrentPlayer,theGeeks);

    }
}
