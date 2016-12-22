using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageBubbleManager : MonoBehaviour {
	public GameObject textMessageModel;
	public GameObject imageMessageModel;
	public static MessageBubbleManager instance;
	public Vector3 startPosition;
	public float stayDuration;
	public float positionTransitDuration;
	public List<MessageBubble> messages = new List<MessageBubble> ();


	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		int currentY = 0;
		for (var i = messages.Count-1; i >=0 ; i--) {
			if (messages[i].TargetLocalPosition != new Vector3 (0, currentY, 0)) {
				messages[i].SetTargetLocalPosition (new Vector3 (0, currentY, 0),positionTransitDuration);
			}
			currentY -= 130;
		}
	}

	public void RemoveMessage(MessageBubble message){
		messages.Remove (message);
	}

	public static void AddTextMessage(string text, float scaleTransitDuration=0.5f, float duration=5f){
		/*
		if (instance.IsDuplication (text)) {
			return;
		}*/
		GameObject newItem = (GameObject)GameObject.Instantiate (instance.textMessageModel, instance.transform);
		newItem.GetComponent<MessageBubble> ().Initialize(text,instance.startPosition, instance.stayDuration);
		newItem.GetComponent<MessageBubble> ().SetTargetLocalScale (Vector3.one, scaleTransitDuration);
		instance.messages.Add (newItem.GetComponent<MessageBubble> ());
	}

	public static void AddImageTextMessage(UnityEngine.Sprite sprite, string text, float scaleTransitDuration=0.5f,float duration=5f){
		if (instance.IsDuplication (text)) {
			return;
		}
		GameObject newItem = (GameObject)GameObject.Instantiate (instance.imageMessageModel, instance.transform);
		newItem.GetComponent<MessageBubble> ().Initialize(text, instance.startPosition,instance.stayDuration);
		newItem.transform.FindChild("Image").GetComponent<UI2DSprite> ().sprite2D =sprite;
		newItem.GetComponent<MessageBubble> ().SetTargetLocalScale (Vector3.one, scaleTransitDuration);
		instance.messages.Add (newItem.GetComponent<MessageBubble> ());
	}
	/*
	public void TestAddTextMessage(){
		
	}
*/
	private bool IsDuplication(string text){
		for (var i = messages.Count-1; i >=0 ; i--) {
			if (messages[i].text== text) {
				return true;
			}
		}
		return false;
	}
	public void TestAddTextMessage(){
		int x = Random.Range (0, 3);
		if (x == 0) {
			AddTextMessage ("Skill is not ready.");
		} else if (x == 1) {
			AddImageTextMessage (testSprite, "Dodge it!");
		} else if (x == 2) {
			AddTextMessage ("Do you want it?");
		} 

	}

	public UnityEngine.Sprite testSprite;
}
