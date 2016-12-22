using UnityEngine;
using System.Collections;

public class DisableAllButtons : MonoBehaviour {

	public static DisableAllButtons instance;
	public GameObject cover;
	// Use this for initialization
	void Start () {
		instance = this;
		EnableAll ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void DisableAll(){
		instance.cover.SetActive (true);
	}

	public static void EnableAll(){
		instance.cover.SetActive (false);
	}
}
