using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyFrame : FrameBase {
    [SerializeField] private List<DailyView> lstDailyView;
    [SerializeField] private List<ItemStack> lstReward;
    //[SerializeField] private ItemStack rewardAds;
    private DailySave DailySave => DataManager.Instance.PlayerData.DailySave;

    private void Awake() {
        int i = 0;
        foreach(DailyView view in lstDailyView) {
            view.Init(this,i);
            i++;
        }
    }

    public override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        for(int i = 0; i < lstDailyView.Count; i++) {
            lstDailyView[i].Show(lstReward[i]);
        }
    }

    //public Status GetStatus(int index) {
    //    int cur_Index = DailySave.GetCurIndex();
    //    if(index == cur_Index) {
    //        if(DailySave.CheckReceived(index)) {
    //            return Status.RECEIVED;
    //        } else {
    //            return Status.ACTIVE;
    //        }
    //    } else if(index > cur_Index) {
    //        return Status.NOACTIVE;
    //    } else {
    //        if(DailySave.CheckReceived(index)) {
    //            return Status.RECEIVED;
    //        } else {
    //            return Status.MISS;
    //        }
    //    }
    //}


}
