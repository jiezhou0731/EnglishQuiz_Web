using UnityEngine;
using System.Collections;using System.Collections.Generic;
using UnityEngine.Advertisements;
public class UIStateController : MonoBehaviour {
	private Dictionary<string,ShowHide> states = new Dictionary<string,ShowHide>();
	private static UIStateController instance;
	void Awake(){
		instance = this;
		foreach (Transform child in transform) {
			states [child.name] = child.GetComponent<ShowHide> ();
		}
	}

	void Start(){
		ShowState ("LandingPage");
		//ShowState ("Category");
	}


	static public void ShowState(string stateName){
		SoundManager.Play ("ButtonClick");

		if (stateName == "QuestionPanel") {
			instance.transform.FindChild ("QuestionPanel").GetComponent<QuestionPanel> ().TriggerEffect ();
		} else if (stateName == "Category") {
			instance.transform.FindChild ("Category").GetComponent<CategoryController> ().TriggerEffect ();
		} else if (stateName == "LandingPage") {
			instance.transform.FindChild ("LandingPage").GetComponent<LandingPageController> ().TriggerEffect ();
		} 

		foreach(KeyValuePair<string, ShowHide> state in UIStateController.instance.states)
		{
			if (state.Key == stateName) {
				state.Value.Show ();
			} else {
				state.Value.Hide ();
			}
		} 
	}

}
