using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : BossBase
{
    private WarriorAttack warriorAttack => enemyAttack as WarriorAttack;
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
            if(!warriorAttack.Attacking) {
                rg2D.velocity = Vector2.zero;
                enemyAnim.SetAnimGetDame(() => {
                    enemyAnim.AnimIdle();
                });
            }
            particleBlood?.Play();
        }
    }
}
