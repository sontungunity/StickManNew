using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHorizontal))]
[RequireComponent(typeof(PlayerVertical))]
[RequireComponent(typeof(PlayerAnim))]
[RequireComponent(typeof(PlayerAttack))]
public class Player : CharacterBase {
    private const int ROOT_HEART = 150;
    private const int ROOT_DAMAGE = 20;
    [SerializeField] private List<SortStatus> lstSortStatus;
    [SerializeField] private PlayerAnim playerAnim;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerVertical playerVertical;
    [SerializeField] private PlayerHorizontal playerHorizontal;
    private PlayerData playerData => DataManager.Instance.PlayerData;
    public SortStatus curStatus;

    public void SetUpPlayer() {
        curStatus = new SortStatus();
        SetUpHeartDame();
    }

    private void SetUpHeartDame() {
        curHeart = 150;
        curDame = 20;

        for(int i = 0; i <= playerData.levelPlayer;i++ ) {
            DataManager.Instance.GetDameHeartByLevel(i,out int heart,out int damage,out int coin);
            curHeart += heart;
            curDame += damage;
        }     
    }

    public override void GetDame(int dame) {
        base.GetDame(dame);
        if(curHeart<=0) {
            playerAnim.HalderAnim(EnumPlayerStatus.DIE);
        } else {
            playerAnim.HalderAnim(EnumPlayerStatus.GETDAME,()=> {
                playerAnim.HalderAnim(EnumPlayerStatus.IDLE);
            });
        }
    }

    public bool SetPlayerStatusCheckRank(EnumPlayerStatus typeAnim, Action callback = null) {
        SortStatus sortStatus = lstSortStatus.Find(x=>x.TypeStatus == typeAnim);
        if(sortStatus == null) {
            return false;
        }
        if(sortStatus.TypeStatus != curStatus.TypeStatus && sortStatus.Rank >= curStatus.Rank) {
            SetPlayerStatus(sortStatus.TypeStatus,sortStatus.Rank,callback);
            return true;
        }
        return false;
    }

    private void SetPlayerStatus(EnumPlayerStatus statusplayer,int rank, Action callback = null) {
        curStatus.Set(statusplayer,rank);
        playerAnim.HalderAnim(curStatus.TypeStatus, callback);
        if(curStatus.TypeStatus == EnumPlayerStatus.DASH) {
            playerAttack.SetUpNoneAttack();
            playerVertical.SetUpNoVertical();
        } else if(curStatus.TypeStatus == EnumPlayerStatus.GETDAME || curStatus.TypeStatus == EnumPlayerStatus.DIE) {
            playerAttack.SetUpNoneAttack();
            playerVertical.SetUpNoVertical();
            playerHorizontal.SetUpNoMove();
        }
    }

    public void SetIdleCheckStatus(List<EnumPlayerStatus> lstStatus,Action callback = null) {
        if(curStatus.TypeStatus == EnumPlayerStatus.IDLE) {
            return;
        }
        bool canSet = false;
        foreach(var status in lstStatus) {
            if(status == curStatus.TypeStatus) {
                canSet = true;
                break;
            }
        }

        if(canSet) {
            SetPlayerStatus(EnumPlayerStatus.IDLE,0,callback);
        }
    }

}

[System.Serializable]
public class SortStatus {
    public EnumPlayerStatus TypeStatus;
    public int Rank;

    public SortStatus() {
        this.TypeStatus = EnumPlayerStatus.IDLE;
        this.Rank = 0;
    }

    public SortStatus(EnumPlayerStatus typeAnim, int rank) {
        this.TypeStatus = typeAnim;
        this.Rank = rank;
    }

    public void Set(EnumPlayerStatus typeAnim, int rank) {
        this.TypeStatus = typeAnim;
        this.Rank = rank;
    }

    public void Coppy(SortStatus sortAnime) {
        this.TypeStatus = sortAnime.TypeStatus;
        this.Rank = sortAnime.Rank;
    }

    public void Defaul() {
        this.TypeStatus = EnumPlayerStatus.IDLE;
        this.Rank = 0;
    }
}

public enum EnumPlayerStatus {
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
    DIE,
    GETDAME,
}
