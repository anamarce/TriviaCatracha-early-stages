using UnityEngine;
using System.Collections;

public class UIBehaviourHealthBar : MonoBehaviour {

	// Use this for initialization
	public UISlider ProgressBar;
	public CommandType CurrentValueCommand;
	public CommandType MaxValueCommand;
	public  float maxValue;
	public float currValue;
	void Start () {
		
		ProgressBar = GetComponent<UISlider>();
		if(ProgressBar==null)
		{
		  Debug.LogError("Could not found Slider");
		  return;
		  	
		}
	    maxValue= float.Parse(Commands.Instance.Command(MaxValueCommand.ToString(),"").ToString());
		
	}
	
	// Update is called once per frame
	void Update () {
	   
		 
	    currValue= float.Parse(Commands.Instance.Command(CurrentValueCommand.ToString(),"").ToString());
		ProgressBar.sliderValue = currValue / maxValue ;
		
	}
}
