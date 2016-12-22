using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitScreenMenu : MonoBehaviour {

	private List<Transform> buttons;
	public Transform content;

	public GameObject currentPressedButton;
	private List<Transform> options;

	public void ButtonClick(GameObject clicked){
		SoundManager.Play ("ButtonClick");
		currentPressedButton.GetComponent<MenuButton> ().ButtonReleased ();
		currentPressedButton = clicked;
		currentPressedButton.GetComponent<MenuButton> ().ButtonClicked ();
		foreach (Transform child  in options){
			if (child.name == clicked.name) {
				child.gameObject.SetActive (true);
			} else {
				child.gameObject.SetActive (false);
			}
		}
	}

	// Use this for initialization
	void Start () {
		BackgroundMusicManager.Play ("Menu");
		buttons = new List<Transform> ();
		foreach (Transform child  in transform){
			buttons.Add (child);
			child.GetComponent<MenuButton> ().ButtonReleased ();
		}
		currentPressedButton.GetComponent<MenuButton> ().ButtonClicked ();
		var first = true;
		options = new List<Transform> ();
		foreach (Transform child  in content){
			options.Add (child);
			if (first) {
				child.gameObject.SetActive (true);
				first = false;
			} else {
				child.gameObject.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
