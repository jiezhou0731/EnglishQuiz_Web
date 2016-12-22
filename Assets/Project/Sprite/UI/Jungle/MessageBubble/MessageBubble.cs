using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageBubble : MonoBehaviour {
	private float duration=-1f;
	private float startTime;
	private bool isFading = false;
	public float fadeDuration = 0.5f;
	private float fadeStartTime;
	public string text;

	private Vector3 startLocalPosition;
	private Vector3 targetLocalPosition;
	public Vector3 TargetLocalPosition
	{
		get { return this.targetLocalPosition; }
	}
	private float positionTransitDuration;
	private float positionTransitStartTime;

	private Vector3 startLocalScale;
	private Vector3 targetLocalScale;
	private float scaleTransitDuration;
	private float scaleTransitStartTime;

	public void Initialize(string text, Vector3 startPosition,float duration){
		this.text = text;
		transform.FindChild ("Text").GetComponent<UILabel> ().text = text;
		transform.localScale = Vector3.zero;
		transform.localPosition = startPosition;
		this.duration = duration;
		startTime = Time.fixedTime; 
		addFadingObjects (transform); 
	}
		
	// Update is called once per frame
	void Update () {
		// Position transition
		if (targetLocalPosition != transform.localPosition) {
			//move by time
			float x = Mathf.Lerp (
				startLocalPosition.x, 
				targetLocalPosition.x, 
				(Time.fixedTime-positionTransitStartTime) / positionTransitDuration);
			float y = Mathf.SmoothStep (
				startLocalPosition.y, 
				targetLocalPosition.y, 
				(Time.fixedTime-positionTransitStartTime) / positionTransitDuration);
			float z = Mathf.SmoothStep (
				startLocalPosition.z, 
				targetLocalPosition.z, 
				(Time.fixedTime-positionTransitStartTime) / positionTransitDuration);
			transform.localPosition = new Vector3 (x, y, z);
		
		}
		// Scale transition
		if (targetLocalScale != transform.localScale) {
			float x = Mathf.SmoothStep (
					startLocalScale.x, 
					targetLocalScale.x, 
					(Time.fixedTime-scaleTransitStartTime) / scaleTransitDuration);
			float y = Mathf.SmoothStep (
				startLocalScale.y, 
				targetLocalScale.y, 
				(Time.fixedTime-scaleTransitStartTime) / scaleTransitDuration);
			float z = Mathf.SmoothStep (
				startLocalScale.z, 
				targetLocalScale.z, 
				(Time.fixedTime-scaleTransitStartTime) / scaleTransitDuration);
			transform.localScale = new Vector3 (x, y, z);
		}
	
		// Fade
		if (isFading == false) {
			if ((duration != -1 && Time.fixedTime - startTime > duration)
				||transform.localPosition.y<-130f*2) {
				StartFading ();
			}
		} else {
			ReduceAphaTo (Mathf.SmoothStep (1, 0, (Time.fixedTime - fadeStartTime) / fadeDuration));
			// Destroy
			if (Time.fixedTime - fadeStartTime > fadeDuration) {
				MessageBubbleManager.instance.RemoveMessage (this);
				Destroy (gameObject);
			}
		}
	}

	public void SetTargetLocalPosition(Vector3 position, float transitDuration = 0.2f){
		startLocalPosition = transform.localPosition;
		targetLocalPosition = position;
		this.positionTransitDuration = transitDuration;
		this.positionTransitStartTime = Time.fixedTime;
	}

	public void SetTargetLocalScale(Vector3 scale, float transitDuration = 0.1f){
		startLocalScale = transform.localScale;
		targetLocalScale = scale;
		this.scaleTransitDuration = transitDuration;
		this.scaleTransitStartTime = Time.fixedTime;
	}


	private void StartFading(){
		fadeStartTime = Time.fixedTime;
		isFading = true;
	}
	private List<UI2DSprite> fadingSprites= new List<UI2DSprite> (); 
	private List<UILabel> fadingLabels= new List<UILabel> (); 
	void addFadingObjects(Transform root){ 
		try { 
			if (root.GetComponent<UI2DSprite> ()!=null){
				fadingSprites.Add (root.GetComponent<UI2DSprite> ()); 
			}
		} catch (System.Exception e){ 
		} 
		try { 
			if (root.GetComponent<UILabel> ()!=null){
				fadingLabels.Add (root.GetComponent<UILabel> ()); 
			}
		} catch (System.Exception e){ 
		} 
		foreach (Transform child in root) {
			addFadingObjects (child);
		}
	} 

	void ReduceAphaTo(float alpha){ 
		foreach (UI2DSprite item in fadingSprites) { 
			if (item.color.a>alpha){
				item.color = new Color (item.color.r, item.color.g, item.color.b, alpha); 
			}
		} 
		foreach (UILabel item in fadingLabels) { 
			if (item.color.a>alpha){
				item.color = new Color (item.color.r, item.color.g, item.color.b, alpha); 
			}
		} 
	}
}
