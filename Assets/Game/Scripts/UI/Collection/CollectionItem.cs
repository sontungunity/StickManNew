using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CollectionItem : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip m_sound;
    private Tween tween;
    public void Show(Sprite icon) {
        this.icon.sprite = icon;
    }

    public void Move(Vector2 startPos, Vector2 endPos, Action callback = null, float timeDelay = 0) {
        transform.position = startPos;
        float time = Vector2.Distance(startPos,endPos)/speed;
        tween = DOVirtual.DelayedCall(timeDelay, () => {
            tween = transform.DOMove(endPos, time).SetEase(Ease.Linear).OnComplete(() => {
                SoundManager.Instance.PlaySound(m_sound);
                gameObject.Recycle();
                callback?.Invoke();
            });
        });
    }

    public void Move(Vector2 root, Vector2 startPos, Vector2 endPos, Action callback = null, float timeDelay = 0) {
        transform.position = root;
        tween = transform.DOMove(startPos, 0.2f).OnComplete(() => {
            float time = Vector2.Distance(startPos,endPos)/speed;
            tween = DOVirtual.DelayedCall(timeDelay, () => {
                tween = transform.DOMove(endPos, time).SetEase(Ease.Linear).OnComplete(() => {
                    SoundManager.Instance.PlaySound(m_sound);
                    gameObject.Recycle();
                    callback?.Invoke();
                });
            });
        });
    }

    private void OnDisable() {
        tween.CheckKillTween();
    }
}
