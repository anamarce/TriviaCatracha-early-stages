using UnityEngine;
using System.Collections;

public class UIBehaviourCommandLabel : MonoBehaviour {
	public UILabel Label =null;
	public CommandType Command;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	        Label.text = Commands.Instance.Command(Command.ToString(),"").ToString();
	}
}
