using UnityEngine;
using System.Collections;

public interface MyAudio {
	
	void Play();
	void Stop();

	void PlayDelayed(float time);
}
