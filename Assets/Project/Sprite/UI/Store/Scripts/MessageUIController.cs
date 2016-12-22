using UnityEngine;
using System.Collections;

public class MessageUIController : MonoBehaviour {
	static MessageUIController instance;
	private GameObject notEnoughClovers;
	// Use this for initialization
	void Start () {
		instance = this;
		notEnoughClovers = transform.FindChild ("NotEnoughClovers").gameObject;
		notEnoughClovers.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

	}

	static public void ShowNotEnoughClovers(){
		instance.notEnoughClovers.SetActive(true);
	}

	public void HideNotEnoughClovers(){
		notEnoughClovers.SetActive(false);
	}
}
