using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundMusicManager : MonoBehaviour {
	static public BackgroundMusicManager instance;
	public Dictionary<string, AudioSource> sounds = new Dictionary<string, AudioSource>();
	private AudioSource currentAudio=null;
	private AudioSource nextAudio = null;
	public float fadingSpeed;
	public float volume;
	// Use this for initialization
	void Awake () {
		instance = this;
		foreach (Transform child in transform) {
			sounds.Add (child.gameObject.name, child.GetComponent<AudioSource> ());
			child.GetComponent<AudioSource> ().volume = volume;
			child.GetComponent<AudioSource> ().Stop ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (nextAudio != null) {
			// Fade out
			if (currentAudio != null) {
				currentAudio.volume -= Time.deltaTime * fadingSpeed;
				if (currentAudio.volume <= 0) {
					currentAudio.Stop ();
					currentAudio = nextAudio;
					nextAudio = null;
					currentAudio.PlayDelayed (1);
					currentAudio.volume = 0;
				}
			} else {
				currentAudio = nextAudio;
				nextAudio = null;
				currentAudio.PlayDelayed (1);
				currentAudio.volume = 0;
			}
		} else {
			// Fade in
			if (currentAudio!=null && currentAudio.volume < volume) {
				currentAudio.volume = Mathf.Min(volume,currentAudio.volume+Time.deltaTime * fadingSpeed*0.1f);
			}
		}
	}

	// SoundManager.Play ("LongBackground");
	public static void Play(string soundName){
		if (instance.currentAudio == instance.sounds [soundName]) {
			return;
		}
		instance.nextAudio = instance.sounds [soundName];

	}


	static public void Pause(){
		if (instance.currentAudio != null) {
			instance.currentAudio.Pause ();
		}
	}

	static public void UnPause(){
		if (instance.currentAudio != null && instance.currentAudio.isPlaying==false) {
			instance.currentAudio.UnPause ();
		}
	}

	static public void UnPauseDelayed(float time){
		//instance.nonStaticUnPauseDelayed(time);
		if (instance.currentAudio != null) {
			instance.currentAudio.UnPause ();
			instance.currentAudio.volume =0;
		}
	}


	public void nonStaticUnPauseDelayed(float time){
		StartCoroutine(UnPauseDelayed());
	}
	IEnumerator UnPauseDelayed() {
		yield return new WaitForSeconds(2);
		if (instance.currentAudio != null) {
			instance.currentAudio.UnPause ();
		}
	}
}
