using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeFrame : FrameBase
{
    [SerializeField] private Button btn_Play;
    [SerializeField] private Button btn_Spin;
    private void Awake() {
        btn_Play.onClick.AddListener(StartGame);
        btn_Spin.onClick.AddListener(()=> { FrameManager.Instance.Push<SpinFrame>(); });
    }

    private void StartGame() {
        SceneManager.Instance.LoadSceneAsyn(SceneManager.SCENE_GAME);
    }
}
