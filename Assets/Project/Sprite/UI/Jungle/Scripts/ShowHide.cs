using UnityEngine;
using System.Collections;

public class ShowHide : MonoBehaviour {
	void Start(){
	}
	public void Show(){
		transform.localPosition = new Vector3 (
			transform.localPosition.x,
			transform.localPosition.y,
			0);
	}


	public void Hide(){
		transform.localPosition = new Vector3 (
			transform.localPosition.x,
			transform.localPosition.y,
			100000);
	}

	private void ShowAll(GameObject current){
		try{
			current.GetComponent<Renderer>().enabled = true;
		} catch(System.Exception e){
		}
		foreach (Transform child in current.transform) {
			ShowAll (child.gameObject);
		}
	}

	private void HideAll(GameObject current){
		try{
			
		} catch(System.Exception e){
		}
		foreach (Transform child in current.transform) {
			HideAll (child.gameObject);
		}
	}

}
