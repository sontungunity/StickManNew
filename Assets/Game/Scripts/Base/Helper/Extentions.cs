using DG.Tweening;

public static class Extentions {
    public static void CheckKillTween(this Tween tween, bool OnComplate = false) {
        if(tween != null) {
            tween.Kill(OnComplate);
        }
    }
}
