using Spine.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : SpineBase
{
    [SerializeField, SpineAnimation] private string anim_Die;
    [SerializeField, SpineAnimation] private string anim_GetDame;
    [SerializeField, SpineAnimation] private List<string> lstStrAttack;
    [SerializeField, SpineAnimation] private string anim_Walk;
    [SerializeField] private AudioClip fire;

    public void SetAnimDie(Action callBack = null) {
        SetAnim(0, anim_Die, false, callBack);
    }

    public void SetAnimGetDame(Action callBack = null) {
        SetAnim(0, anim_GetDame, false, callBack);
    }
    public void SetAnimAttack(Action callBack = null) {
        int index = UnityEngine.Random.Range(0,lstStrAttack.Count);
        SoundManager.Instance.PlaySound(fire);
        SetAnim(0, lstStrAttack[index], false, callBack);
    }

    public void SetAnimWalk(Action callBack = null) {
        SetAnim(0, anim_Walk, true, callBack);
    }
}
