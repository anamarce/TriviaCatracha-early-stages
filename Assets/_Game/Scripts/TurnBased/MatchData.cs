


using UnityEngine;

using System.IO;




public class MatchData
{
    private const int Header = 201402; // Para numero de version

    public string CurrentPlayer = "";
    public string Player1 = "";
    public string Player2 = "";

    private int Player1Answers = 0;
    private int Player2Answers = 0;

    private bool Player1Wins = false;
    private bool Player2Wins = false;


    public int topanswers = 5;
    public string PlayerWon = "";

    
    public MatchData()
    {

    }

    public int GetScoreParticipantID(string participantid)
    {
        if (participantid == Player1)
        {
            return Player1Answers;
        }
        else
        {
            return Player2Answers;
        }
        
    }


    public void AddScoreParticipantID(string participantid, int answers)
    {
        if (participantid == Player1)
        {
            Player1Answers+=answers;
        }
        else
        {
            Player2Answers+=answers;
        }
    }


    public MatchData(byte[] b) : this()
    {
        if (b != null)
        {
            ReadFromBytes(b);
        
          
        }
        else
        {
            
            Debug.Log("Game has started, matchDataNull");
        }
    }

   
    public byte[] ToBytes()
    {
        MemoryStream memStream = new MemoryStream();
        BinaryWriter w = new BinaryWriter(memStream);
        w.Write(Header);
        w.Write(this.CurrentPlayer);
        w.Write(this.Player1);
        w.Write(this.Player2);
        w.Write(this.Player1Answers);
        w.Write(this.Player2Answers);
        w.Write(this.topanswers);
        w.Write(this.PlayerWon);
        
        
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
        this.CurrentPlayer = r.ReadString();
        this.Player1 = r.ReadString();
        this.Player2 = r.ReadString();
        this.Player1Answers = r.ReadInt32();
        this.Player2Answers = r.ReadInt32();
        this.topanswers = r.ReadInt32();
        this.PlayerWon = r.ReadString();



    }


    public class UnsupportedMatchFormatException : System.Exception
    {
        public UnsupportedMatchFormatException(string message) : base(message)
        {
        }
    }

   

    public override string ToString()
    {
      
      
        return string.Format(@"Player1={0},AnswersPlayer1={1},Player2={2},AnswersPlayer2={3},
                              CurrentPlayer={4}",Player1,Player1Answers,Player2,Player2Answers,
                                                CurrentPlayer);

    }
}
