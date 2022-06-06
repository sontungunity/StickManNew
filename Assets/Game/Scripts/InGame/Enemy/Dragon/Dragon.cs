using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : BossBase
{
    private DragonAttack bossAttack => enemyAttack as DragonAttack;
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
            if(bossAttack.CurAttackStatus != DragonAttack.Status.ATTACK) {
                enemyAnim.SetAnimGetDame(() => {
                    enemyAnim.AnimIdle();
                });
            }
            particleBlood?.Play();
        }
    }
}
