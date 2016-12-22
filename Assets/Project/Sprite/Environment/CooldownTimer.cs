using UnityEngine;
using System.Collections;

public class CooldownTimer : MonoBehaviour {

	public  float cooldownTime = 0;
	private float cooldownTimeCounter = 0;
	private bool distroyAfterCooldown = false;


	public void SetCooldownTime(float seconds, bool distroyAfterCooldown=false){
		cooldownTime = seconds;
		this.distroyAfterCooldown = distroyAfterCooldown;
	}

	public void Heat(){
		cooldownTimeCounter = cooldownTime;
	}

	public void Cool(){
		cooldownTimeCounter = 0;
	}

	public bool IsCool(){
		if (cooldownTimeCounter > 0) {
			return false;
		} 
		return true;
	}

	public float TimeLeft(){
		return cooldownTimeCounter;
	}
	// Update is called once per frame
	void Update () {
		if (cooldownTimeCounter > 0) {
			cooldownTimeCounter -= Time.deltaTime;
		} else if (distroyAfterCooldown){
			Destroy (gameObject);
		}
	}
}
