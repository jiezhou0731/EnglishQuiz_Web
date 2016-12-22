using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHeightAdapter : MonoBehaviour {

	public bool topFixed = true;
	private float heightInPercentage=0.4f;
	void Start () {
	}	
	
	// Update is called once per frame
	void Update () {
		try{
			transform.FindChild("Label").GetComponent<UILabel>().fontSize = Mathf.Min(90, Mathf.RoundToInt(Screen.height * heightInPercentage*0.8f));
		} catch{
		}
		if (topFixed) {
			GetComponent<UIWidget> ().bottomAnchor.Set (1f, -Screen.height * heightInPercentage);
		} else {
			GetComponent<UIWidget> ().topAnchor.Set (0f, Screen.height * heightInPercentage);
		}
	}
}
