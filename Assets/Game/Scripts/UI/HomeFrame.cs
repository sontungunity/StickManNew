using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeFrame : FrameBase
{
    [SerializeField] private Button btn_Play;

    private void Awake() {
        btn_Play.onClick.AddListener(StartGame);
    }

    private void StartGame() {
        SceneManager.Instance.LoadSceneAsyn(SceneManager.SCENE_GAME);
    }
}
