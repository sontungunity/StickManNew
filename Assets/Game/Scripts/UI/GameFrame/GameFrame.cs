using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFrame : FrameBase
{
    [SerializeField] private LifeAndHeart lifeAndHeart;
    public override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
    }
}
