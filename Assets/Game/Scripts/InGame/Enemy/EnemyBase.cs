using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    [SerializeField] private EnemyAnim enemyAnim;
    [SerializeField] private BeamRayCast eye_Befor,eye_After,distan_attack;
    [SerializeField] private EnemyStatus curStatus;
    public EnemyStatus CurStatus => curStatus;
    private EnemyStatus afterUpdateStatus;
    public DirHorizontal dirFace {
        get {
            if(transform.localEulerAngles.y == 0) {
                return DirHorizontal.RIGHT;
            } else {
                return DirHorizontal.LEFT;
            }
        }
    }
    public override void GetDame(int dame) {
        if(curStatus == EnemyStatus.DIE) {
            return;
        }
        base.GetDame(dame);
        if(heart<=0) {
            curStatus = EnemyStatus.DIE;
            enemyAnim.SetAnimDie(() => {
               transform.gameObject.SetActive(false);
            });
        } else {
            curStatus = EnemyStatus.GET_DAME;
            enemyAnim.SetAnimGetDame(() => {
                SetStatus(EnemyStatus.MOVE);
            });
        }
    }

    private void Update() {
        if(curStatus != EnemyStatus.DIE && curStatus != EnemyStatus.GET_DAME ) {
            afterUpdateStatus = EnemyStatus.MOVE;
            SetupStatus();
            SetStatus(afterUpdateStatus);
        }
    }

    private void SetupStatus() {
        foreach(var col in eye_Befor.ArrayCollider2D) {
            if(col != null && col.GetComponent<Player>()) {
                afterUpdateStatus = EnemyStatus.DETECH;
                break;
            }
        }

        if(afterUpdateStatus != EnemyStatus.DETECH) {
            foreach(var col in eye_After.ArrayCollider2D) {
                if(col != null && col.GetComponent<Player>()) {
                    afterUpdateStatus = EnemyStatus.DETECH;
                    Flip();
                    break;
                }
            }
        }

        if(afterUpdateStatus == EnemyStatus.DETECH) {
            foreach(var col in distan_attack.ArrayCollider2D) {
                if(col != null && col.GetComponent<Player>()) {
                    afterUpdateStatus = EnemyStatus.ATTACK;
                    break;
                }
            }
        }
    }


    public void Flip() {
        if(dirFace == DirHorizontal.RIGHT) {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        } else if(dirFace == DirHorizontal.LEFT) {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
    
    private bool SetStatus(EnemyStatus status) {
        if(curStatus == EnemyStatus.DIE || curStatus == status) {
            return false;
        }
        curStatus = status;
        if(curStatus == EnemyStatus.ATTACK) {
            enemyAnim.SetAnimAttack(() => {
                curStatus = EnemyStatus.MOVE;
            });
        } else if(curStatus == EnemyStatus.DETECH && curStatus == EnemyStatus.MOVE) {
            enemyAnim.SetAnimWalk();
        } else {
            enemyAnim.SetAnimWalk();
        }
        return true;
    }

}

public enum EnemyStatus {
    NONE = 0,
    MOVE = 1,
    DETECH = 2,
    ATTACK = 3,
    GET_DAME = 4,
    DIE = 5,
}

