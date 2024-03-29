﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class EventWithObject : UnityEvent<object>{}

public class EventManager : MonoBehaviour {

	private Dictionary <string, UnityEvent<object>> eventDictionary;

	private static EventManager eventManager;

	private static EventManager instance {
		get {
			if (!eventManager) {
				eventManager = FindObjectOfType (typeof(EventManager)) as EventManager;

				if (!eventManager) {
					Debug.LogError ("EventManager not found");
				} else {
					eventManager.Init ();
				}
			}
			return eventManager;
		}
	}

	void Init(){
		if (eventDictionary == null) {
			eventDictionary = new Dictionary<string, UnityEvent<object>> ();
		}
	}

	public static void StartListening(string eventName, UnityAction<object> listener){
		UnityEvent<object> thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.AddListener (listener);
		} else {
			thisEvent =new EventWithObject ();	
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction<object> listener){
		if (eventManager == null)
			return;
		UnityEvent<object> thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.RemoveListener (listener);
		}
	}

	public static void TriggerEvent(string eventName, object data=null){
		UnityEvent<object> thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.Invoke (data);
		}
	}

}
