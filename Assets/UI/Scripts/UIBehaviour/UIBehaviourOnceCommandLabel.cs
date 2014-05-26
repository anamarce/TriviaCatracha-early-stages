using UnityEngine;
using System.Collections;

public class UIBehaviourOnceCommandLabel : MonoBehaviour
{
	public UILabel Label =null;
	public CommandType Command;
	
	
	// Use this for initialization
	void Start () {
        Label.text = Commands.Instance.Command(Command.ToString(), "").ToString();
	}
	
}
