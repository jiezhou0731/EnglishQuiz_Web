using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ColorChangingButton : MonoBehaviour {
	private Vector3 originalScale;
	private string state="IsReleased";
	private string lastState="IsReleased";
	private List<UI2DSprite> sprites = new List<UI2DSprite>();
	private UIButton uiButton;
	void Start () {
		uiButton = GetComponent<UIButton> ();
		foreach (Transform child in transform) {
			if (child.GetComponent<UI2DSprite> () != null) {
				sprites.Add (child.GetComponent<UI2DSprite> ());
			}
		}
	}

	void ChangeColorTo(Color color){
		foreach (UI2DSprite sprite in sprites) {
			//sprite.color = color;
		}
	}


	void UpdateState(){
		if (uiButton.IsPressed ()) {
			state = "IsPressed";
		} else {
			state = "IsReleased";
		}

		if (lastState!=state){
			if (state == "IsPressed") {
				ButtonClicked ();
			} else {
				ButtonReleased ();
			}
		}
		lastState = state;
	}
	// Update is called once per frame
	void Update () {
		UpdateState ();
		if (isStarted) {
			if (state == "IsPressed") {
				float t = (Time.fixedTime - startTime) / duration;
				if (t > 1) {
					isStarted = false;
					ChangeColorTo(pressedColor);
				}
				ChangeColorTo( Color.Lerp (releasedColor, pressedColor, t));
			} else if (state == "IsReleased") {
				float t = (Time.fixedTime - startTime) / duration;
				if (t > 1) {
					isStarted = false;
					ChangeColorTo(releasedColor);
				}
				ChangeColorTo( Color.Lerp (pressedColor, releasedColor, t));
			}
		}
	}

	private float startTime;
	public float duration=0.2f;
	private bool isStarted = false;
	public Color pressedColor;
	public Color releasedColor;
	public void ButtonClicked(){
		isStarted = true;
		startTime = Time.fixedTime;
	}

	public void ButtonReleased(){
		isStarted = true;
		startTime = Time.fixedTime;
	}
}
