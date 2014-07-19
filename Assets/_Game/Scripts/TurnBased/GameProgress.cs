using UnityEngine;
using System.Collections;

public class GameProgress  {

    private const string PlayerPrefsKey = "triviageek-game-progress";
    private int mHighestPostedScore = 0;

  // do we have modifications to write to disk/cloud?
    private bool mDirty = false;

    public GameProgress()
    {
        mHighestPostedScore = 0;
    }
    public static GameProgress LoadFromDisk()
    {
        string s = PlayerPrefs.GetString(PlayerPrefsKey, "");
        if (s == null || s.Trim().Length == 0)
        {
            return new GameProgress();
        }
        return GameProgress.FromString(s);
    }

    public static GameProgress FromString(string s)
    {
        GameProgress gp = new GameProgress();
        //TODO logic
        return gp;
    }

}
