using GooglePlayGames.BasicApi;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;

public class CloudManager : GooglePlayGames.BasicApi.OnStateLoadedListener
{
    private static CloudManager sInstance = new CloudManager();

    private GooglePlayGames.BasicApi.OnStateLoadedListener mAppStateListener;

    public static CloudManager Instance
    {
        get
        {
            return sInstance;
        }
    }
    private CloudManager()
    {
       // mProgress = GameProgress.LoadFromDisk();
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
