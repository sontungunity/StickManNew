using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : CharacterBase {
    [SerializeField] private Transform display;
    [SerializeField] protected EnemyAnim enemyAnim;
    [SerializeField] protected BeamRayCast eye_Befor,eye_After,distan_attack;
    [SerializeField] protected EnemyStatus curStatus;
    [SerializeField] protected EnemyHeartBar enemyBar;
    [SerializeField] protected EnemyAttack enemyAttack;
    [SerializeField] private Collider2D collider2D;
    [SerializeField] protected Rigidbody2D rg2D;
    [Header("ForceDie")]
    [SerializeField] protected Vector2 forceDie = new Vector2(20,5);
    [Header("Effect")]
    [SerializeField] protected ParticleSystem particleBlood;
    public Rigidbody2D Rg2D => rg2D;
    public Transform Display => display;
    public EnemyStatus CurStatus => curStatus;
    protected EnemyStatus afterUpdateStatus;
    public DirHorizontal dirFace {
        get {
            if(display.localEulerAngles.y == 0) {
                return DirHorizontal.RIGHT;
            } else {
                return DirHorizontal.LEFT;
            }
        }
    }
    public float PercentHeart {
        get {
            return curHeart / (float)originHeart;
        }
    }

    protected Tween tween;

    protected virtual void Start() {
        enemyBar.Init();
    }



    protected virtual void Update() {
        if(curStatus == EnemyStatus.IDLE || curStatus == EnemyStatus.MOVE || curStatus == EnemyStatus.NONE || curStatus == EnemyStatus.DETECH) {
            afterUpdateStatus = EnemyStatus.MOVE;
            SetupStatus();
            SetStatus(afterUpdateStatus);
        }
        //if(Input.GetKeyDown(KeyCode.D)) {
        //    Die(InGameManager.Instance.Player.gameObject);
        //}
    }

    public override void GetDame(int dame,GameObject objMakeDame = null) {
        if(curStatus == EnemyStatus.DIE || curHeart <= 0) {
            return;
        }
        curHeart -= dame;
        if(curHeart <= 0) {
            Die(objMakeDame);
        } else {
            curStatus = EnemyStatus.GET_DAME;
            rg2D.velocity = Vector2.zero;
            enemyAnim.SetAnimGetDame(() => {
                SetStatus(EnemyStatus.IDLE);
            });
            enemyBar.UpdateHeart(PercentHeart);
            particleBlood?.Play();
        }
    }
    protected virtual void SetupStatus() {
        foreach(var col in distan_attack.ArrayCollider2D) {
            if(col != null && col.GetComponent<Player>() != null) {
                afterUpdateStatus = EnemyStatus.ATTACK;
                return;
            }
        }

        foreach(var col in eye_Befor.ArrayCollider2D) {
            if(col != null && col.GetComponent<Player>() != null) {
                afterUpdateStatus = EnemyStatus.DETECH;
                break;
            }
        }

        if(afterUpdateStatus != EnemyStatus.DETECH) {
            foreach(var col in eye_After.ArrayCollider2D) {
                if(col != null && col.GetComponent<Player>() != null) {
                    afterUpdateStatus = EnemyStatus.DETECH;
                    Flip();
                    break;
                }
            }
        }
    }


    public void Flip() {
        if(dirFace == DirHorizontal.RIGHT) {
            display.localEulerAngles = new Vector3(0, 180, 0);
        } else if(dirFace == DirHorizontal.LEFT) {
            display.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    protected virtual bool SetStatus(EnemyStatus status) {
        if(curStatus == status) {
            return true;
        }
        curStatus = status;
        if(curStatus == EnemyStatus.ATTACK) {
            rg2D.velocity = Vector2.zero;
            enemyAttack.Attack(() => { 
                SetStatus(EnemyStatus.IDLE); 
            });
        } else if(curStatus == EnemyStatus.DETECH || curStatus == EnemyStatus.MOVE) {
            enemyAnim.SetAnimWalk();
        } else {
            enemyAnim.SetAnimWalk();
        }
        return true;
    }

    protected virtual void Die(GameObject objMakeDame = null) {
        curStatus = EnemyStatus.DIE;
        enemyAnim.SetAnimDie();
        tween = DOVirtual.DelayedCall(2f, () => {
            transform.gameObject.SetActive(false);
        });
        collider2D.isTrigger = true;
        if(objMakeDame!=null && objMakeDame.transform.position.x > transform.position.x) {
            rg2D.AddForce(new Vector2(-forceDie.x,forceDie.y), ForceMode2D.Impulse);
        } else {
            rg2D.AddForce(forceDie, ForceMode2D.Impulse);
        }
        SpawnerCoin.Instance.Spawner(transform.position, 3);
        InGameManager.Instance.AddEnemyDie();
    }
}

public enum EnemyStatus {
    NONE = 0,
    IDLE = 1,
    MOVE = 2,
    DETECH = 3,
    ATTACK = 4,
    GET_DAME = 5,
    DIE = 6,
    WIN = 7,
}

