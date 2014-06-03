using UnityEngine;
using System.Collections;

public class UiRenderSignInOut : MonoBehaviour {

	public UILabel label;
	// Use this for initialization
	void Start () {
		UpdateLabel();
	}

	void UpdateLabel()
	{
		if(label==null) return;
		
		if (Managers.Social.IsAuthenticated())
			label.text = Localization.Localize("signout");
		else
			label.text = Localization.Localize("signin");
	}

	// Update is called once per frame
	void Update () {
		UpdateLabel();
	}
}
