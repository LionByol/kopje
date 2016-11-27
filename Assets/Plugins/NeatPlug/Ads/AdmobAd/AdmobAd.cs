/**
 * AdmobAd.cs
 * 
 * A Singleton class encapsulating public access methods for Admob Ad processes.
 * 
 * Please read the code comments carefully, or visit 
 * http://www.neatplug.com/integration-guide-unity3d-admob-ad-plugin to find information how 
 * to use this program.
 * 
 * End User License Agreement: http://www.neatplug.com/eula
 * 
 * (c) Copyright 2012 NeatPlug.com All rights reserved.
 * 
 * @version 1.4.8
 * @sdk_version(android) 6.4.1
 * @sdk_version(ios) 6.6.1
 *
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AdmobAd  {
	
	#region Fields
		
	public enum BannerAdType
	{
		Universal_SmartBanner = 0,
		Universal_Banner_320x50,
		Tablets_IAB_MRect_300x250,
		Tablets_IAB_Banner_468x60,
		Tablets_IAB_LeaderBoard_728x90,
		Tablets_IAB_SkyScraper_160x600
	};
	
	public enum AdLayout
	{
		Top_Centered = 0,
		Top_Left,
		Top_Right,
		Bottom_Centered,
		Bottom_Left,
		Bottom_Right,
		Middle_Centered,
		Middle_Left,
		Middle_Right
	};
	
	private class AdmobAdNativeHelper : IAdmobAdNativeHelper {
		
#if UNITY_ANDROID	
		private AndroidJavaObject _plugin = null;
#endif		
		public AdmobAdNativeHelper()
		{
			
		}
		
		public void CreateInstance(string className, string instanceMethod)
		{	
#if UNITY_ANDROID			
			AndroidJavaClass jClass = new AndroidJavaClass(className);
			_plugin = jClass.CallStatic<AndroidJavaObject>(instanceMethod);	
#endif			
		}
		
		public void Call(string methodName, params object[] args)
		{
#if UNITY_ANDROID			
			_plugin.Call(methodName, args);	
#endif
		}
		
		public void Call(string methodName, string signature, object arg)
		{
#if UNITY_ANDROID			
			var method = AndroidJNI.GetMethodID(_plugin.GetRawClass(), methodName, signature);			
			AndroidJNI.CallObjectMethod(_plugin.GetRawObject(), method, AndroidJNIHelper.CreateJNIArgArray(new object[] {arg}));
#endif			
		}
	
	};	
	
	private static AdmobAd _instance = null;
	
	#endregion
	
	#region Functions
	
	/**
	 * Constructor.
	 */
	private AdmobAd()
	{	
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().SetNativeHelper(new AdmobAdNativeHelper());
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance();
#endif
	}
	
	/**
	 * Instance method.
	 */
	public static AdmobAd Instance()
	{		
		if (_instance == null) 
		{
			_instance = new AdmobAd();
		}
		
		return _instance;
	}	
	
	/**
	 * Initialization, set the AdmobId (Site publisher ID or mediation ID).
	 * 
	 * This function is for the legacy Admob front-end, which uses only a 
	 * single Ad publisher ID.
	 * 
	 * @param admobId
	 *            string - Your Admob Publisher ID
	 */
	public void Init(string admobId)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().Init(admobId, admobId, false);		
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().Init(admobId, admobId, false);
#endif		
	}
	
	/**
	 * Initialization, set the Ad Unit IDs.
	 * 
	 * This function is for the new Admob frond-end.
	 * NOTE: the Ad Unit ID is in the following form:
	 * ca-app-pub-XXXXXXXXXXXXXXXX/NNNNNNNNNN
	 * 
	 * @param bannerAdUnitId
	 *            string - Your Banner Ad Unit ID.
	 * 
	 * @param interstitialAdUnitId
	 *            string - Your Interstitial Ad Unit ID.
	 * 
	 */
	public void Init(string bannerAdUnitId, string interstitialAdUnitId)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().Init(bannerAdUnitId, interstitialAdUnitId, false);		
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().Init(bannerAdUnitId, interstitialAdUnitId, false);
#endif		
	}	
	
	/**
	 * Initialization, set the Ad Unit IDs.
	 * 
	 * This function is for the new Admob frond-end.
	 * NOTE: the Ad Unit ID is in the following form:
	 * ca-app-pub-XXXXXXXXXXXXXXXX/NNNNNNNNNN
	 * 
	 * @param bannerAdUnitId
	 *            string - Your Banner Ad Unit ID.
	 * 
	 * @param interstitialAdUnitId
	 *            string - Your Interstitial Ad Unit ID.
	 * 
	 * @param testMode
	 *            bool - True for test mode on, false for off.
	 * 
	 */
	public void Init(string bannerAdUnitId, string interstitialAdUnitId, bool testMode)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().Init(bannerAdUnitId, interstitialAdUnitId, testMode);		
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().Init(bannerAdUnitId, interstitialAdUnitId, testMode);
#endif		
	}	
	
	/**
	 * Set test mode.
	 *
	 * @param testMode
	 *             bool - true for test mode On, false for test mode Off.
	 */
	public void SetTestMode(bool testMode)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().SetTestMode(testMode);		
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().SetTestMode(testMode);
#endif			
	}
	
	/**
	 * Load a Banner Ad and show it it immediately once loaded.
	 * 
	 * @param adType
	 *           BannerAdType - type of the Ad.
	 * 
	 * @param layout
	 *           AdLayout - in which layout the Ad should display.
	 *	
	 */
	public void LoadBannerAd(BannerAdType adType, AdLayout layout)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().LoadBannerAd((int)adType, (int)layout, 0, 0, false, null);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().LoadBannerAd((int)adType, (int)layout, 0, 0, false, null);
#endif		
	}

	/**
	 * Load a Banner Ad.
	 * 
	 * @param adType
	 *           BannerAdType - type of the Ad.
	 * 
	 * @param layout
	 *           AdLayout - in which layout the Ad should display.
	 * 
     * @param hide
	 *           bool - whether the ad should keep being invisible after loaded,
	 *                  true for making the ad hidden, false for showing the 
	 *                  ad immediately. if the parameter is set to true, then 
	 *                  you need to programmatically display the ad by calling 
	 *                  ShowBannerAd() after you get notified with event
	 *                  OnReceiveAd from AdmobAdListener.
	 *	
	 */
	public void LoadBannerAd(BannerAdType adType, AdLayout layout, bool hide)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().LoadBannerAd((int)adType, (int)layout, 0, 0, hide, null);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().LoadBannerAd((int)adType, (int)layout, 0, 0, hide, null);
#endif		
	}
	
	/**
	 * Load a Banner Ad.
	 * 
	 * @param adType
	 *           BannerAdType - type of the Ad.
	 * 
	 * @param layout
	 *           AdLayout - in which layout the Ad should display.
	 * 
     * @param hide
	 *           bool - whether the ad should keep being invisible after loaded,
	 *                  true for making the ad hidden, false for showing the 
	 *                  ad immediately. if the parameter is set to true, then 
	 *                  you need to programmatically display the ad by calling 
	 *                  ShowBannerAd() after you get notified with event
	 *                  OnReceiveAd from AdmobAdListener.
	 * 
	 * @param extras
	 *           Dictionary<string, string> - The extra parameters of Ad request.
	 *	
	 */
	public void LoadBannerAd(BannerAdType adType, AdLayout layout, bool hide, 
	                         Dictionary<string, string> extras)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().LoadBannerAd((int)adType, (int)layout, 0, 0, hide, extras);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().LoadBannerAd((int)adType, (int)layout, 0, 0, hide, extras);
#endif		
	}	
	
	/**
	 * Load a Banner Ad.
	 * 
	 * @param adType
	 *           BannerAdType - Type of the Ad.
	 * 
	 * @param layout
	 *           AdLayout - In which layout the Ad should display.
	 * 
	 * @param offsetX
	 *           int - The horizontal offset of the Ad, if the layout is set to left, 
	 *                 the offset is from the left edge of screen to the left edge of 
	 *                 the Ad; if the layout is set to right, the offset is from the 
	 *                 right edge of screen to the right edge of the Ad. 
	 *                 The offset is in pixels.
	 * 
	 * @param offsetY
	 *           int - The vertical offset of the Ad, if the layout is set to top, 
	 *                 the offset is from the top edge of screen to the top edge of 
	 *                 the Ad; if the layout is set to bottom, the offset is from the 
	 *                 bottom edge of screen to the bottom edge of the Ad. 
	 *                 The offset is in pixels.                 
	 * 
     * @param hide
	 *           bool - whether the ad should keep being invisible after loaded,
	 *                  true for making the ad hidden, false for showing the 
	 *                  ad immediately. if the parameter is set to true, then 
	 *                  you need to programmatically display the ad by calling 
	 *                  ShowBannerAd() after you get notified with event
	 *                  OnReceiveAd from AdmobAdListener.
	 * 
	 * @param extras
	 *           Dictionary<string, string> - The extra parameters of Ad request.
	 *	
	 */
	public void LoadBannerAd(BannerAdType adType, AdLayout layout, int offsetX, int offsetY, bool hide, 
	                         Dictionary<string, string> extras)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().LoadBannerAd((int)adType, (int)layout, offsetX, offsetY, hide, extras);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().LoadBannerAd((int)adType, (int)layout, offsetX, offsetY, hide, extras);
#endif		
	}
	
	/**
	 * Load a Banner Ad and place it at specified absolute position, and 
	 * show it immediately once loaded.
	 * 
	 * Please note that the Ad won't display if the top or left applied
	 * makes the Ad display area outside the screen.
	 * 
	 * @param adType
	 * 			BannerAdType - the type of Ad.
	 * 
	 * @param top
	 * 			int - the top margin (in pixels) for placing Ad in absolute position.
	 * 
	 * @param left
	 * 			int - the left margin (in pixels) for placing Ad in absolute position. 
	 *	
	 */
	public void LoadBannerAd(BannerAdType adType, int top, int left)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().LoadBannerAd((int)adType, top, left, false, null);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().LoadBannerAd((int)adType, top, left, false, null);
#endif	
	}	
	
	/**
	 * Load a Banner Ad and place it at specified absolute position.
	 * 
	 * Please note that the Ad won't display if the top or left applied
	 * makes the Ad display area outside the screen.
	 * 
	 * @param adType
	 * 			BannerAdType - the type of Ad.
	 * 
	 * @param top
	 * 			int - the top margin (in pixels) for placing Ad in absolute position.
	 * 
	 * @param left
	 * 			int - the left margin (in pixels) for placing Ad in absolute position. 
	 * 
	 * @param hide
	 *           bool - whether the ad should keep being invisible after loaded,
	 *                  true for making the ad hidden, false for showing the 
	 *                  ad immediately. if the parameter is set to true, then 
	 *                  you need to programmatically display the ad by calling 
	 *                  ShowBannerAd() after you get notified with event
	 *                  OnReceiveAd from AdmobAdListener.
	 */
	public void LoadBannerAd(BannerAdType adType, int top, int left, bool hide)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().LoadBannerAd((int)adType, top, left, hide, null);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().LoadBannerAd((int)adType, top, left, hide, null);
#endif	
	}
	
	/**
	 * Load a Banner Ad and place it at specified absolute position.
	 * 
	 * Please note that the Ad won't display if the top or left applied
	 * makes the Ad display area outside the screen.
	 * 
	 * @param adType
	 * 			BannerAdType - the type of Ad.
	 * 
	 * @param top
	 * 			int - the top margin (in pixels) for placing Ad in absolute position.
	 * 
	 * @param left
	 * 			int - the left margin (in pixels) for placing Ad in absolute position. 
	 * 
	 * @param hide
	 *           bool - whether the ad should keep being invisible after loaded,
	 *                  true for making the ad hidden, false for showing the 
	 *                  ad immediately. if the parameter is set to true, then 
	 *                  you need to programmatically display the ad by calling 
	 *                  ShowBannerAd() after you get notified with event
	 *                  OnReceiveAd from AdmobAdListener.
	 * 
	 * @param extras
	 *           Dictionary<string, string> - The extra parameters of Ad request.
	 */
	public void LoadBannerAd(BannerAdType adType, int top, int left, bool hide, 
	                         Dictionary<string, string> extras)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().LoadBannerAd((int)adType, top, left, hide, extras);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().LoadBannerAd((int)adType, top, left, hide, extras);
#endif	
	}	
	
	/**
	 * Refresh the Banner Ad.
	 * 
	 * This initiates a new ad request to plugin.
	 */
	public void RefreshBannerAd()
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().RefreshBannerAd();
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().RefreshBannerAd();
#endif	
	}
	
	/**
	 * Show the Banner Ad.
	 * 
	 * This sets the banner ad to be visible.
	 */
	public void ShowBannerAd()
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().ShowBannerAd();
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().ShowBannerAd();
#endif
	}
	
	/**
	 * Hide the Banner Ad.
	 * 
	 * This sets the banner ad to be invisible again.
	 */
	public void HideBannerAd()
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().HideBannerAd();
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().HideBannerAd();
#endif
	}	
	
	/**
	 * Reposition the Banner Ad.
	 * 
	 * @param layout
	 *           AdLayout - In which layout the Ad should display.	
	 */
	public void RepositionBannerAd(AdLayout layout)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().RepositionBannerAd((int)layout, 0, 0);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().RepositionBannerAd((int)layout, 0, 0);
#endif	
	}	
	
	/**
	 * Reposition the Banner Ad.
	 * 
	 * @param layout
	 *           AdLayout - In which layout the Ad should display.
	 * 
	 * @param offsetX
	 *           int - The horizontal offset of the Ad, if the layout is set to left, 
	 *                 the offset is from the left edge of screen to the left edge of 
	 *                 the Ad; if the layout is set to right, the offset is from the 
	 *                 right edge of screen to the right edge of the Ad. 
	 *                 The offset is in pixels.
	 * 
	 * @param offsetY
	 *           int - The vertical offset of the Ad, if the layout is set to top, 
	 *                 the offset is from the top edge of screen to the top edge of 
	 *                 the Ad; if the layout is set to bottom, the offset is from the 
	 *                 bottom edge of screen to the bottom edge of the Ad. 
	 *                 The offset is in pixels. 
	 */
	public void RepositionBannerAd(AdLayout layout, int offsetX, int offsetY)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().RepositionBannerAd((int)layout, offsetX, offsetY);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().RepositionBannerAd((int)layout, offsetX, offsetY);
#endif	
	}	
	
	/**
	 * Destroy the Banner Ad.
	 */
	public void DestroyBannerAd()
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().DestroyBannerAd();
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().DestroyBannerAd();
#endif
	}
	
	/**
	 * Load an Interstitial Ad and show it immediately once loaded. 	 
	 */
	public void LoadInterstitialAd()
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().LoadInterstitialAd(false, null);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().LoadInterstitialAd(false, null);
#endif
	}	
	
	/**
	 * Load an Interstitial Ad.	 
	 * 
	 * @param hide
	 *           bool - whether the ad should keep being invisible after loaded,
	 *                  true for making the ad hidden, false for showing the 
	 *                  ad immediately. if the parameter is set to true, then 
	 *                  you need to programmatically display the ad by calling 
	 *                  ShowInterstitialAd() after you get notified with event
	 *                  OnReceiveAdInterstitial from AdmobAdListener.
	 */
	public void LoadInterstitialAd(bool hide)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().LoadInterstitialAd(hide, null);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().LoadInterstitialAd(hide, null);
#endif
	}
	
	/**
	 * Load an Interstitial Ad.	 
	 * 
	 * @param hide
	 *           bool - whether the ad should keep being invisible after loaded,
	 *                  true for making the ad hidden, false for showing the 
	 *                  ad immediately. if the parameter is set to true, then 
	 *                  you need to programmatically display the ad by calling 
	 *                  ShowInterstitialAd() after you get notified with event
	 *                  OnReceiveAdInterstitial from AdmobAdListener.
	 * 
	 * @param extras
	 *           Dictionary<string, string> - The extra parameters of Ad request.
	 */
	public void LoadInterstitialAd(bool hide, Dictionary<string, string> extras)
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().LoadInterstitialAd(hide, extras);
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().LoadInterstitialAd(hide, extras);
#endif
	}	
	
	/**
	 * Show the Interstitial Ad.
	 * 
	 * This sets the Interstitial ad to be visible.
	 */
	public void ShowInterstitialAd()
	{
#if UNITY_ANDROID		
		AdmobAdAndroid.Instance().ShowInterstitialAd();
#endif	
#if UNITY_IPHONE
		AdmobAdIOS.Instance().ShowInterstitialAd();
#endif
	}
	
	#endregion
}
