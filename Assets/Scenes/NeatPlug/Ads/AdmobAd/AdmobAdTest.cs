/**
 * AdmobAdTest.cs
 * 
 * A Test script for demonstrating the usage of AdmobAd Plugin.
 * 
 * Please read the code comments carefully, or visit 
 * http://www.neatplug.com/integration-guide-unity3d-admob-ad-plugin to find information 
 * about how to integrate and use this program.
 * 
 * End User License Agreement: http://www.neatplug.com/eula
 * 
 * (c) Copyright 2013 NeatPlug.com All Rights Reserved.
 * 
 * @version 1.4.3
 * @sdk_version(android) 6.3.1
 * @sdk_version(ios) 6.4.0
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdmobAdTest : MonoBehaviour {	
	
	private float _buttonWidth =  (Mathf.Min(Screen.height, Screen.width) - 40f) / 3f;	
	private float _buttonHeight = 60f;	
	
	void OnGUI() {	
		
		if (UnityEngine.GUI.Button (new Rect(10f, 100f, _buttonWidth, _buttonHeight), "Hide Banner")) {		
			AdmobAd.Instance().HideBannerAd();
		}
		
		if (UnityEngine.GUI.Button (new Rect(20f + _buttonWidth, 100f, _buttonWidth, _buttonHeight), "Show Banner")) {		
			AdmobAd.Instance().ShowBannerAd();
		}		
		
		if (UnityEngine.GUI.Button (new Rect(30f + 2f * _buttonWidth, 100f, _buttonWidth, _buttonHeight), "Destroy Banner")) {		
			AdmobAd.Instance().DestroyBannerAd();
		}
		
		if (UnityEngine.GUI.Button (new Rect(10f, 200f, _buttonWidth, _buttonHeight), "Load Banner (Bottom)")) {			 
			AdmobAd.Instance().LoadBannerAd(AdmobAd.BannerAdType.Universal_SmartBanner, AdmobAd.AdLayout.Bottom_Centered);	
		}		
		
		if (UnityEngine.GUI.Button (new Rect(20f + _buttonWidth, 200f, _buttonWidth, _buttonHeight), "Load Banner (Top)")) {			
			AdmobAd.Instance().LoadBannerAd(AdmobAd.BannerAdType.Universal_SmartBanner, AdmobAd.AdLayout.Top_Centered);
		}		
		
		if (UnityEngine.GUI.Button (new Rect(30f + 2f * _buttonWidth, 200f, _buttonWidth, _buttonHeight), "Load Banner (MRect Center)")) {		
			AdmobAd.Instance().LoadBannerAd(AdmobAd.BannerAdType.Tablets_IAB_MRect_300x250, AdmobAd.AdLayout.Middle_Centered);
		}		
		
		
		if (UnityEngine.GUI.Button (new Rect(10f, 300f, _buttonWidth, _buttonHeight), "Load Banner (Bottom)\nWith Offset (0, 100)")) {			 
			AdmobAd.Instance().LoadBannerAd(AdmobAd.BannerAdType.Universal_SmartBanner, AdmobAd.AdLayout.Bottom_Centered, 0, 100, false, null);	
		}		
		
		if (UnityEngine.GUI.Button (new Rect(20f + _buttonWidth, 300f, _buttonWidth, _buttonHeight), "Load Banner (Top)\nWith Offset (0, 100)")) {			
			AdmobAd.Instance().LoadBannerAd(AdmobAd.BannerAdType.Universal_SmartBanner, AdmobAd.AdLayout.Top_Centered, 0, 100, false, null);
		}		
			
		if (UnityEngine.GUI.Button (new Rect(30f + 2f * _buttonWidth, 300f, _buttonWidth, _buttonHeight), "Move to Top")) {		
			AdmobAd.Instance().RepositionBannerAd(AdmobAd.AdLayout.Top_Centered);
		}	
		
		if (UnityEngine.GUI.Button (new Rect(10f, 400f, _buttonWidth, _buttonHeight), "Load & Show Interstitial")) {			
			AdmobAd.Instance().LoadInterstitialAd(false);		
		}		
		
		if (UnityEngine.GUI.Button (new Rect(20f + _buttonWidth, 400f, _buttonWidth, _buttonHeight), "Load & Hide Interstitial")) {			
			AdmobAd.Instance().LoadInterstitialAd(true);
		}		
		
		if (UnityEngine.GUI.Button (new Rect(30f + 2f * _buttonWidth, 400f, _buttonWidth, _buttonHeight), "Show Interstitial")) {		
			AdmobAd.Instance().ShowInterstitialAd();
		}	
		
	}
	
	
	// Quit test app when back button is tapped.
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{					
			Application.Quit();
		}	
	}
}
