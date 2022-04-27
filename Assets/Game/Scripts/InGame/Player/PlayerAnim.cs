using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : SpineBase {
    [Header("NameAnime")]
    [SerializeField, SpineAnimation] private string animRun;
    [SerializeField, SpineAnimation] private string animJump,animJumpFall;
    [SerializeField, SpineAnimation] private string animClimb;
    [SerializeField, SpineAnimation] private string animDash;
    [SerializeField, SpineAnimation] private string attack1,attack2,attack3;
    [SerializeField, SpineAnimation] private string animDie;
    [SerializeField, SpineAnimation] private string animGetDame;

    protected override void Awake() {
        base.Awake();

    }



    public void HalderAnim(EnumPlayerStatus typeAnim, Action callback = null) {
        switch(typeAnim) {
            case EnumPlayerStatus.IDLE:
                SetAnim(0, animIdle, true, callback);
                break;
            case EnumPlayerStatus.RUN:
                SetAnim(0, animRun, true, callback);
                break;
            case EnumPlayerStatus.DASH:
                SetAnim(0, animDash, true, callback);
                break;
            case EnumPlayerStatus.JUMP:
                Debug.Log("Jump");
                SetAnim(0, animJump, true, callback);
                break;
            case EnumPlayerStatus.JUMPFALL:
                SetAnim(0, animJumpFall, true, callback);
                break;
            case EnumPlayerStatus.ATTACK1:
                SetAnim(0, attack1, false, callback);
                break;
            case EnumPlayerStatus.ATTACK2:
                SetAnim(0, attack2, false, callback);
                break;
            case EnumPlayerStatus.ATTACK3:
                SetAnim(0, attack3, false, callback);
                break;
            case EnumPlayerStatus.CLIMB:
                SetAnim(0, animClimb, false, callback);
                break;
            case EnumPlayerStatus.DIE:
                SetAnim(0, animDie, false, callback);
                break;
            case EnumPlayerStatus.GETDAME:
                SetAnim(0, animGetDame, false, callback);
                break;
            default:
                SetAnim(0, animIdle, true, callback);
                break;
        }
    }

    //public void DOAnimIdle(int trackIndex = 0, bool loop = true, Action callBack = null) {
    //    cur_Anime.Set(EnumPlayerStatus.IDLE, 0);
    //    SetAnim(trackIndex, animIdle, loop, callBack);
    //}
}




