using UnityEngine;
using System.Collections;

public class SimpleAudio : MonoBehaviour, MyAudio {

	AudioSource audio;
	public void Play(){
		audio.Play ();
	}

	public void PlayDelayed(float time){
		audio.PlayDelayed (time);
	}


	public void Stop(){
		audio.Stop ();
	}

	// Use this for initialization
	void Awake () {
		audio = GetComponent<AudioSource> ();
		audio.Stop ();
	}

}
