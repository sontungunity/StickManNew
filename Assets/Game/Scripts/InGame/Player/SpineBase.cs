using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpineBase : MonoBehaviour {
    [SerializeField] private SkeletonAnimation animmation;
    [SerializeField] private Action evtComplate;
    [SerializeField, SpineAnimation] protected string animIdle;
    public SkeletonAnimation Anim => animmation;
    private Tween tween;
    protected virtual void Awake() {
        animmation.AnimationState.Complete += HandleEventComplete;
    }
    
    private void HandleEventComplete(TrackEntry trackEntry) {
        evtComplate?.Invoke();
        evtComplate = null;
    }

    public void SetAnim(int trackIndex, string animationName, bool loop,Action callBack = null ) {
        if(animmation == null) {
            callBack?.Invoke();
            return;
        }
        animmation.AnimationState.SetAnimation(trackIndex, animationName, loop);
        evtComplate = callBack;
    }

    public void SetAnimByTime(int trackIndex, string animationName, float timeDelay, Action callBack) {
        if(animmation == null) {
            tween.CheckKillTween();
            tween = DOVirtual.DelayedCall(timeDelay, () => {
                callBack?.Invoke();
            });
            return;
        }
        tween.CheckKillTween();
        tween = DOVirtual.DelayedCall(timeDelay, () => {
            animmation.AnimationState.SetAnimation(trackIndex, animationName, false);
            evtComplate = callBack;
        });
        animmation.AnimationState.SetAnimation(trackIndex, animationName, true);
    }

    public void AnimIdle(int trackIndex = 0, bool loop = true, Action callBack = null) {
        SetAnim(trackIndex, animIdle,loop,callBack);
    }

    public float SetTimeAnim(int trackIndex, string animationName, bool loop) {
        SetAnim(trackIndex, animationName, loop);
        return animmation.AnimationState.GetCurrent(0).TrackTime;
    }

    public void Fade(bool appear,float timeFade = 1,Action callback = null) {
        //tween.CheckKillTween();
        //if(appear) {
        //    tween = DOTween.To(() => 0f, (value) => { Anim.skeleton.A = value; }, 1f, timeFade).OnComplete(() => {
        //        callback?.Invoke();
        //    });
        //} else {
        //    tween = DOTween.To(() => 1f, (value) => { Anim.skeleton.A = value; }, 0f, timeFade).OnComplete(() => {
        //        callback?.Invoke();
        //    });
        //}
    }

    private void OnDisable() {
        //tween.CheckKillTween();
    }

    public void SetOrder(int order) {
        Anim.GetComponent<MeshRenderer>().sortingOrder = order;
    }
}
