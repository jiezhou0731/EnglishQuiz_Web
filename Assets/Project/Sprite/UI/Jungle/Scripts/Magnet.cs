using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Magnet : MonoBehaviour {
	public string anchor ="TopLeftOfScreen";
	private Camera gameCamera;
	private Camera uiCamera;
	private List<AttractingItem> attractingItems = new List<AttractingItem>();
	// Use this for initialization
	void Awake () {
		//gameCamera = GameObject.Find("CameraContainer/Main Camera").GetComponent<Camera>();
		uiCamera = GameObject.Find("UI/Camera").GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		for(int i=attractingItems.Count - 1; i > -1; i--) {
			AttractingItem aItem = attractingItems [i];

			if (aItem.GetCurrentT () > 1) {
				attractingItems.RemoveAt (i);
				Destroy (aItem.transform.gameObject);
				continue;
			}
			float x = Mathf.SmoothStep (
				aItem.startPosition.x,
				aItem.endPosition.x,
				aItem.GetCurrentT ());
			float y = aItem.GetY (x);
			aItem.transform.position = new Vector3 (x, y, transform.position.z);
		}
	

	}
		
	public void Attract(Transform item,  float sphereLocalScale=1f, float duration = 0.5f){
		Vector3 viewpos = gameCamera.WorldToViewportPoint(item.position);
		Vector3 screenpos = uiCamera.ViewportToWorldPoint(viewpos);
		item.SetParent (transform.root);
		item.localScale = sphereLocalScale*Vector3.one;
		//item.SetLayer (transform.gameObject.layer);
		item.position = new Vector3 (screenpos.x , screenpos.y , 0);

		// calculate offset
		float height =  uiCamera.orthographicSize * 2.0f;
		float width = height * Screen.width / Screen.height;
		Vector3 offset = new Vector3 (-0.1f, -0.1f, 0);

		AttractingItem aItem = new AttractingItem (item, transform.position, offset, duration);
		attractingItems.Add (aItem);
	}
}

class AttractingItem{
	public Transform transform;
	public float startTime=0f;
	public float duration = 0.5f;
	public Vector3 startPosition;
	public Vector3 endPosition;
	private Vector3 lowerPoint;
	private double A;
	private double B;
	private double C;
	public AttractingItem(Transform transform, Vector3 endPosition, Vector3 offset, float duration){
		this.transform = transform;
		startTime = Time.fixedTime;
		startPosition = transform.position;
		this.endPosition = endPosition;
		this.duration = duration;

		this.lowerPoint = startPosition + offset;

		// calculate parabola
		double x1 = startPosition.x;
		double y1 = startPosition.y;
		double x2 = endPosition.x;
		double y2 = endPosition.y;
		double x3 = lowerPoint.x;
		double y3 = lowerPoint.y;
		double denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
		if (denom == 0) {
			denom = 0.0001f;
		}
		A = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
		B = (x3 *x3 * (y1 - y2) + x2 *x2 * (y3 - y1) + x1 *x1 * (y2 - y3)) / denom;
		C = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;
	}

	public float GetY(float x){
		return (float)(A*x*x+B*x+C);
	}

	public float GetCurrentT(){
		return (Time.fixedTime - startTime) / duration;
	}

}