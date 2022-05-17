using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyView : MonoBehaviour {

    [SerializeField] private Button btn_Buy;
    [SerializeField] private int Index;
    [SerializeField] private ItemStackView itemStackV;
    [Space]
    [SerializeField] private GameObject disPlay;
    [SerializeField] private GameObject tickV;
    [SerializeField] private DisplayObjects btn_Stt; //0.Active 1.NoActive
    private DailyFrame dailyFrame;
    private Status status;
    private ItemStack reward;
    private void Awake() {
        btn_Buy.onClick.AddListener(OnClaim);
    }

    public void Init(DailyFrame dailyFrame, int index) {
        this.dailyFrame = dailyFrame;
        this.Index = index;
    }

    public void Show(ItemStack itemStack) {
        this.reward = itemStack;
        itemStackV.Show(itemStack);
        Show();
    }

    #region Status
    private void SetUpNoActive() {
        disPlay.SetActive(false);
        tickV.SetActive(false);
        btn_Buy.gameObject.SetActive(true);
        btn_Stt.Active(1);
    }

    private void SetUpActive() {
        disPlay.SetActive(false);
        tickV.SetActive(false);
        btn_Buy.gameObject.SetActive(true);
        btn_Stt.Active(0);
    }

    private void SetUpReceived() {
        disPlay.SetActive(true);
        tickV.SetActive(true);
        btn_Buy.gameObject.SetActive(false);
        btn_Stt.Active(0);
    }

    private void SetUpMiss() {
        disPlay.SetActive(true);
        tickV.SetActive(false);
        btn_Buy.gameObject.SetActive(false);
        btn_Stt.Active(0);
    }
    #endregion

    private void OnClaim() {
        if(status == Status.ACTIVE) {
            CollectionController.Instance.GetItemStack(reward, Camera.main.WorldToScreenPoint(btn_Buy.transform.position), () => {
                DataManager.Instance.PlayerData.AddItem(reward);
                DataManager.Instance.PlayerData.DailySave.AddIndexDay(Index);
                this.status = Status.RECEIVED;
                Show();
                //EventDispatcher.Dispatch<EventKey.DailyChange>(new EventKey.DailyChange());
            });
        }
    }

    private void Show() {
        switch(status) {
            case Status.NOACTIVE:
                SetUpNoActive();
                break;
            case Status.ACTIVE:
                SetUpActive();
                break;
            case Status.RECEIVED:
                SetUpReceived();
                break;
        }
    }

    public enum Status {
        NONE,
        NOACTIVE,
        ACTIVE,
        RECEIVED,
    }
}
