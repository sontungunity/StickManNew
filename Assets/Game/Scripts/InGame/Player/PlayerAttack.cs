using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField,SpineEvent] private string eventATK;
    [SerializeField] private PlayerAnim playerAnim;
    [SerializeField] private OverlapCircleAll circleAttackInfo;
    [SerializeField] private Player player;
    private TurnAttack turnAttack;
    private bool inputAttack;
    public EnumPlayerAttack StatusAttack => turnAttack.curAttack;
    private void Awake() {
        turnAttack = new TurnAttack();
        playerAnim.Anim.AnimationState.Event += EventDamage;
    }

    private void Update() {
        //Input
        if(Input.GetKeyDown(KeyCode.C) || CrossPlatformInputManager.GetButtonDown("Attack")) {
            inputAttack = true;
        }
        //Halder Input 
        if(inputAttack) {
            if(turnAttack.input < 3) {
                if(turnAttack.curAttack == EnumPlayerAttack.NONE) {
                    turnAttack.input++;
                    turnAttack.timeCountDown = PlayerMovement.TIME_DELAY_KEY;
                } else if(turnAttack.timeCountDown > 0) {
                    turnAttack.input++;
                    turnAttack.timeCountDown = PlayerMovement.TIME_DELAY_KEY;
                }
            }
            inputAttack = false;
        }

        //Halder Attack Anime
        if(turnAttack.curAttack == EnumPlayerAttack.NONE && turnAttack.input > 0) {
            DoAttack();
        }

        //Halder time
        if(turnAttack.timeCountDown > 0) {
            turnAttack.timeCountDown -= Time.deltaTime;
        }
    }

    private void DoAttack() {
        if(turnAttack.curAttack == EnumPlayerAttack.NONE && turnAttack.input > 0) {
            if(playerAnim.SetPlayerAnim(EnumPlayerAnim.ATTACK1, DoAttack)) {
                turnAttack.curAttack = EnumPlayerAttack.ATTACK1;
            } else {
                turnAttack.Defaul();
            }
        }else if(turnAttack.curAttack == EnumPlayerAttack.ATTACK1 && turnAttack.input > 1) {
            if(playerAnim.SetPlayerAnim(EnumPlayerAnim.ATTACK2, DoAttack)) {
                turnAttack.curAttack = EnumPlayerAttack.ATTACK2;
            } else {
                turnAttack.Defaul();
            }
        }else if(turnAttack.curAttack == EnumPlayerAttack.ATTACK2 && turnAttack.input > 2) {
            if(playerAnim.SetPlayerAnim(EnumPlayerAnim.ATTACK3, DoAttack)) {
                turnAttack.curAttack = EnumPlayerAttack.ATTACK3;
            } else {
                turnAttack.Defaul();
            }
        } else {
            SetUpNoneAttack();
        }
    }

    public void SetUpNoneAttack(bool animeIdel = true) {
        if(turnAttack.curAttack == EnumPlayerAttack.ATTACK2) {
            Debug.Log("Attack2");
        }
        turnAttack.Defaul();
        if(animeIdel) {
            playerAnim.DOAnimIdle();
        }
  
    }

    void EventDamage(TrackEntry trackEntry, Spine.Event e) {
        // Play some sound if the event named "footstep" fired.
        if(e.Data.Name == eventATK) {
            Collider2D[] listCol = Physics2D.OverlapCircleAll(circleAttackInfo.transform.position, circleAttackInfo.lookRadius, circleAttackInfo.layerMask);
            foreach(var col in listCol) {
                EnemyBase enemyBase = col.GetComponent<EnemyBase>();
                if(enemyBase != null) {
                    Debug.Log("Detech Enemy");
                    enemyBase.GetDame(player.Damage);
                }
            }
        }
    }
}

public class TurnAttack {
    public int input = 0;
    public float timeCountDown = 0f;
    public EnumPlayerAttack curAttack = EnumPlayerAttack.NONE;

    public void Defaul() {
        this.input = 0;
        this.timeCountDown = 0f;
        this.curAttack = EnumPlayerAttack.NONE;
    }
}


public enum EnumPlayerAttack {
    NONE = 0,
    ATTACK1 = 1,
    ATTACK2 = 2,
    ATTACK3 = 3
}
