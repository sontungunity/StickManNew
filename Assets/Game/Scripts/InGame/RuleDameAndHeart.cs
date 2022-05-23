using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RuleDameAndHeart
{
    public const int Heart_Base_Player = 150;
    public const int Dame_Base_Player = 20;
    #region Rule Dame - Heart - Coin player;
    public static void GetDameHeartByLevel(int level, out int heart, out int damage, out int coin) {
        if(level == 0) {
            damage = 0;
            heart = 0;
            coin = 0;
        } else if(level > 0 && level < 6) { // dame = level , tiền thì cấp 1 = 300, cấp sau x2 cấp trước
            damage = level;
            heart =  2 * level;
            coin = 2 ^ (level - 1) * 30;
        } else { // cấp 6 trở đi mặc định
            damage = 5;
            heart = 10;
            coin = 2 ^ (level - 1) * 30;
        }
    }
    #endregion
}
