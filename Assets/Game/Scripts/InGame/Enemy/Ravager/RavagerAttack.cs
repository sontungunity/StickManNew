using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavagerAttack : EnemyAttack {
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private BeamRayCast beamFace;
    [SerializeField, SpineAnimation] private string animMove,animAttack,animShock;
    [Header("Customer")]
    [SerializeField] private float speedTarget;
    [SerializeField] private int turnMove;
    [SerializeField] private float timeStun;
    private int curTurnMove;
    private RavagerAttackType attackType;
    public RavagerAttackType AttackType => attackType;
    private Action callback;
    private Tween tween;
    public override void Attack(Action callback = null) {
        this.callback = callback;
        curTurnMove = turnMove;
        attackType = RavagerAttackType.MOVE;
        enemyAnim.SetAnim(0, animMove, true, null);
    }

    private void Update() {
        if(attackType != RavagerAttackType.MOVE) {
            return;
        }

        if(!CheckFaceCanMove()) {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
            curTurnMove--;
            if(curTurnMove>0) {
                attackType = RavagerAttackType.ATTACK;
                enemyAnim.SetAnim(0, animAttack, false,()=> {
                    enemyBase.Flip();
                    attackType = RavagerAttackType.MOVE;
                    enemyAnim.SetAnim(0, animMove, true, null);
                });
            } else {
                attackType = RavagerAttackType.SHOCK;
                enemyAnim.SetAnim(0, animShock, true);
                tween.CheckKillTween();
                tween = DOVirtual.DelayedCall(timeStun,() => {
                    attackType = RavagerAttackType.NONE;
                    callback?.Invoke();
                });
            }
            return;
        }
    }

    private void FixedUpdate() {
        if(attackType == RavagerAttackType.MOVE) {
            rb2D.velocity = new Vector2((int)enemyBase.Display.right.x * speedTarget, rb2D.velocity.y);
        }
    }

    private bool CheckFaceCanMove() {
        Collider2D collider2D = null;
        foreach(var col in beamFace.ArrayCollider2D) {
            if(col != null) {
                collider2D = col;
                break;
            }
        }

        return collider2D == null;
    }

    private void OnDisable() {
        tween.CheckKillTween();
    }

    public enum RavagerAttackType {
        NONE,
        MOVE,
        ATTACK,
        SHOCK,
    }
}

