using UnityEngine;
using System.Collections;

public class ScreenRotationController : MonoBehaviour {
	public bool fixToPortrait;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (fixToPortrait) {
			Screen.orientation = ScreenOrientation.Portrait;
			Screen.autorotateToLandscapeLeft = false;
			Screen.autorotateToLandscapeRight = false;
			Screen.autorotateToPortrait = true;

		} else {
			Screen.orientation = ScreenOrientation.AutoRotation;
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			Screen.autorotateToPortrait = true;

		}
	}
}
