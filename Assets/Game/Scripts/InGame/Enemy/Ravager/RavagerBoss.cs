using STU;
using UnityEngine;

public class RavagerBoss : BossBase
{
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
            //rg2D.velocity = Vector2.zero;
            //enemyAnim.SetAnimGetDame(() => {
            //    SetStatus(EnemyStatus.MOVE);
            //});

            particleBlood?.Play();
        }

    }
}
