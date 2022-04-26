using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFrame : FrameBase
{
    [SerializeField] private LifeAndHeart lifeAndHeart;
    [SerializeField] private TextMeshProUGUI txt_AmountEnemy;
    private LevelMap levelMap => InGameManager.Instance.LevelMap;
    public override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        lifeAndHeart.Init();
        txt_AmountEnemy.text = $"{InGameManager.Instance.enemyKilled}/{levelMap.AmountEnemy}";
    }
}
