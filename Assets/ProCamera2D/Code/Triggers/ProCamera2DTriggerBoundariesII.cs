using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProCamera2DTriggerBoundariesII : ProCamera2DTriggerBoundaries {
    [SerializeField] private float sizeBonus = 40;
    private float originLeft,originRight;
    private Tween tween;
    protected override void EnteredTrigger() {
        base.EnteredTrigger();
        tween.CheckKillTween();
        NumericBoundaries.UseNumericBoundaries = true;
        if(originLeft != 0 && originRight != 0) {
            NumericBoundaries.LeftBoundary = originLeft;
            NumericBoundaries.RightBoundary = originRight;
        }
    }

    protected override void ExitedTrigger() {
        base.ExitedTrigger();
        originLeft = NumericBoundaries.LeftBoundary;
        originRight = NumericBoundaries.RightBoundary;
        tween = DOTween.To(() => 0, (value) => {
            NumericBoundaries.LeftBoundary -= value;
            NumericBoundaries.RightBoundary += value;
        }, sizeBonus, sizeBonus * 2).SetEase(Ease.Linear);
    }

    protected override void OnDisable() {
        base.OnDisable();
        tween.CheckKillTween();
        NumericBoundaries.UseNumericBoundaries = false;
    }
}
