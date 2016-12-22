using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsInit : MonoBehaviour {

	// After which the users are forced to watch an ad.
	static float adsInterval = 70;



	static public UnityAdsInit instance;

	[SerializeField]
	private string
	androidGameId = "1157830",
	iosGameId = "1157799";

	[SerializeField]
	private bool testMode;

	void Start ()
	{
		instance = this;
		string gameId = null;
		lastPlayTime = Time.fixedTime;
		#if UNITY_ANDROID
		gameId = androidGameId;
		#elif UNITY_IOS
		gameId = iosGameId;
		#endif

		if (Advertisement.isSupported && !Advertisement.isInitialized) {
			Advertisement.Initialize(gameId, testMode);
		}
	}

	static float lastPlayTime=0;
	public static bool IsMandatoryAdsReady(){
		if (lastPlayTime + adsInterval < Time.fixedTime) {
			return true;
		} else {
			return false;
		}
	}

	static public void SetAdsWatched(){
		lastPlayTime = Time.fixedTime;
	}

	public string zoneId;

	public static void ForceToPlayAds(){
		UnityAdsInit.instance.PlayAds ();
	}

	public void PlayAds(){
		if (string.IsNullOrEmpty (zoneId)) zoneId = null;
		if (Advertisement.IsReady (zoneId)) {
			SetAdsWatched ();
			ShowOptions options = new ShowOptions();
			options.resultCallback = HandleShowResult;
			Advertisement.Show (zoneId, options);
			DisableAllButtons.DisableAll ();
		}
	}

	private void HandleShowResult (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			break;
		case ShowResult.Skipped:
			break;
		case ShowResult.Failed:
			break;
		}
		DisableAllButtons.EnableAll ();
	}
}
