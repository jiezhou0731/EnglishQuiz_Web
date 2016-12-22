using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomPlayAudio : MonoBehaviour, MyAudio {

	List<AudioSource> audios = new List<AudioSource>();
	public void Play(){
		for (var i = 0; i < audios.Count; i++) {
			audios[i].Stop ();
		}
		audios [Random.Range (0, audios.Count)].Play ();
	}

	public void PlayDelayed(float time){
		for (var i = 0; i < audios.Count; i++) {
			audios[i].Stop ();
		}
		audios [Random.Range (0, audios.Count)].PlayDelayed (time);
	}


	public void Stop(){
		for (var i = 0; i < audios.Count; i++) {
			audios[i].Stop ();
		}
	}

	// Use this for initialization
	void Awake () {
		foreach (Transform child in transform) {
			audios.Add (child.GetComponent<AudioSource> ());
			audios [audios.Count - 1].Stop ();
		}
	}

}
