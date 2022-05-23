using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HomeUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Level,txt_Hp,txt_Damage;
    [SerializeField] private Button btn_UpByCoin,btn_UpByAds;
    private PlayerData playerData => DataManager.Instance.PlayerData;

    private void Awake() {
        btn_UpByCoin.onClick.AddListener(HalderUpdateByCoin);
        btn_UpByAds.onClick.AddListener(HalderUpdateByAds);
    }

    private void Start() {
        GenderInfoPlayer();
    }

    private void GenderInfoPlayer() {
        txt_Level.text = playerData.LevelPlayer.ToString();
        var levelPlayerInfo = RuleDameAndHeart.GetTotalDameHeartCoinByLevel(playerData.LevelPlayer);
        txt_Hp.text = levelPlayerInfo.Heart.ToString();
        txt_Damage.text = levelPlayerInfo.Damage.ToString();
        bool enoughCoin = playerData.Enought(ItemID.COIN,RuleDameAndHeart.Coin_UP_Level);
        btn_UpByCoin.gameObject.SetActive(enoughCoin);
        btn_UpByAds.gameObject.SetActive(!enoughCoin);
    }

    private void HalderUpdateByCoin() {
        if(playerData.RemoveItem(new ItemStack(ItemID.COIN, RuleDameAndHeart.Coin_UP_Level))) {
            playerData.LevelPlayer++;
            GenderInfoPlayer();
        }
    }

    private void HalderUpdateByAds() {
        AdsManager.Instance.ShowRewarded((value)=> {
            if(value) {
                playerData.LevelPlayer++;
                GenderInfoPlayer();
            } else {
                TextNotify.Instance.ShowFailAds();
            }
        });
    }
}
