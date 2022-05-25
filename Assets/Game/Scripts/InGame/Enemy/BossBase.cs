using DG.Tweening;
using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : EnemyBase
{
    protected override void Start() {
        SetStatus(EnemyStatus.IDLE);
    }

    public override void GetDame(int dame, GameObject objMakeDame = null) {
        if(curStatus == EnemyStatus.DIE || curHeart <= 0) {
            return;
        }
        curHeart -= dame;
        EventDispatcher.Dispatch<EventKey.BossGetDame>(new EventKey.BossGetDame());
        if(curHeart <= 0) {
            Die(objMakeDame);
        } else {
            curStatus = EnemyStatus.GET_DAME;
            rg2D.velocity = Vector2.zero;
            enemyAnim.SetAnimGetDame(() => {
                SetStatus(EnemyStatus.MOVE);
            });
            particleBlood?.Play();
        }
    }

    protected override void Die(GameObject objMakeDame = null) {
        curStatus = EnemyStatus.DIE;
        enemyAttack.enabled = false;
        enemyAnim.SetAnimDie();
        tween = DOVirtual.DelayedCall(2f, () => {
            transform.gameObject.SetActive(false);
        });
        //GetComponent<Collider2D>().isTrigger = true;
        //if(objMakeDame != null && objMakeDame.transform.position.x > transform.position.x) {
        //    rg2D.AddForce(new Vector2(-forceDie.x, forceDie.y), ForceMode2D.Impulse);
        //} else {
        //    rg2D.AddForce(forceDie, ForceMode2D.Impulse);
        //}
        SpawnerCoin.Instance.Spawner(transform.position, 20);
        InGameManager.Instance.AddEnemyDie();
    }
}
