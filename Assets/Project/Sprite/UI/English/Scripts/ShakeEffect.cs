using UnityEngine;
using System.Collections;

public class ShakeEffect : MonoBehaviour {

	public bool triggerredFromStart = false;
	public float duration;
	public float speed = 10.0f; //how fast it shakes
	public float amount = 0.2f; //how much it shakes
	public float interval = 2f;
	private CooldownTimer intervalTimer;
	public float durationBetweenInterval = 0.5f;
	private CooldownTimer durationBetweenIntervalTimer;

	private bool isShaking = false;
	private Vector3 originalPosition;

	private bool isAtInterval = false;
	void Start(){
		originalPosition = transform.localPosition;
		if (triggerredFromStart) {
			Trigger ();
		}
		intervalTimer = Util.GetCooldownTimer (transform, interval, false);
		durationBetweenIntervalTimer = Util.GetCooldownTimer (transform, durationBetweenInterval, false);
		isAtInterval = false;
	}
	void Update()
	{
		if (isShaking) {
			if (isAtInterval) {
				if (intervalTimer.IsCool ()) {
					isAtInterval = false;
					durationBetweenIntervalTimer.Heat ();
				}
			} else {
				if (durationBetweenIntervalTimer.IsCool ()) {
					isAtInterval = true;
					intervalTimer.Heat ();
				}
			}
			if (isAtInterval == false) {
				if ((Time.fixedTime - startTime) < duration) {
					float offset = Mathf.Sin (Time.fixedTime * speed) * amount;
					transform.localPosition = new Vector3 (
						originalPosition.x + offset * 1.5f,
						originalPosition.y + offset,
						originalPosition.z);
				} else {
					float offset = Mathf.Sin (Time.fixedTime * speed) * amount;
					// Close to original position, can stop.
					if (Mathf.Abs (offset) < speed * Time.deltaTime * 3) {
						isShaking = false;
						transform.localPosition = originalPosition;
					} else {
						transform.localPosition = new Vector3 (
							originalPosition.x + offset * 1.5f,
							originalPosition.y + offset,
							originalPosition.z);
					}
				}
			}
		}
	}

	float startTime;
	public void Trigger(){
		isShaking = true;
		startTime = Time.fixedTime;
	}
}
