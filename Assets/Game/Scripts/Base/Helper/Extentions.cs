using DG.Tweening;
using UnityEngine;

public static class Extentions {
    public static void CheckKillTween(this Tween tween, bool OnComplate = false) {
        if(tween != null) {
            tween.Kill(OnComplate);
        }
    }

    public static Color GetColorByInt(this int index) { //0.green, 1.red, 2.yellow, 3.blue, Defaul.While
        if(index == 0) {
            return Color.green;
        }else if(index == 1) {
            return Color.red;
        }else if(index == 2) {
            return Color.yellow;
        }else if(index == 3) {
            return Color.blue;
        }else{
            return Color.white;
        }
    }
}
