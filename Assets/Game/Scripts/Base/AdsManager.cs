using EasyMobile;
using STU;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : Singleton<AdsManager> {
    private bool isShowingInterAndReward = false;
    private bool isShowingBanner = false;
    private bool hasRemoveAds = false;
    private Action<bool> rewardCallBack;
    public bool IsShowingInterAndReward => isShowingInterAndReward;
    public bool IsShowingBanner => isShowingBanner;
    protected override void Awake() {
        base.Awake();
        Advertising.InterstitialAdCompleted += OnInterstitialAdCompleted;

        Advertising.RewardedAdCompleted += Advertising_RewardedAdCompleted;
        Advertising.RewardedAdSkipped += Advertising_RewardedAdSkipped;
    }

    private void OnEnable() {
        EventDispatcher.AddListener<EventKey.IteamChange>(HalderItem);
    }

    private void OnDisable() {
        EventDispatcher.RemoveListener<EventKey.IteamChange>(HalderItem);
    }

    private void HalderItem(EventKey.IteamChange evt) {
        if(evt.itemID == ItemID.REMOVEADS) {
            hasRemoveAds = ItemID.REMOVEADS.GetSaveByID().Amount > 0;
        }
    }

    private void Start() {
        hasRemoveAds = ItemID.REMOVEADS.GetSaveByID().Amount > 0;
    }
    #region Interstitial
    public void ShowInterstitial(GameEvent gameEvent = null) {
        if(hasRemoveAds) {
            return;
        }

        if(!Advertising.IsInterstitialAdReady()) {
            Logs.Log("[GameAds] Interstitial Ad is not ready");
            return;
        }

        if(isShowingInterAndReward) {
            Logs.Log("[GameAds] Interstitial Ad is showing");
            return;
        }
        isShowingInterAndReward = true;
        Advertising.ShowInterstitialAd();
    }

    private void OnInterstitialAdCompleted(InterstitialAdNetwork network, AdPlacement placement) {
        isShowingInterAndReward = false;
        Debug.Log(string.Format(
            "Interstitial ad has been closed. Network: {0}, Placement: {1}",
            network, AdPlacement.GetPrintableName(placement)));
    }
    #endregion
    #region Reward
    public void ShowRewarded(Action<bool> callback, GameEvent evt = null) {
        if(!Advertising.IsRewardedAdReady()) {
            Logs.Log("[GameAds] Reward Ad is not ready");
            return;
        }

        if(isShowingInterAndReward) {
            Logs.Log("[GameAds] Reward Ad is showing");
            return;
        }
        this.rewardCallBack = callback;
        isShowingInterAndReward = true;
        Advertising.ShowInterstitialAd();
    }

    private void Advertising_RewardedAdCompleted(RewardedAdNetwork arg1, AdPlacement arg2) {
        Logs.Log("[GameAds] Reward ad => completed");
        this.rewardCallBack.Invoke(true);
        this.rewardCallBack = null;
        this.isShowingInterAndReward = false;
    }

    private void Advertising_RewardedAdSkipped(RewardedAdNetwork arg1, AdPlacement arg2) {
        Logs.Log("[GameAds] Reward ad => skipped");
        this.rewardCallBack.Invoke(false);
        this.rewardCallBack = null;
        this.isShowingInterAndReward = false;
    }
    #endregion
    #region Banner
    public void ShowBanner(BannerAdPosition position) {
        if(isShowingBanner) {
            Logs.Log("[GameAds] Banner Ad is showing");
            return;
        }
        isShowingBanner = true;
        Advertising.ShowBannerAd((EasyMobile.BannerAdPosition)position, BannerAdSize.SmartBanner);
    }

    public void HideBanner() {
        Logs.Log("[GameAds] Banner ad => hide");
        isShowingBanner = false;
        Advertising.HideBannerAd();
    }

    public enum BannerAdPosition {
        Top,
        Bottom,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Null,
    }
    #endregion
}
