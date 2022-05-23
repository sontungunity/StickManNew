using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpiderLandSp : EnemyBase
{
    [SerializeField] private float timeDelay = 2f;
    private Tween tweenDisable;

    protected override void OnDisable() {
        base.OnDisable();
        tweenDisable.CheckKillTween();
    }

    public void MakeEnemyDelay() {
        this.enabled = false;
        tweenDisable.CheckKillTween();
        tweenDisable = DOVirtual.DelayedCall(timeDelay, () => {
            enabled = true;
        });
    }

    protected override void Die(GameObject objMakeDame = null) {
        curStatus = EnemyStatus.DIE;
        enemyAnim.SetAnimDie();
        tween = DOVirtual.DelayedCall(2f, () => {
            transform.gameObject.SetActive(false);
        });
        GetComponent<Collider2D>().isTrigger = true;
        if(objMakeDame != null && objMakeDame.transform.position.x > transform.position.x) {
            rg2D.AddForce(new Vector2(-forceDie.x, forceDie.y), ForceMode2D.Impulse);
        } else {
            rg2D.AddForce(forceDie, ForceMode2D.Impulse);
        }
        SpawnerCoin.Instance.Spawner(transform.position, 3);
        //InGameManager.Instance.AddEnemyDie();
    }
}
