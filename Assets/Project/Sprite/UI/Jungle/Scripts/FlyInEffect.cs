using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyInEffect : MonoBehaviour {

	// use them causing strange NaN problem. Probably unity's bug. So use their actual value's in the code.

	public bool isActive = false;
	public float delayTime = 1f;
	public float duration=0.5f;

	public float flyInPercent=50f;
	public float shrinkPercent=30;

	public float startOffset = 2000;

	private Vector3 originalPosition;
	private Vector3 originalScale;
	private float shrinkPercentFloat;
	private float flyInPercentFloat;

	private List<UI2DSprite> fadingSprites= new List<UI2DSprite> ();
	void Start () {
		originalPosition = transform.localPosition;
		originalScale = transform.localScale;
		shrinkPercentFloat = shrinkPercent / 100f;
		flyInPercentFloat = flyInPercent / 100f;
		addFadingSprites (transform);
	}

	void addFadingSprites(Transform root){
		try {
			fadingSprites.Add (root.GetComponent<UI2DSprite> ());
		} catch (System.Exception e){
		}
	}

	void ChangeAllAphaTo(float alpha){
		try {
			/*
			foreach (UI2DSprite sprite in fadingSprites) {
				sprite.color = new Color (1, 1, 1, alpha);
			}
			*/
		}
		catch (System.Exception e){
		}
	}

	private float flyInSpeed;
	// Update is called once per frame
	void Update () {
		if (effectIsHappenning) {
			if (Time.fixedTime >= startTime + duration) {
				effectIsHappenning = false;
				transform.localPosition = originalPosition;
				transform.localScale = originalScale;
				ChangeAllAphaTo (1);
			} else {
				float currentStage = (Time.fixedTime - startTime) / duration;//duration;
				if (currentStage < flyInPercentFloat) {
					// Fly in
					float currentDistance = Time.deltaTime * flyInSpeed;
					transform.localPosition = transform.localPosition+new Vector3(currentDistance,0f,0f);

					ChangeAllAphaTo (Mathf.SmoothStep (0, 1, currentStage * 2));
				} else {
					//Fly in finished
					ChangeAllAphaTo (1);
					transform.localPosition = originalPosition;

					// Scale
					float currentScaleStage = (Time.fixedTime - startTime - flyInPercentFloat * duration) / ((1-flyInPercentFloat) * duration);
					if (currentScaleStage < 0.5) {
						// Shrink
						transform.localScale = new Vector3 (
							Mathf.Lerp (originalScale.x, (1f - shrinkPercentFloat) * originalScale.x, currentScaleStage * 2f),
							Mathf.Lerp (originalScale.y, (1f - shrinkPercentFloat) * originalScale.y, currentScaleStage * 2f),
							1);

					} else {
						// Expand
						transform.localScale = new Vector3 (
							Mathf.Lerp ((1f - shrinkPercentFloat) * originalScale.x, originalScale.x, (currentScaleStage-0.5f) * 2f),
							Mathf.Lerp ((1f - shrinkPercentFloat) * originalScale.y, originalScale.y, (currentScaleStage-0.5f) * 2f),
							1);
					}
				}
			}
		}
	}

	private bool effectIsHappenning=false;
	private float startTime = 0;
	public void Trigger() {
		if (isActive) {
			StartCoroutine (StartEffect ());
		}
	}

	IEnumerator StartEffect() {
		transform.localPosition = new Vector3 (
			originalPosition.x + startOffset,
			originalPosition.y,
			originalPosition.z);
		yield return new WaitForSeconds(delayTime);

		SoundManager.Play ("FlyIn");
		if (flyInPercentFloat <= 0) {
			flyInPercentFloat = 0.5f;
		}

		startTime = Time.fixedTime;
		effectIsHappenning = true;
		transform.localPosition = new Vector3 (
			originalPosition.x + startOffset,
			originalPosition.y,
			originalPosition.z);
		flyInSpeed =  (-startOffset)/(duration*flyInPercentFloat);
		ChangeAllAphaTo (0);
	}

}
