using UnityEngine;
using System.Collections;

public class DelayStartScene : MonoBehaviour
{

    public float DelayTime = 1F;
    public string SceneName;
	// Use this for initialization
	void Start () {
	     Invoke("LoadScene",DelayTime);
	}
	
	
	void LoadScene()
    {
	  Application.LoadLevel(SceneName);
	}
}
