using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using STU;

public class HomeFrame : FrameBase {
    [SerializeField] private Button btn_Play;
    [SerializeField] private Button btn_Spin,btn_Skin,btn_Daily, btn_Levels, btn_Shop,btn_LinkGame,btn_RemoveAds;
    [SerializeField] private TextMeshProUGUI txt_Level;
    [SerializeField] private AudioClip musicMenu;
    private void Awake() {
        btn_Play.onClick.AddListener(StartGame);
        btn_Spin.onClick.AddListener(() => { FrameManager.Instance.Push<SpinFrame>(); });
        btn_Skin.onClick.AddListener(() => { FrameManager.Instance.Push<SkinFrame>(); });
        btn_Daily.onClick.AddListener(() => { FrameManager.Instance.Push<DailyFrame>(); });
        btn_Levels.onClick.AddListener(() => { FrameManager.Instance.Push<LevelSelectFrame>(); });
        btn_Shop.onClick.AddListener(() => { FrameManager.Instance.Push<ShopFrame>(); });
        btn_LinkGame.onClick.AddListener(()=> { Application.OpenURL("https://play.google.com/store/apps/details?id=com.gafu.stickman.red.blue"); });
        btn_RemoveAds.onClick.AddListener(BuyRemoveAds);
    }

    private void OnEnable() {
        EventDispatcher.AddListener<EventKey.IteamChange>(HalderEventItemChange);
    }

    private void OnDisable() {
        EventDispatcher.RemoveListener<EventKey.IteamChange>(HalderEventItemChange);
    }

    private void HalderEventItemChange(EventKey.IteamChange evt) {
        if(evt.itemID == ItemID.REMOVEADS && evt.curAmount > 0) {
            btn_RemoveAds.gameObject.SetActive(false);
        }
    }

    public override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        txt_Level.text = $"LEVEL {DataManager.Instance.PlayerData.LevelMap + 1}";
        SoundManager.Instance.PlayMusic(musicMenu);
        btn_RemoveAds.gameObject.SetActive(ItemID.REMOVEADS.GetSaveByID().Amount < 1);
    }

    private void StartGame() {
        GameManager.Instance.CurLevel = DataManager.Instance.PlayerData.LevelMap;
        SceneManagerLoad.Instance.LoadSceneAsyn(SceneManagerLoad.SCENE_GAME);
    }

    private void BuyRemoveAds() {
        IAPManager.Instance.BuyItem("com.metagame.removeads", (value) => {
            if(value) {
                DataManager.Instance.PlayerData.AddItem(new ItemStack(ItemID.REMOVEADS,1));
            }
        });
    }
}
