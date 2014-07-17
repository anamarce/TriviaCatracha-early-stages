using UnityEngine;

public class AdBuddizScript : MonoBehaviour {
	
	void Start() // Init SDK on app start
	{ 
		Messenger.AddListener ("ShowAd", ShowAd);
		AdBuddizBinding.SetLogLevel(AdBuddizBinding.ABLogLevel.Info);         // log level
		AdBuddizBinding.SetAndroidPublisherKey("926d483c-9992-42a0-b1c2-03b35263ea76"); // replace with your Android app publisher key
		AdBuddizBinding.SetIOSPublisherKey("TEST_PUBLISHER_KEY_IOS");         // replace with your iOS app publisher key
		AdBuddizBinding.SetTestModeActive();                                  // to delete before submitting to store
		AdBuddizBinding.CacheAds();                                           // start caching ads
	}

	public void ShowAd()
	{
			AdBuddizBinding.ShowAd();

	}
	
	void OnEnable()
	{
		// Listen to AdBuddiz events
		AdBuddizManager.didFailToShowAd += DidFailToShowAd;
		AdBuddizManager.didCacheAd += DidCacheAd;
		AdBuddizManager.didShowAd += DidShowAd;
		AdBuddizManager.didClick += DidClick;
		AdBuddizManager.didHideAd += DidHideAd;
	}
	
	void OnDisable()
	{
		// Remove all event handlers
		AdBuddizManager.didFailToShowAd -= DidFailToShowAd;
		AdBuddizManager.didCacheAd -= DidCacheAd;
		AdBuddizManager.didShowAd -= DidShowAd;
		AdBuddizManager.didClick -= DidClick;
		AdBuddizManager.didHideAd -= DidHideAd;
	}
	
	void DidFailToShowAd(string adBuddizError) {
		AdBuddizBinding.LogNative("DidFailToShowAd: " + adBuddizError);
		Debug.Log ("Unity: DidFailToShowAd: " + adBuddizError);
	}
	
	void DidCacheAd() {
		AdBuddizBinding.LogNative("DidCacheAd");
		Debug.Log ("Unity: DidCacheAd");
	}

	void DidShowAd() {
		AdBuddizBinding.LogNative("DidShowAd");
		Debug.Log ("Unity: DidShowAd");
	}
	
	void DidClick() {
		AdBuddizBinding.LogNative("DidClick");
		Debug.Log ("Unity: DidClick");
	}
	
	void DidHideAd() {
		AdBuddizBinding.LogNative("DidHideAd");
		Debug.Log ("Unity: DidHideAd");
	}
}
