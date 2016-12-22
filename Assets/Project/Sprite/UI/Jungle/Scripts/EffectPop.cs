using UnityEngine;
using System.Collections;

public class EffectPop : MonoBehaviour {
	private Vector3 originalScale;
	void Start () {
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (isStarted) {
			float t = (Time.fixedTime - startTime) / duration;
			if (t > 1) {
				isStarted = false;
			}
			if (t < duration * percent) {
				transform.localScale = new Vector3 (
					Util.EaseOut (t, originalScale.x, originalScale.x * offsetPercent, duration * percent), 
					Util.EaseOut (t, originalScale.y, originalScale.y * offsetPercent, duration * percent), 
					1);
			} else {
				transform.localScale = new Vector3 (
					Util.EaseOut (t, originalScale.x * (1 + offsetPercent), -originalScale.x * offsetPercent, duration * (1 - percent)), 
					Util.EaseOut (t, originalScale.y * (1 + offsetPercent), -originalScale.y * offsetPercent, duration * (1 - percent)), 
					1);
			}
		} else {
			transform.localScale = originalScale;
		}
	}

	private float startTime;
	public float duration=0.2f;
	public float percent = 0.1f;
	public float offsetPercent = 0.1f;
	private bool isStarted = false;
	public void Trigger(){
		isStarted = true;
		startTime = Time.fixedTime;
	}
}
