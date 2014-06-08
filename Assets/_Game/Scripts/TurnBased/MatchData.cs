

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
        this.name = "";
        this.correctAnswers = 0;
    }
	public Geek(string id, string name,int answers) {

		this.id=id;
		this.name=name;
		this.correctAnswers = answers;
	}
	public override string ToString () {
		return "[Geek: '" + id + "' " +  name +" , Answers=" + correctAnswers + "]";
	}
};

public class MatchData {
    private const int Header = 201461; // Para numero de version
    public int numberplayers = 0;
    public int status = 0;
    public string datematch = "";
    public string language = "";
    public int CurrentIndexPlayer = -1;
    public List<Geek> geeks = new List<Geek>();
	public string GeekIdWon="";
     

    public MatchData() {

    }

    public MatchData(byte[] b) : this() {
        if (b != null) {
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

   /* public byte[] ToBytes() {
        MemoryStream memStream = new MemoryStream();
        BinaryWriter w = new BinaryWriter(memStream);
        w.Write(Header);
        w.Write((byte)mParticipantIdX.Length);
        w.Write(mParticipantIdX.ToCharArray());
        int x;
        for (x = 0; x < mBoard.Length; x++) {
            w.Write(mBoard[x]);
        }
        w.Write(mBlockDescs.Count);
        foreach (BlockDesc b in mBlockDescs) {
            w.Write(b.mark);
            w.Write(b.position.x);
            w.Write(b.position.y);
            w.Write(b.position.z);
            w.Write(b.rotation.x);
            w.Write(b.rotation.y);
            w.Write(b.rotation.z);
            w.Write(b.rotation.w);
        }
        w.Close();
        byte[] buf = memStream.GetBuffer();
        memStream.Close();
        return buf;
    }
*/
   private void ReadFromBytes(byte[] b) {
        BinaryReader r = new BinaryReader(new MemoryStream(b));
        int header = r.ReadInt32();
        if (header != Header) {
            // Wrong header
            throw new UnsupportedMatchFormatException("Match data header " + header +
                    " not recognized.");
        }

       this.numberplayers = r.ReadInt32();
       this.status = r.ReadInt32();
       datematch=r.ReadString();
       language = r.ReadString();
	   CurrentIndexPlayer=r.ReadInt32();
       
       ComputeWinner();

       
    }

  
    public class UnsupportedMatchFormatException : System.Exception {
        public UnsupportedMatchFormatException(string message) : base(message) {}
    }
}
