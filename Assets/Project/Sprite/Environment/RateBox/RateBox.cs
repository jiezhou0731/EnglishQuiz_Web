 using UnityEngine;
using System.Collections;

public class RateBox : MonoBehaviour {
	string appleId = "1167591210";
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void RateButtonClicked(){
		GameState.PopRateBox ();
		GameState.Rate ();

		#if UNITY_ANDROID
		Application.OpenURL("market://details?id="+"com.jiezhoudev.poisonedjungleandroid");
		#elif UNITY_IPHONE
		Application.OpenURL("itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id="+appleId+"&onlyLatestVersion=true&pageNumber=0&sortOrdering=1&type=Purple+Software");
		#endif
	}


	public void CancelButtonClicked(){
		GameState.PopRateBox ();
		UIStateController.ShowState ("Category");
	}
}
