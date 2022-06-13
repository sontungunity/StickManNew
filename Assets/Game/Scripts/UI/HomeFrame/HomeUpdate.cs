using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using STU;

public class HomeUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Level,txt_Hp,txt_Damage;
    [SerializeField] private Button btn_UpByCoin,btn_UpByAds;
    private PlayerData playerData => DataManager.Instance.PlayerData;

    private void Awake() {
        btn_UpByCoin.onClick.AddListener(HalderUpdateByCoin);
        btn_UpByAds.onClick.AddListener(HalderUpdateByAds);
    }

    private void OnEnable() {
        EventDispatcher.AddListener<EventKey.IteamChange>(HalderItemChanger);
    }

    private void OnDisable() {
        GenderInfoPlayer();
        GenderInfoButton();
        EventDispatcher.RemoveListener<EventKey.IteamChange>(HalderItemChanger);
    }

    private void HalderItemChanger(EventKey.IteamChange evt) {
        if(evt.itemID == ItemID.COIN) {
            GenderInfoButton();
        }
    }


    private void Start() {
        GenderInfoPlayer();
        GenderInfoButton();
    }

    private void GenderInfoPlayer() {
        txt_Level.text = (playerData.LevelPlayer+1).ToString();
        var levelPlayerInfo = RuleDameAndHeart.GetTotalDameHeartCoinByLevel(playerData.LevelPlayer);
        txt_Hp.text = levelPlayerInfo.Heart.ToString();
        txt_Damage.text = levelPlayerInfo.Damage.ToString();
       
    }

    private void GenderInfoButton() {
        bool enoughCoin = playerData.Enought(ItemID.COIN,RuleDameAndHeart.Coin_UP_Level);
        btn_UpByCoin.gameObject.SetActive(enoughCoin);
        btn_UpByAds.gameObject.SetActive(!enoughCoin);
    }

    private void HalderUpdateByCoin() {
        if(playerData.RemoveItem(new ItemStack(ItemID.COIN, RuleDameAndHeart.Coin_UP_Level))) {
            playerData.LevelPlayer++;
            GenderInfoPlayer();
            EventDispatcher.Dispatch<EventKey.EventUpdatePower>(new EventKey.EventUpdatePower());
        }
    }

    private void HalderUpdateByAds() {
        AdsManager.Instance.ShowRewarded((value)=> {
            if(value) {
                playerData.LevelPlayer++;
                GenderInfoPlayer();
                EventDispatcher.Dispatch<EventKey.EventUpdatePower>(new EventKey.EventUpdatePower());
            } else {
                TextNotify.Instance.ShowFailAds();
            }
        });
    }
}
