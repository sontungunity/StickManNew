using STU;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : Singleton<AdsManager>
{
    public void ShowRewarded(Action<bool> callback) {
        callback?.Invoke(true);
    }
}
