using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RuleDameAndHeart
{
    public const int Heart_Base_Player = 150;
    public const int Dame_Base_Player = 20;
    public const int Coin_UP_Level = 300;
    public const int Heart_Up_Level = 15;
    public const int Dame_Up_Level = 2;
    #region Rule Dame - Heart - Coin player;
    public static LevelPlayerInfo GetTotalDameHeartCoinByLevel(int level) {
        LevelPlayerInfo result = new LevelPlayerInfo();
        result.Heart = Heart_Base_Player + level * Heart_Up_Level;
        result.Damage = Dame_Base_Player + level * Dame_Base_Player;
        result.Coin = Coin_UP_Level * level;
        return result;
    }
    #endregion
}

public struct LevelPlayerInfo{
    public int Heart;
    public int Damage;
    public int Coin;
}