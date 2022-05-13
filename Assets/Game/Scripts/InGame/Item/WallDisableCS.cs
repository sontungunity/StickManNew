using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallDisableCS : MonoBehaviour
{
    private Tween tween;
    private void OnEnable() {
        EventDispatcher.AddListener<EventKey.EnemyDie>(HalderEnemyDie);
    }

    private void OnDisable() {
        tween.CheckKillTween();
        EventDispatcher.RemoveListener<EventKey.EnemyDie>(HalderEnemyDie);
    }

    private void HalderEnemyDie(EventKey.EnemyDie evt) {
        if(InGameManager.Instance.KillAllEnemy) {
            tween.CheckKillTween();
            tween = DOVirtual.DelayedCall(3f, () => {
                gameObject.SetActive(false);
            });
        }
    }
}
