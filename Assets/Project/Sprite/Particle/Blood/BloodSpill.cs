using UnityEngine;
using System.Collections;

public class BloodSpill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingLayerName = "Weapon";
		GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingOrder = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.identity;
	}
}
