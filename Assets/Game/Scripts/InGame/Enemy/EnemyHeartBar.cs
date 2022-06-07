using DG.Tweening;
using UnityEngine;

public class EnemyHeartBar : MonoBehaviour {
    [SerializeField] private GameObject display;
    [SerializeField] private Transform curHeart;
    [SerializeField] private float timeBar;
    private Tween tween;
    public void Init() {
        display.SetActive(false);
        curHeart.localScale = Vector3.one;
    }

    public void UpdateHeart(float percent) {
        tween.CheckKillTween();
        display.SetActive(true);
        float time = Mathf.Abs(curHeart.localScale.x - percent) * timeBar;
        tween = curHeart.DOScaleX(percent, time).OnComplete(() => {
            display.SetActive(false);
        });
    }

    private void OnDisable() {
        tween.CheckKillTween();
    }
}
