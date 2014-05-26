using UnityEngine;
using System.Collections;

public class ActionPlaySoundWithEmitter : Action {
	
	
	public AudioClip AudioClipAsSource;
	public string AudioFileNameAsSource;
	public float Volume;
	public Transform Emitter;
	
	public override void ActionPerformed ()
	{
	   if(AudioClipAsSource!=null)
			Managers.Audio.Play(AudioClipAsSource,Emitter,Volume);
	   else
			Managers.Audio.Play(AudioFileNameAsSource,Emitter,Volume,1);
	}
	
}
