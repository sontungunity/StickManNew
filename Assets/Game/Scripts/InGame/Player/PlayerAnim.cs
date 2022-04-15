using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : SpineBase {
    [SerializeField] private List<SortAnime> lstAnime;
    [Header("NameAnime")]
    [SerializeField, SpineAnimation] private string animRun;
    [SerializeField, SpineAnimation] private string animJump,animJumpFall;
    [SerializeField, SpineAnimation] private string animClimb;
    [SerializeField, SpineAnimation] private string animDash;
    [SerializeField, SpineAnimation] private string attack1,attack2,attack3;
    private SortAnime cur_Anime;
    protected override void Awake() {
        base.Awake();
        cur_Anime = new SortAnime();
    }

    public bool SetPlayerAnim(EnumPlayerAnim typeAnim, Action callback = null) {
        SortAnime anim = lstAnime.Find(x=>x.TypeAnim == typeAnim);
        if(anim == null) {
            return false;
        }
        if(anim.TypeAnim != cur_Anime.TypeAnim && anim.Rank >= cur_Anime.Rank) {
            cur_Anime.Coppy(anim);
            HalderAnim(typeAnim, callback);
            return true;
        }
        return false;
    }

    public void HalderAnim(EnumPlayerAnim typeAnim, Action callback = null) {
        switch(typeAnim) {
            case EnumPlayerAnim.IDLE:
                SetAnim(0, animIdle, true, callback);
                break;
            case EnumPlayerAnim.RUN:
                SetAnim(0, animRun, true, callback);
                break;
            case EnumPlayerAnim.DASH:
                SetAnim(0, animDash, true, callback);
                break;
            case EnumPlayerAnim.JUMP:
                Debug.Log("Jump");
                SetAnim(0, animJump, true, callback);
                break;
            case EnumPlayerAnim.JUMPFALL:
                SetAnim(0, animJumpFall, true, callback);
                break;
            case EnumPlayerAnim.ATTACK1:
                SetAnim(0, attack1, false, callback);
                break;
            case EnumPlayerAnim.ATTACK2:
                SetAnim(0, attack2, false, callback);
                break;
            case EnumPlayerAnim.ATTACK3:
                SetAnim(0, attack3, false, callback);
                break;
            case EnumPlayerAnim.CLIMB:
                SetAnim(0, animClimb, false, callback);
                break;
            default:
                DOAnimIdle();
                break;
        }
    }

    public void DOAnimIdle(int trackIndex = 0, bool loop = true, Action callBack = null) {
        cur_Anime.Set(EnumPlayerAnim.IDLE, 0);
        SetAnim(trackIndex, animIdle, loop, callBack);
    }
}

[System.Serializable]
public class SortAnime {
    public EnumPlayerAnim TypeAnim;
    public int Rank;

    public SortAnime() {
        this.TypeAnim = EnumPlayerAnim.IDLE;
        this.Rank = 0;
    }

    public SortAnime(EnumPlayerAnim typeAnim, int rank) {
        this.TypeAnim = typeAnim;
        this.Rank = rank;
    }

    public void Set(EnumPlayerAnim typeAnim, int rank) {
        this.TypeAnim = typeAnim;
        this.Rank = rank;
    }

    public void Coppy(SortAnime sortAnime) {
        this.TypeAnim = sortAnime.TypeAnim;
        this.Rank = sortAnime.Rank;
    }

    public void Defaul() {
        this.TypeAnim = EnumPlayerAnim.IDLE;
        this.Rank = 0;
    }
}

public enum EnumPlayerAnim {
    NONE,
    IDLE,
    RUN,
    JUMP,
    JUMPFALL,
    DASH,
    ATTACK1,
    ATTACK2,
    ATTACK3,
    CLIMB,
}
