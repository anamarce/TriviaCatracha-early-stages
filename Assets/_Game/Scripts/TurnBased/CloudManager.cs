using GooglePlayGames.BasicApi;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;

public class CloudManager : GooglePlayGames.BasicApi.OnStateLoadedListener
{
    private static CloudManager sInstance = new CloudManager();

    private GameProgress mProgress;
  
    // list of achievements we know we have unlocked (to avoid making repeated calls to the API)
    private Dictionary<string, bool> mUnlockedAchievements = new Dictionary<string, bool>();
    // achievement increments we are accumulating locally, waiting to send to the games API
    private Dictionary<string, int> mPendingIncrements = new Dictionary<string, int>();

    // what is the highest score we have posted to the leaderboard?
    private int mHighestPostedScore = 0;  // In this case the amount of matchs the player wins


    private GooglePlayGames.BasicApi.OnStateLoadedListener mAppStateListener;
    public static CloudManager Instance
    {
        get
        {
            return sInstance;
        }
    }
    public bool Authenticated
    {
        get
        {
            return Social.Active.localUser.authenticated;
        }
    }
   
    private CloudManager()
    {
        mProgress = GameProgress.LoadFromDisk();
       
    }
    public void LoadFromCloud()
    {
        // Cloud save is not in ISocialPlatform, it's a Play Games extension,
        // so we have to break the abstraction and use PlayGamesPlatform:
        Debug.Log("Loading game progress from the cloud.");
        ((PlayGamesPlatform)Social.Active).LoadState(0, this);
    }

     public void OnStateLoaded(bool success, int slot, byte[] data) 
     {
      
     }

    public byte[] OnStateConflict(int slot, byte[] local, byte[] server)
    {
        return null;
    }

    public void OnStateSaved(bool success, int slot)
    {
    }


}
