using UnityEngine;
using System.Collections;

public class ShakingLabelDisplay : MonoBehaviour {
	UILabel label;
	ShakeEffect shaker;
	ShowHide showHide;
	// Use this for initialization
	void Awake () {
		label = transform.FindChild("Label").GetComponent<UILabel> ();
		shaker = GetComponent<ShakeEffect>();
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void SetLabel(string text){
		shaker.Trigger ();
		label.text = text;
	}
}
