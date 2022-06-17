using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using STU;
using UnityEngine.SceneManagement;

public class AppOpenAdLauncher : Singleton<AppOpenAdLauncher> {
    private bool showFirstOpen = true;
    protected override void Awake() {
        base.Awake();
        MobileAds.SetiOSAppPauseOnBackground(true);

        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

        // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
        deviceIds.Add("20B85444041647AE99E8CF3029B11050");
#elif UNITY_ANDROID
        deviceIds.Add("1FF875FC5C6868256B2E3BEEE212678D");
#endif

        // Configure TagForChildDirectedTreatment and test device IDs.
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
            .SetTestDeviceIds(deviceIds).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus) {
        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() => {
            AppOpenAdManager.Instance.LoadAd();
        });
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
        if(arg0.name == SceneManagerLoad.SCENE_HOME) {
            if(showFirstOpen && AppOpenAdManager.ConfigOpenApp) {
                AppOpenAdManager.Instance.ShowAdIfAvailable();
                showFirstOpen = false;
            }
        }
    }

    private void OnApplicationPause(bool pause) {
        if(!pause && AppOpenAdManager.ConfigResumeApp && !AppOpenAdManager.ResumeFromAdsIAP) {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }
}