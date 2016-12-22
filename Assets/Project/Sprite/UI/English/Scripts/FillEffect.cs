using UnityEngine;
using System.Collections;

public class FillEffect : MonoBehaviour {
	public float delay = 0;
	public float duration;
	private float startTime=0;
	private UI2DSprite sprite;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<UI2DSprite> ();
		Hide ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.fixedTime - startTime - delay > 0) {
			sprite.fillAmount = (Time.fixedTime - startTime - delay) / duration;
		}
	}
	public void Trigger(){
		gameObject.SetActive (true);
		sprite.fillAmount = 0;
		startTime = Time.fixedTime;
	}

	public void Hide(){
		gameObject.SetActive (false);
	}
}
