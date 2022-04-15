using DG.Tweening;
using STU;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : Singleton<LoadingManager> {
    [SerializeField] private Image cur_;
    [SerializeField] private float time = 1;
    private Tween tween;
    private bool LoadFinal;
    private void OnEnable() {
        EventDispatcher.AddListener<EventKey.LoadFinal>(LoadHome);
    }

    private void OnDisable() {
        EventDispatcher.RemoveListener<EventKey.LoadFinal>(LoadHome);
        tween.CheckKillTween();
    }


    private void Start() {
        LoadFinal = false;
        cur_.transform.localScale = new Vector3(0, 1, 1);
        tween = cur_.transform.DOScaleX(0.8f, time).SetEase(Ease.Linear).OnComplete(() => {
            LoadFinal = true;
            if(GameManager.Instance.State == GameManager.States.Started) {
                SceneManager.Instance.LoadSceneAsyn(SceneManager.SCENE_HOME);
            } else {
                Debug.Log("Load too long");
            }
        });
    }

    private void LoadHome(EventKey.LoadFinal evt) {
        if(LoadFinal == true) {
            SceneManager.Instance.LoadSceneAsyn(SceneManager.SCENE_HOME);
        }
    }
}

