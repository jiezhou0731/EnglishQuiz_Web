using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Tip : MonoBehaviour
{
	public string zoneId;


	public void ButtonClicked(){
		if (string.IsNullOrEmpty (zoneId)) zoneId = null;

		if (Advertisement.IsReady (zoneId)) {
			UnityAdsInit.SetAdsWatched ();// reset the timer
			ShowOptions options = new ShowOptions ();
			options.resultCallback = HandleShowResult;
			Advertisement.Show (zoneId, options);
			DisableAllButtons.DisableAll ();
		} else {
			MessageBubbleManager.AddTextMessage("Tip only shows in mobile version. \nPlease download the app using your phone.",0.5f,3f);
		}
	}

	private void HandleShowResult (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			TextQuestion.ShowTip ();
			MessageBubbleManager.AddTextMessage("Ads completed, showing tips.",0.5f,3f);
			Debug.Log ("Video completed. ");
			break;
		case ShowResult.Skipped:
			MessageBubbleManager.AddTextMessage("Ads skipped, no tips.",0.5f,3f);
			Debug.LogWarning ("Video was skipped.");
			break;
		case ShowResult.Failed:
			Debug.LogError ("Video failed to show.");
			break;
		}
		DisableAllButtons.EnableAll ();
	}
}