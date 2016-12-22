using UnityEngine;
using System.Collections;


public class MenuButton : MonoBehaviour {
	private Vector3 originalScale;
	private string state="isReleased";
	private Transform background;
	public ParticleSystem blinkStar;
	void Start () {
		background = transform.FindChild("Background").transform;
		originalScale = background.transform.localScale;
		blinkStar.Stop ();
		blinkStar.loop = true;
	}

	// Update is called once per frame
	void Update () {
		if (isStarted) {
			if (state == "IsPressed") {
				if (blinkStar.isPlaying == false) {
					blinkStar.Play ();
				}
				float t = (Time.fixedTime - startTime) / duration;
				if (t > 1) {
					isStarted = false;
					ChangeColor (gameObject, pressedColor);
					background.transform.localScale = originalScale * (1f-offsetPercent);
				}
				ChangeColor (gameObject, Color.Lerp (releasedColor, pressedColor, t));
				background.transform.localScale = new Vector3 (
					Mathf.SmoothStep(originalScale.x, originalScale.x * (1f-offsetPercent), t),
					Mathf.SmoothStep(originalScale.y, originalScale.y * (1f-offsetPercent), t),
					1);
			} else if (state == "IsReleased") {
				float t = (Time.fixedTime - startTime) / duration;
				if (t > 1) {
					isStarted = false;
					ChangeColor (gameObject, releasedColor);
					background.transform.localScale = originalScale;
				}
				ChangeColor (gameObject,  Color.Lerp (pressedColor, releasedColor, t));
				background.transform.localScale = new Vector3 (
					Mathf.SmoothStep(originalScale.x * (1f-offsetPercent), originalScale.x, t),
					Mathf.SmoothStep(originalScale.y * (1f-offsetPercent), originalScale.y, t),
					1);
			}
		}
	}

	private float startTime;
	public float duration=0.2f;
	public float offsetPercent = 0.1f;
	private bool isStarted = false;
	public Color pressedColor;
	public Color releasedColor;
	public void ButtonClicked(){
		state = "IsPressed";
		isStarted = true;
		startTime = Time.fixedTime;
	}

	public void ButtonReleased(){
		state = "IsReleased";
		isStarted = true;
		startTime = Time.fixedTime;
		blinkStar.Stop ();
	}

	public bool IsPressed(){
		return state == "IsPressed";
	}
	private void ChangeColor(GameObject component, Color color){
		Transform background = component.transform.FindChild ("Background");
		foreach (Transform child in background) {
			child.GetComponent<UI2DSprite> ().color = color;
		}
	}
}
