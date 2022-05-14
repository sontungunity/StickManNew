using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinPlayer : MonoBehaviour
{
    [SerializeField] private Button btn_Player;
    [SerializeField] private SkeletonGraphic skeleton;
    [SerializeField, SpineAnimation] private string animIdle; 
    [SerializeField, SpineAnimation] private List<string> lstAnim;
    [SerializeField] private Action evtComplate;
    private void Awake() {
        skeleton.AnimationState.Complete += HandleEventComplete;
        btn_Player.onClick.AddListener(HalderOnSelect);
    }

    private void HandleEventComplete(TrackEntry trackEntry) {
        evtComplate?.Invoke();
        evtComplate = null;
    }

    private void HalderOnSelect() {
        int indexRandom = UnityEngine.Random.Range(0,lstAnim.Count);
        skeleton.AnimationState.SetAnimation(0, lstAnim[indexRandom], false);
        evtComplate = () => { skeleton.AnimationState.SetAnimation(0, animIdle, false); };
    }
}
