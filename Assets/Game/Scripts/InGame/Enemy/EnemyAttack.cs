using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected EnemyAnim enemyAnim;
    [SerializeField, SpineEvent] private string eventATK;
    [SerializeField] private OverlapCircleAll circleAttackInfo;
    [SerializeField] protected EnemyBase enemyBase;
    [SerializeField] private float timeDelayAttack= 1f;
    private Tween tween;
    private bool canAttack;
    protected virtual void Awake() {
        enemyAnim.Anim.AnimationState.Event += EventDamege;
        canAttack = true;
    }

    public virtual void Attack(Action callback = null ) {
        enemyBase.Rg2D.velocity = Vector2.zero;
        if(canAttack) {
            canAttack = false;
            enemyAnim.SetAnimAttack(()=> {
                callback?.Invoke();
                tween.CheckKillTween();
                tween = DOVirtual.DelayedCall(timeDelayAttack, () => {
                    canAttack = true;
                });
            });
        }
        
    }

    protected virtual void EventDamege(TrackEntry trackEntry, Spine.Event e) {
        // Play some sound if the event named "footstep" fired.
        if(e.Data.Name == eventATK && circleAttackInfo != null) {
            Collider2D[] listCol = Physics2D.OverlapCircleAll(circleAttackInfo.transform.position, circleAttackInfo.lookRadius, circleAttackInfo.layerMask);
            foreach(var col in listCol) {
                Player player = col.GetComponent<Player>();
                if(player != null) {
                    Debug.Log("Detech player");
                    player.GetDame(enemyBase.curDame);
                }
            }
        }
    }

    private void OnDisable() {
        tween.CheckKillTween();
    }
}
