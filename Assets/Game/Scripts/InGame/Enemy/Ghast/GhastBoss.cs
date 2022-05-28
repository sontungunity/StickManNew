using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhastBoss : BossBase
{
    private GhastAttack gastAttack => enemyAttack as GhastAttack;
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
            if(gastAttack.CurAttackStatus != GhastAttack.Status.MOVE && gastAttack.CurAttackStatus != GhastAttack.Status.ATTACK) {
                enemyAnim.SetAnimGetDame();
            }
            particleBlood?.Play();
        }
    }
}
