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
    
    [Header("Sound")]
    [SerializeField] private AudioClip getDame;
    [SerializeField] private AudioClip die;
    [SerializeField] private AudioClip packCoinDrop;

    public void SetAnimDie(Action callBack = null) {
        SetAnim(0, anim_Die, false, callBack);
        SoundManager.Instance.PlaySound(die);
    }

    public void SetAnimGetDame(Action callBack = null) {
        SetAnim(0, anim_GetDame, false, callBack);
        SoundManager.Instance.PlaySound(getDame);
    }
    public void SetAnimAttack(Action callBack = null) {
        int index = UnityEngine.Random.Range(0,lstStrAttack.Count);
        SetAnim(0, lstStrAttack[index], false, callBack);
    }

    public void SetAnimWalk(Action callBack = null) {
        SetAnim(0, anim_Walk, true, callBack);
    }
}
