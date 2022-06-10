using DG.Tweening;
using STU;
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
            //curStatus = EnemyStatus.GET_DAME;
            rg2D.velocity = Vector2.zero;
            enemyAnim.SetAnimGetDame(()=> {
                enemyAnim.AnimIdle();
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
        SpawnerCoin.Instance.SpawnerII(transform.position + Vector3.up*2, 15);
        InGameManager.Instance.AddEnemyDie(this);
    }
}
