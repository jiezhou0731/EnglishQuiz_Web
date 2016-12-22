using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UITransitionController : MonoBehaviour {
	private List<UITransition> transitions = new List<UITransition> ();

	protected class UITransition{
		// called only in first update
		public virtual void FirstUpdate(){

		}

		// called when isActive == true;
		public virtual void Update(){

		}

		// called at the last frame of active;
		public virtual void End(){
		}
			
		public float duration = 2f;
		public float finishedPercentage = 0;
		public string name="";
		private bool _isActive = false;
		public  bool isActive {
			get {
				return _isActive;
			}    
		}

		private float startTime = 0f;
		public void UpdateProcess(){
			finishedPercentage = (Time.fixedTime - startTime) / duration;
			if (duration>0 && finishedPercentage > 1f) {
				_isActive = false;
				End ();
			} else {
				if (_isActiveInLastUpdate == false) {
					FirstUpdate ();
				}
			 	Update ();
			}
			_isActiveInLastUpdate = _isActive;
		}

		private bool _isActiveInLastUpdate = false;
		public void SetActive(bool active){
			if (active == true) {
				startTime = Time.fixedTime;
				_isActive = true;
			} else {
				if (_isActive) {
					End ();
				}
				_isActive = false;
				_isActiveInLastUpdate = false;
			}
		}
	}

	protected void AddTransition(UITransition transition){
		transitions.Add (transition);
	}
	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		foreach (UITransition transition in transitions) {
			if (transition.isActive == true) {
				transition.UpdateProcess ();
			}
		}
	}

	public void GoTo(string transitionName){
		foreach (UITransition transition in transitions) {
			if (transition.name != transitionName) {
				transition.SetActive (false);
			} else {
				transition.SetActive (true);
			}
		}
	}
}

