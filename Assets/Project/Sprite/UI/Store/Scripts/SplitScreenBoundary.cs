using UnityEngine;
using System.Collections;

public class SplitScreenBoundary : MonoBehaviour {

	public GameObject verticle;
	public GameObject horizonal;
	// Update is called once per frame
	void Update () {
		if (Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.LandscapeRight || Screen.orientation == ScreenOrientation.LandscapeLeft) {
			verticle.SetActive (true);
			horizonal.SetActive (false);
		} else if (Screen.orientation == ScreenOrientation.Portrait) {
			verticle.SetActive (false);
			horizonal.SetActive (true);
		}
	}
}
