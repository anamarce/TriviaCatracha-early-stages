using UnityEngine;
using System.Collections;

public class PanelScript : MonoBehaviour {

	// Use this for initialization

	public UIPanel myPanel;
	public string name;
	void Start () {

	}
	void OnDisable() 
	{
		print("script was removed");
	}

	void OnEnable()
	{

		print (gameObject.transform.localPosition);
			print("script enable");
		

	}
	// Update is called once per frame
	void Update () {

	

	}
}
