using System;
using System.Collections;
//using GoogleMobileAds.Api;
using UnityEngine;

namespace VavilichevGD.Monetization.AdMob {
//	public class ADSBehaviorAdMob : ADSBehavior {
//		
//		private enum State {
//			None,
//			Busy,
//			WaitForReward
//		}
//		
//		private InterstitialAd interstitial;
//		private RewardBasedVideoAd rewardBasedVideo;
//		private State stateInterstitial;
//		private State stateRewardedVideo;
//		private ADSResultsHandler callbackInterstitial;
//		private ADSResultsHandler callbackRewardedVideo;
//		private ADSAdmobUpdateHelper helper;
//		private ADSSettingsAdMob settings;
//
//		private const string PATH_SETTINGS = "ADSSettingsAdMob";
//		
//		
//		protected override void Initialize() {
//			settings = Resources.Load<ADSSettingsAdMob>(PATH_SETTINGS);
//			
//			MobileAds.Initialize(settings.appId);
//
//			InitRewardedVideo();
//			InitInterstitial();
//			CreateHelper();
//		}
//
//		private void CreateHelper() {
//			if (!helper) {
//				GameObject helperGO = new GameObject("AdMob Helper");
//				helper = helperGO.AddComponent<ADSAdmobUpdateHelper>();
//				helper.OnUpdate += Update;
//				UnityEngine.Object.DontDestroyOnLoad(helperGO);
//			}
//		}
//
//		
//		#region Initialize RewardedVideo
//
//		private void InitRewardedVideo() {
//			rewardBasedVideo = RewardBasedVideoAd.Instance;
//
//			rewardBasedVideo.OnAdClosed += RewardBasedVideo_OnAdClosed;
//			rewardBasedVideo.OnAdLeavingApplication += RewardBasedVideo_OnAdLeavingApplication;
//			rewardBasedVideo.OnAdOpening += RewardBasedVideo_OnAdOpening;
//			rewardBasedVideo.OnAdRewarded += RewardBasedVideo_OnAdRewarded;
//			rewardBasedVideo.OnAdStarted += RewardBasedVideo_OnAdStarted;
//			RequestRewardBasedVideo(settings.rewardedVideoId);
//		}
//
//		private void RequestRewardBasedVideo(string id) {
//			AdRequest request = null;
//			if (settings.testMode)
//				request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator)
//					.AddTestDevice(SystemInfo.deviceUniqueIdentifier.ToUpper()).Build();
//			else
//				request = new AdRequest.Builder().Build();
//			rewardBasedVideo.LoadAd(request, id);
//		}
//
//		#endregion
//
//		
//		
//		#region Initialize Interstitial
//
//		private void InitInterstitial() {
//			interstitial = new InterstitialAd(settings.interstitialId);
//
//			interstitial.OnAdOpening += Interstitial_OnAdOpened;
//			interstitial.OnAdClosed += Interstitial_OnAdOpenedOnAdClosed;
//			interstitial.OnAdLeavingApplication += Interstitial_OnAdLeavingApplication;
//			RequestInterstitial();
//		}
//
//		private void RequestInterstitial() {
//			AdRequest request = null;
//			if (settings.testMode)
//				request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator)
//					.AddTestDevice(SystemInfo.deviceUniqueIdentifier.ToUpper()).Build();
//			else
//				request = new AdRequest.Builder().Build();
//			interstitial.LoadAd(request);
//		}
//
//		#endregion
//
//
//		
//		#region Interstitial WORK
//
//		protected override IEnumerator ShowInterstitialRoutine(ADSResultsHandler callback) {
//			if (callbackInterstitial != null) {
//				callback?.Invoke(FAIL, "Another interstitial is working");
//				yield break;
//			}
//			
//			callbackInterstitial = callback;
//
//#if !UNITY_EDITOR
//			float timer = 0f;
//			while (!interstitial.IsLoaded()) {
//				yield return null;
//				timer += Time.unscaledDeltaTime;
//				if (timer >= settings.breakTime) {
//					NotifyAboutInterstitial(FAIL, "Break time reached. (Interstitial)");
//					yield break;
//				}
//			}
//#endif
//			
//			interstitial.Show();
//		}
//
//		#endregion
//		
//
//		#region Interstitial EVENTS
//
//		private void Interstitial_OnAdLeavingApplication(object sender, EventArgs e) {
//			stateInterstitial = State.None;
//			UnPause();
//			NotifyAboutInterstitial(FAIL, "User leave the game (Interstitial)");
//		}
//
//		public void Interstitial_OnAdOpened(object sender, EventArgs args) {
//			stateInterstitial = State.Busy;
//			Pause();
//		}
//
//		public void Interstitial_OnAdOpenedOnAdClosed(object sender, EventArgs args) {
//			stateInterstitial = State.WaitForReward;
//		}
//		
//		#endregion
//
//		
//		
//		#region RewardedVideo WORK
//		
//		protected override IEnumerator ShowRewardedVideoRoutine(ADSResultsHandler callback) {
//			if (callbackRewardedVideo != null) {
//				callback?.Invoke(FAIL, "Another rewardedVideo is working");
//				yield break;
//			}
//			
//			callbackRewardedVideo = callback;
//
//#if !UNITY_EDITOR
//			float timer = 0f;
//			while (!rewardBasedVideo.IsLoaded()) {
//				yield return null;
//				timer += Time.unscaledDeltaTime;
//				if (timer >= settings.breakTime) {
//					NotifyAboutRewardedVideo(FAIL, "Break time reached. (RewardedVideo)");
//					yield break;
//				}
//			}
//#endif
//			
//			rewardBasedVideo.Show();
//		}
//		
//		#endregion
//
//		
//		#region RewardedVideo EVENTS
//
//		private void RewardBasedVideo_OnAdStarted(object sender, EventArgs e) {
//			stateRewardedVideo = State.Busy;
//			Pause();
//		}
//
//		private void RewardBasedVideo_OnAdRewarded(object sender, GoogleMobileAds.Api.Reward e) {
//			stateRewardedVideo = State.WaitForReward;
//		}
//
//		private void RewardBasedVideo_OnAdOpening(object sender, EventArgs e) {
//			stateRewardedVideo = State.Busy;
//			Pause();
//		}
//
//		private void RewardBasedVideo_OnAdLeavingApplication(object sender, EventArgs e) {
//			stateRewardedVideo = State.None;
//			UnPause();
//			NotifyAboutRewardedVideo(FAIL, "User leave the game (RewardedVideo)");
//		}
//
//		private void RewardBasedVideo_OnAdClosed(object sender, EventArgs e) {
//			UnPause();
//		}
//
//		#endregion
//
//		
//		
//		private void Update() {
//			if (callbackInterstitial != null) {
//				if (stateInterstitial == State.WaitForReward) {
//					stateInterstitial = State.None;
//					UnPause();
//					NotifyAboutInterstitial(SUCCESS);
//				}
//			}
//
//			if (callbackRewardedVideo != null) {
//				if (stateRewardedVideo == State.WaitForReward) {
//					stateRewardedVideo = State.None;
//					UnPause();
//					NotifyAboutRewardedVideo(SUCCESS);
//				}
//			}
//		}
//		
//		
//		private void NotifyAboutInterstitial(bool success, string error = "") {
//			callbackInterstitial?.Invoke(success, error);
//			callbackInterstitial = null;
//		}
//		
//		
//		private void NotifyAboutRewardedVideo(bool success, string error = "") {
//			callbackRewardedVideo?.Invoke(success, error);
//			callbackRewardedVideo = null;
//		}
//		
//		
//		private void UnPause() {
//			// TODO: Need to unpause game in iOS. (AdMob doesnt pause game)
//			#if UNITY_IOS
//			#endif
//		}
//		
//		
//		private void Pause() {
//			// TODO: Need to pause game in iOS. (AdMob doesnt pause game)
//			#if UNITY_IOS
//			#endif
//		}
//    }
}
