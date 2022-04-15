using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    [SerializeField] private SpineBase spineBase;
    [SerializeField, SpineAnimation] private string animGetDame;
    public override void GetDame(int dame) {
        base.GetDame(dame);
        if(spineBase!=null) {
            spineBase.SetAnim(0,animGetDame,false,()=> {
                spineBase.AnimIdle();
            });
        }
    }
}
