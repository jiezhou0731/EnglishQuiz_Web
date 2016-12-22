using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {
	static public SoundManager instance;
	public Dictionary<string, MyAudio> sounds = new Dictionary<string, MyAudio>();
	// Use this for initialization
	void Awake () {
		instance = this;
		foreach (Transform child in transform) {
			sounds.Add (child.gameObject.name, child.GetComponent<MyAudio> ());
		}
	}

	void GetAllAudioSources(){
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// SoundManager.Play ("LongBackground");
	public static void Play(string soundName){
		instance.sounds [soundName].Play ();
	}

	public static void Stop(string soundName){
		instance.sounds [soundName].Stop ();
	}
		
	public void Test(){
		SoundManager.Play ("LongBackground");
	}

	public void Test1(){
		SoundManager.Play ("LongBackground1");
	}
}

