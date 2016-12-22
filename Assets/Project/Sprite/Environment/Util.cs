using UnityEngine;
using System.Collections;

public class Util :MonoBehaviour{

	public static GameObject FindParentWithTag(GameObject childObject, string tag)
	{
		Transform t = childObject.transform;
		while (t.parent != null)
		{
			if (t.parent.tag == tag)
			{
				return t.parent.gameObject;
			}
			t = t.parent.transform;
		}
		return null; // Could not find a parent with given tag.
	}

	public static int NumberRotate(int current, int offset, int totalNumber){
		current = current + offset;
		if (current == -1) {
			current = totalNumber - 1;
		}
		current = current % totalNumber;
		return current;
	}


	static public CooldownTimer GetCooldownTimer(Transform owner, float seconds, bool destroyAfterCooldown=false){
		GameObject go = (GameObject) GameObject.Instantiate (GameObject.Find ("Environment/CooldownTimerModel"));
		go.transform.SetParent (owner);
		CooldownTimer timer =go.GetComponent<CooldownTimer> ();
		timer.SetCooldownTime (seconds,destroyAfterCooldown);
		timer.Heat ();
		return timer;
	}


	static public float EaseOut(float currentTime, float startValue, float changeInValue, float duration) {
		currentTime /= duration;
		return -changeInValue * currentTime*(currentTime-2) + startValue;
	}

	static public string PercentageToGrade(float percent){
		if (percent < 0) {
			return null;
		}
		string rate = "";
		if (percent >= 0.95f) {
			rate = "A+";
		} else if (percent >= 0.90f) {
			rate = "A";
		} else if (percent >= 0.85f) {
			rate = "A-";
		} else if (percent >= 0.80f) {
			rate = "B+";
		} else if (percent >= 0.75f) {
			rate = "B";
		} else if (percent >= 0.70f) {
			rate = "B-";
		} else if (percent >= 0.65f) {
			rate = "C+";
		} else if (percent >= 0.60f) {
			rate = "C";
		} else if (percent >= 0.55f) {
			rate = "C-";
		} else if (percent >= 0.45f) {
			rate = "D+";
		} else if (percent >= 0.35f) {
			rate = "D";
		} else if (percent >= 0.10f) {
			rate = "D-";
		} else {
			rate = "F";
		}

		return rate;
	}
}
	

public static class TransformExtensions
{
	public static void SetLayer(this Transform trans, int layer) 
	{
		trans.gameObject.layer = layer;
		foreach(Transform child in trans)
			child.SetLayer( layer);
	}
}