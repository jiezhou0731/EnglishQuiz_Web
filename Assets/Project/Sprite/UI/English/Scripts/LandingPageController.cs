using UnityEngine;
using System.Collections;

public class LandingPageController : MonoBehaviour {

	public FlyInEffect startButton;

	public void TriggerEffect(){
		startButton.Trigger ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartButtonClicked(){
		UIStateController.ShowState ("Category");
	}
}
