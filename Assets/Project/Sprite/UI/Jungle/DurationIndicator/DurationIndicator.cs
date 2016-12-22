using UnityEngine;
using System.Collections;

public class DurationIndicator : MonoBehaviour {

	public float duration;
	private UI2DSprite sprite;
	private float startTime = 0;
	private string currentState = "Hide";

	// Use this for initialization
	void Start () {
		sprite = transform.FindChild ("Image").GetComponent<UI2DSprite> ();
		SetChildrenActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		float currentPercentage = (startTime + duration - Time.fixedTime) / duration;
		if (currentState == "Show") {
			if (currentPercentage>=0){
				sprite.fillAmount = currentPercentage;
			} else {
				sprite.fillAmount = 0;
				currentState = "Hide";
				SetChildrenActive (false);
			}
		} 


	}

	public void Trigger(){
		if (currentState == "Hide") {
			currentState = "Show";
			SetChildrenActive (true);
		}
		startTime = Time.fixedTime;
	}

	public void SetDuration(float duration){
		this.duration = duration;
	}

	public void Reset(){
		startTime = 0;
	}

	private void SetChildrenActive(bool active){
		foreach (Transform child in transform){
			child.gameObject.SetActive (active);
		}
	}
}
