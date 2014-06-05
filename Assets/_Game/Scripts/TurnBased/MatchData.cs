

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public struct Geek {
	public string id;
	public string name;
	public int correctAnswers;


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
    private const int Header = 100100; // Para numero de version
	private int numberplayers=0;
	private int status=0;
	private string datematch="";
	private string language="";
	private int CurrentIndexPlayer=0;
	private List<Geek> geeks = new List<Geek>();
	private string GeekIdWon="";
     

    public MatchData() {

    }

    public MatchData(byte[] b) : this() {
        if (b != null) {
           // ReadFromBytes(b);
            ComputeWinner();
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
 /*   private void ReadFromBytes(byte[] b) {
        BinaryReader r = new BinaryReader(new MemoryStream(b));
        int header = r.ReadInt32();
        if (header != Header) {
            // we don't know how to parse this version; user has to upgrade game
            throw new UnsupportedMatchFormatException("Board data header " + header +
                    " not recognized.");
        }

        int len = (int)r.ReadByte();
        mParticipantIdX = new string(r.ReadChars(len));

        int x;
        for (x = 0; x < mBoard.Length; x++) {
            mBoard[x] = r.ReadChars(mBoard.Length);
        }
        ComputeWinner();

        mBlockDescs.Clear();
        int blockDescs = r.ReadInt32(), i;
        for (i = 0; i < blockDescs; i++) {
            float px, py, pz, rx, ry, rz, rw;
            char mark = r.ReadChar();
            px = r.ReadSingle();
            py = r.ReadSingle();
            pz = r.ReadSingle();
            rx = r.ReadSingle();
            ry = r.ReadSingle();
            rz = r.ReadSingle();
            rw = r.ReadSingle();
            mBlockDescs.Add(new BlockDesc(mark, new Vector3(px, py, pz),
                    new Quaternion(rx, ry, rz, rw)));
        }
    }
*/
  
    public class UnsupportedMatchFormatException : System.Exception {
        public UnsupportedMatchFormatException(string message) : base(message) {}
    }
}
