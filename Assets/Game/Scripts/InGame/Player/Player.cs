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
    [SerializeField] private PlayerAnim playerAnim;
    private PlayerData playerData => DataManager.Instance.PlayerData;


    public void SetUpPlayer() {
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
            playerAnim.HalderAnim(EnumPlayerAnim.DIE);
        } else {
            playerAnim.HalderAnim(EnumPlayerAnim.GETDAME,()=> {
                playerAnim.HalderAnim(EnumPlayerAnim.IDLE);
            });
        }
    }
}

public enum PlayerStatus {
    NONE,
    LIFE,
    DIE
}
