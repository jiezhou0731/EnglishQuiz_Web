using UnityEngine;
using System.Collections;

public class ParticleSystemAutoDestroy : MonoBehaviour {

	void Start()
	{
		if (transform.name != "BloodSpillModel") {
			Destroy (gameObject, GetComponent<ParticleSystem> ().duration+3); 
		}
	}
}
