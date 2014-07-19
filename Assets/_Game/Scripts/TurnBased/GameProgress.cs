using UnityEngine;
using System.Collections;

public class GameProgress  {

	private const string PlayerPrefsKey = "triviageek-game-progress";

   // do we have modifications to write to disk/cloud?
    private bool mDirty = false;

    public GameProgress()
    {
    }

}
