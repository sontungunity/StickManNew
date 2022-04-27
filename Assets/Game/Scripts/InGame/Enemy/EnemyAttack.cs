using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyAnim enemyAnim;
    [SerializeField, SpineEvent] private string eventATK;
    [SerializeField, SpineAnimation] private List<string> lstStrAttack;
    [SerializeField] private OverlapCircleAll circleAttackInfo;
    [SerializeField] private EnemyBase enemyBase;
    private void Awake() {
        enemyAnim.Anim.AnimationState.Event += EventDamege;    }

    public void Attack(Action callback = null ) {
        int index = UnityEngine.Random.Range(0,lstStrAttack.Count);
        enemyAnim.SetAnim(0, lstStrAttack[index], false, callback);
    }

    void EventDamege(TrackEntry trackEntry, Spine.Event e) {
        // Play some sound if the event named "footstep" fired.
        if(e.Data.Name == eventATK) {
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
}
