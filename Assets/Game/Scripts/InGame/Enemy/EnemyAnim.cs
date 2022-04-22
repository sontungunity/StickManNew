using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : SpineBase
{
    [SerializeField, SpineAnimation] private string anim_Die;
    [SerializeField, SpineAnimation] private string anim_GetDame;
    [SerializeField, SpineAnimation] private string anim_Attack;
    [SerializeField, SpineAnimation] private string anim_Walk;

    public void SetAnimDie(Action callBack = null) {
        SetAnim(0, anim_Die, false, callBack);
    }

    public void SetAnimGetDame(Action callBack = null) {
        SetAnim(0, anim_GetDame, false, callBack);
    }
    public void SetAnimAttack(Action callBack = null) {
        SetAnim(0, anim_Attack, false, callBack);
    }

    public void SetAnimWalk(Action callBack = null) {
        SetAnim(0, anim_Walk, true, callBack);
    }
}
