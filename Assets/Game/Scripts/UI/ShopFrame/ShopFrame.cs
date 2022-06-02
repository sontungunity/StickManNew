using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopFrame : FrameBase
{
    [SerializeField] private List<PackFrames> packFrame;
    [SerializeField] private List<PackBtn> packBtn;
    [SerializeField] private List<PriceBtn> priceBtn;

    public override void OnShow(Action onCompleted = null, bool instant = false)
    {
        base.OnShow(onCompleted, instant);
        packFrame[0].ShowPack0();
        packFrame[1].ShowPack1();
        packFrame[2].ShowPack2();
        packFrame[3].ShowPack3();
    }
}
