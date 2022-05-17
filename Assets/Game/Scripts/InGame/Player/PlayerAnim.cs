using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : SpineBase {
    [Header("Player")]
    [SerializeField] private Player player;
    [Header("NameAnime")]
    [SerializeField, SpineAnimation] private string animRun;
    [SerializeField, SpineAnimation] private string animJump,animJumpFall,animJumpBefor;
    [SerializeField, SpineAnimation] private string animClimb;
    [SerializeField, SpineAnimation] private string animDash;
    [SerializeField, SpineAnimation] private string animDie;
    [SerializeField, SpineAnimation] private string animGetDame;
    [SerializeField, SpineAnimation] private string animWin;
    [SerializeField, SpineAnimation] private string animStun;
    [Header("NameAnime Attack")] 
    [SerializeField, SpineAnimation] private List<string> lstAttackNone;
    [SerializeField, SpineAnimation] private List<string> lstAttackWeapon;

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
                SetAnim(0, animJump, true, callback);
                break;
            case EnumPlayerStatus.JUMPFALL:
                SetAnim(0, animJumpFall, true, callback);
                break;
            case EnumPlayerStatus.ATTACK1:
                SetAnim(0, GetStringAnimByWeapon(player.WeaponID, 0), false, callback);
                break;
            case EnumPlayerStatus.ATTACK2:
                SetAnim(0, GetStringAnimByWeapon(player.WeaponID, 1), false, callback);
                break;
            case EnumPlayerStatus.ATTACK3:
                SetAnim(0, GetStringAnimByWeapon(player.WeaponID,2), false, callback);
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
            case EnumPlayerStatus.WIN:
                SetAnim(0, animWin, false, callback);
                break;
            case EnumPlayerStatus.STUN:
                SetAnim(0, animStun, false, callback);
                break;
            case EnumPlayerStatus.JUMPBEFOR:
                SetAnim(0, animJumpBefor, false, callback);
                break;
            default:
                SetAnim(0, animIdle, true, callback);
                break;
        }
    }

    public string GetStringAnimByWeapon(WeaponID id, int index) {
        if(player.WeaponID == WeaponID.SWORD) {
            return lstAttackWeapon[index];
        } else {
            return lstAttackNone[index];
        }
    }

    public void SetSkin(params string[] lstSkin) {
        Skin skin = new Skin("Skin");
        foreach(string nameSkin in lstSkin) {
            skin.AddSkin(Anim.Skeleton.Data.FindSkin(nameSkin));
        }
        Anim.Skeleton.SetSkin(skin);
        Anim.Skeleton.SetSlotsToSetupPose();
    }

    public void AddSkin(string skinAdd) {
        Skin skin = Anim.Skeleton.Skin;
        skin.AddSkin(Anim.Skeleton.Data.FindSkin(skinAdd));
        Anim.Skeleton.SetSkin(skin);
        Anim.Skeleton.SetSlotsToSetupPose();
        //Anim.Skeleton.Skin.AddSkin(Anim.Skeleton.Data.FindSkin(skinAdd));
    }
}




