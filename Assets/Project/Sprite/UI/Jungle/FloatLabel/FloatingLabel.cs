using UnityEngine;
using System.Collections;

public class FloatingLabel : MonoBehaviour {

	public float upOffset = 10f;
	public float duration = 1f;
	UILabel label;
	private bool isStarted = false;
	private float startTime;
	private Vector3 originalScale;
	private Vector3 originalPosition;
	private Color originalColor;

	public void Initialize(Transform parent, string text){
		transform.SetParent (parent);
		transform.localScale = Vector3.zero;
		label.text = text;
		isStarted = true;
		startTime = Time.fixedTime;
	}
	// Use this for initialization
	void Awake () {
		label = GetComponent<UILabel> ();
		originalColor = label.color;
		originalScale = transform.localScale;
		originalPosition = transform.localPosition;
	}
	void Start(){
	}
	// Update is called once per frame
	void Update () {
		if (isStarted) {
			float currentStage = (Time.fixedTime - startTime) / duration;

			// constantly float upward
			transform.localPosition = new Vector3 (
				originalPosition.x,
				Util.EaseOut (currentStage, originalPosition.x, upOffset, 1f), 
				1f);
			
			if (currentStage >= 1f) {
				Destroy (gameObject);
			}

			// Apha
			if (currentStage < 0.1) {
				label.color = new Color (
					originalColor.r,
					originalColor.g,
					originalColor.b,
					Util.EaseOut (currentStage, 0f, 1f, 0.1f));
			} else if (currentStage > 0.5) {
				// Fade out
				label.color = new Color (
					originalColor.r,
					originalColor.g,
					originalColor.b,
					Util.EaseOut (currentStage - 0.5f, 1f, -1f, duration - 0.5f));
			} else{
				label.color = new Color (
					originalColor.r,
					originalColor.g,
					originalColor.b,
					1);
			}

			// Scale
			if (currentStage < 0.1f) {
				// Fade in
				transform.localScale = new Vector3 (
					Util.EaseOut (currentStage, originalScale.x*0.7f, 0.3f*originalScale.x, 0.1f), 
					Util.EaseOut (currentStage, originalScale.y*0.7f, 0.3f*originalScale.y, 0.1f),
					1f);
			} else {
				// Fade out
				transform.localScale = new Vector3 (
					Util.EaseOut (currentStage-0.1f, originalScale.x, -0.1f*originalScale.x, 0.9f), 
					Util.EaseOut (currentStage-0.1f, originalScale.y, -0.1f*originalScale.y, 0.9f), 
					1f);
			}
		}
	}
}
