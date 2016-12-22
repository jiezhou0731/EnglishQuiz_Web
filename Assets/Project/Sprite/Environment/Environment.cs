using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {
	static public Transform cameraTransform;
	// Use this for initialization
	void Start () {
		cameraTransform = GameObject.Find ("Camera").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
