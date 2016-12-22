using UnityEngine;
using System.Collections;

public class CameraChangeSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Set camera offset
		float height = Camera.main.orthographicSize * 2.0f;
		float width = height * Screen.width / Screen.height;
		transform.localPosition = new Vector3 (0.218f * width, 0.18f*height, transform.localPosition.z);

		if (Screen.orientation == ScreenOrientation.Landscape) {
			GetComponent<Camera> ().orthographicSize = 4;
		} else {
			GetComponent<Camera> ().orthographicSize = 7;
		}
	}
}
