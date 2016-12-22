using UnityEngine;
using System.Collections;

public class NGUIButtonLabel : MonoBehaviour {
	private UI2DSprite container;
	private UILabel label;
	// Use this for initialization
	void Start () {
		container = transform.parent.GetComponent<UI2DSprite> ();
		label = transform.GetComponent<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
		label.depth = container.depth + 1;
	}
}
