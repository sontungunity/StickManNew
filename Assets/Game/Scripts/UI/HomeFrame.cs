using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using STU;

public class HomeFrame : FrameBase {
    [SerializeField] private Button btn_Play;
    [SerializeField] private Button btn_Spin,btn_Skin,btn_Daily;
    [SerializeField] private TextMeshProUGUI txt_Level;
    private void Awake() {
        btn_Play.onClick.AddListener(StartGame);
        btn_Spin.onClick.AddListener(() => { FrameManager.Instance.Push<SpinFrame>(); });
        btn_Skin.onClick.AddListener(() => { FrameManager.Instance.Push<SkinFrame>(); });
        btn_Daily.onClick.AddListener(() => { FrameManager.Instance.Push<DailyFrame>(); });
    }

    private void OnEnable() {
        EventDispatcher.AddListener<EventKey.EventLevelChange>(HalderEventLevelChange);
    }

    private void OnDisable() {
        EventDispatcher.RemoveListener<EventKey.EventLevelChange>(HalderEventLevelChange);
    }

    private void HalderEventLevelChange(EventKey.EventLevelChange evt) {
        txt_Level.text = $"LEVEL {GameManager.Instance.CurLevel}";
    }

    public override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        txt_Level.text = $"LEVEL {GameManager.Instance.CurLevel}";
    }

    private void StartGame() {
        SceneManager.Instance.LoadSceneAsyn(SceneManager.SCENE_GAME);
    }
}
