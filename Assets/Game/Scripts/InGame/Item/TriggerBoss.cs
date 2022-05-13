using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TriggerBoss : MonoBehaviour {
    private Player player => InGameManager.Instance.Player;
    private Tween tween;

    private void OnTriggerEnter2D(Collider2D collision) {
        EventDispatcher.Dispatch<EventKey.EnterBossArea>(new EventKey.EnterBossArea());
        tween.CheckKillTween();
        tween = DOVirtual.DelayedCall(2f, () => {
            gameObject.SetActive(false);
        });

    }

    private void OnTriggerExit2D(Collider2D collision) {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        tween.CheckKillTween();
    }
}
