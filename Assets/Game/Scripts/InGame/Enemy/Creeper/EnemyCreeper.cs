using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreeper : EnemyBase {
    [SerializeField] private ParticleSystem effectPref;
    public override void GetDame(int dame, GameObject objMakeDame = null) {
        if(curStatus == EnemyStatus.DIE || curHeart <= 0) {
            return;
        }
        curHeart -= dame;
        if(curHeart <= 0) {
            Die(objMakeDame);
        } else {
            curStatus = EnemyStatus.GET_DAME;
            rg2D.velocity = Vector2.zero;
            enemyAttack.Attack(() => { DieByExploed(); });
            enemyBar.UpdateHeart(curHeart / (float)originHeart);
        }
    }

    protected override bool SetStatus(EnemyStatus status) {
        if(curStatus == EnemyStatus.DIE || curStatus == status) {
            return false;
        }
        curStatus = status;
        if(curStatus == EnemyStatus.ATTACK) {
            rg2D.velocity = Vector2.zero;
            enemyAttack.Attack(() => { DieByExploed(); });
        } else if(curStatus == EnemyStatus.DETECH || curStatus == EnemyStatus.MOVE) {
            enemyAnim.SetAnimWalk();
        } else {
            enemyAnim.SetAnimWalk();
        }
        return true;
    }

    private void DieByExploed() {
        curStatus = EnemyStatus.DIE;
        transform.gameObject.SetActive(false);
        int coinRandom = Random.Range(1,6);
        SpawnerCoin.Instance.Spawner(transform.position, coinRandom);
        InGameManager.Instance.AddEnemyDie(this);
        var effectnew = effectPref.Spawn();
        effectnew.transform.position = transform.position;
        effectnew.Play();
    }
}
