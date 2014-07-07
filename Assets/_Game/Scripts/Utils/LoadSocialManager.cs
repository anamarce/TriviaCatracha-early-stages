using UnityEngine;
using System.Collections;

public class LoadSocialManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
     
		Managers.Social.SignIn(false);
        Managers.Trivia.LoadTopicsStats();
        Managers.Social.RegisterDelegates();
	}
	

}
