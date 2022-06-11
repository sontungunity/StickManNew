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
    public static LevelInfo GetTotalDameHeartCoinByLevel(int level) {
        LevelInfo result = new LevelInfo();
        result.Heart = Heart_Base_Player + level * Heart_Up_Level;
        result.Damage = Dame_Base_Player + level * Dame_Up_Level;
        result.Coin = Coin_UP_Level * level;
        return result;
    }
    #endregion

    public static LevelInfo GetHeartDameNormalEnemy(int levelMap) {
        LevelInfo result = new LevelInfo();
        int indexTheme = Mathf.FloorToInt( (levelMap-1)/6);
        result.Heart = (Heart_Base_Player + indexTheme * 10 * Heart_Up_Level)*2/3;
        result.Damage = (Dame_Base_Player + indexTheme * 10 * Dame_Up_Level) / 2;
        return result;
    }

    public static LevelInfo GetHeartDameBoss(int levelMap) {
        LevelInfo result = new LevelInfo();
        int indexTheme = Mathf.FloorToInt( (levelMap-1)/6);
        result.Damage = Dame_Base_Player + indexTheme * 10 * Dame_Up_Level;
        result.Heart = result.Damage*30;
        return result;
    }
}

public struct LevelInfo{
    public int Heart;
    public int Damage;
    public int Coin;
}