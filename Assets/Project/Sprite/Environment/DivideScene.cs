using UnityEngine;
using System.Collections;

public class DivideScene : MonoBehaviour {
	private UI2DSprite top;
	private UI2DSprite bottom;
	public Transform topRoot;
	public Transform bottomRoot;
	// Use this for initialization
	void Start () {
		top = topRoot.FindChild ("Background").GetComponent<UI2DSprite>();
		bottom = bottomRoot.FindChild ("Background").GetComponent<UI2DSprite>();
	}

	public float topPercentage;
	// Update is called once per frame
	void Update () {
		//Debug.Log (Screen.orientation);
		if (Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.LandscapeRight || Screen.orientation == ScreenOrientation.LandscapeLeft) {
			top.bottomAnchor.Set (topRoot,0,0);
			top.rightAnchor.Set (topRoot,0,Screen.width * topPercentage);
			bottom.topAnchor.Set (bottomRoot,1,0);
			bottom.bottomAnchor.Set (bottomRoot,0,0);
			bottom.leftAnchor.Set (bottomRoot,0,Screen.width * topPercentage);
		} else if (Screen.orientation == ScreenOrientation.Portrait) {
			top.rightAnchor.Set (topRoot,1, 0);
			top.bottomAnchor.Set (topRoot,1,-Screen.height * topPercentage);
			bottom.topAnchor.Set (bottomRoot,1,-Screen.height * topPercentage);
			bottom.bottomAnchor.Set (bottomRoot,0,0);
			bottom.leftAnchor.Set (bottomRoot,0,0);
		}

	}
}
