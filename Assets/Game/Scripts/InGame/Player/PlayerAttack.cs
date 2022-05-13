using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : MonoBehaviour
{
    public static float TIME_DELAY_KEY = 0.5f;
    [SerializeField,SpineEvent] private string eventATK;
    [SerializeField] private PlayerAnim playerAnim;
    [SerializeField] private OverlapCircleAll circleAttackInfo;
    [SerializeField] private Player player;
    [Header("ListAnim")]
    [SerializeField] private List<EnumPlayerStatus> lstStatusIdle;
    public TurnAttack TurnAttack;
    private bool inputAttack;
    public bool IsAttacking {
        get {
            bool isAttacking = false;
            foreach(var status in lstStatusIdle) {
                if(status == player.CurStatus.TypeStatus) {
                    isAttacking = true;
                    break;
                }
            }
            return isAttacking; 
        }
    }
    private void Awake() {
        TurnAttack = new TurnAttack();
        playerAnim.Anim.AnimationState.Event += EventDamage;
    }

    private void Update() {
        //Input
        if(Input.GetKeyDown(KeyCode.C) || CrossPlatformInputManager.GetButtonDown("Attack")) {
            inputAttack = true;
        }
        //Halder Input 
        if(inputAttack) {
            inputAttack = false;
            if(TurnAttack.input >= 3) {
                return;
            }
            if(TurnAttack.input == 0) {
                if(player.SetPlayerStatusCheckRank(EnumPlayerStatus.ATTACK1,DoAttack)) {
                    TurnAttack.input++;
                    TurnAttack.timeCountDown = TIME_DELAY_KEY;
                }
            } else if(TurnAttack.timeCountDown > 0) {
                TurnAttack.input++;
                TurnAttack.timeCountDown = TIME_DELAY_KEY;
            }
        }

        //Halder time
        if(TurnAttack.timeCountDown > 0) {
            TurnAttack.timeCountDown -= Time.deltaTime;
        }
    }

    private void DoAttack() {
        if(player.CurStatus.TypeStatus == EnumPlayerStatus.ATTACK1 && TurnAttack.input > 1) {
            if(!player.SetPlayerStatusCheckRank(EnumPlayerStatus.ATTACK2, DoAttack)) {
                SetUpNoneAttack();
            } 
        }else if(player.CurStatus.TypeStatus == EnumPlayerStatus.ATTACK2 && TurnAttack.input > 2) {
            if(!player.SetPlayerStatusCheckRank(EnumPlayerStatus.ATTACK3, DoAttack)) {
                SetUpNoneAttack();
            }
        } else {
            SetUpNoneAttack();
        }
    }

    public void SetUpNoneAttack() {
        TurnAttack.Defaul();
        player.SetIdleCheckStatus(lstStatusIdle);
    }

    void EventDamage(TrackEntry trackEntry, Spine.Event e) {
        // Play some sound if the event named "footstep" fired.
        if(e.Data.Name == eventATK) {
            Collider2D[] listCol = Physics2D.OverlapCircleAll(circleAttackInfo.transform.position, circleAttackInfo.lookRadius, circleAttackInfo.layerMask);
            foreach(var col in listCol) {
                EnemyBase enemyBase = col.transform.parent.GetComponent<EnemyBase>();
                if(enemyBase != null) {
                    enemyBase.GetDame(player.curDame,gameObject);
                    SpawnerTextDame.Instance.Spawner(circleAttackInfo.transform.position, player.curDame.ToString());
                }
            }
        }
    }
}

public class TurnAttack {
    public int input = 0;
    public float timeCountDown = 0f;
    //public EnumPlayerAttack curAttack = EnumPlayerAttack.NONE;

    public void Defaul() {
        this.input = 0;
        this.timeCountDown = 0f;
        //this.curAttack = EnumPlayerAttack.NONE;
    }
}


//public enum EnumPlayerAttack {
//    NONE = 0,
//    ATTACK1 = 1,
//    ATTACK2 = 2,
//    ATTACK3 = 3
//}
