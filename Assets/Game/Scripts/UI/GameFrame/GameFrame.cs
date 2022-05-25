using System;
using UnityEngine;
using UnityEngine.UI;

public class GameFrame : FrameBase
{
    [SerializeField] private LifeAndHeart lifeAndHeart;
    [SerializeField] private Button btn_Setting;
    [SerializeField] private BossBarHeart bossBarHeart;

    private void Awake() {
        btn_Setting.onClick.AddListener(() => {
            FrameManager.Instance.Push<SettingFrame>();
        });   
    }


    public override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        bossBarHeart.StartActive(false);
    }
}
