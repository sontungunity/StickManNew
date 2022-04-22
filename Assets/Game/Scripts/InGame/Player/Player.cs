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
public class Player : MonoBehaviour {
    private const int ROOT_HEART = 150;
    private const int ROOT_DAMAGE = 20;
    private PlayerData playerData => DataManager.Instance.PlayerData;
    public int m_Heart;
    public int m_Damage;

    public void SetUpPlayer() {
        SetUpHeartDame();
    }

    private void SetUpHeartDame() {
        m_Heart = 150;
        m_Damage = 20;

        for(int i = 0; i <= playerData.levelPlayer;i++ ) {
            DataManager.Instance.GetDameHeartByLevel(i,out int heart,out int damage,out int coin);
            m_Heart += heart;
            m_Damage += damage;
        }     
    }
}
