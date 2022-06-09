using STU;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPManager : Singleton<IAPManager>
{
    public void BuyItem(string key, Action<bool> onCompele) {
        onCompele(true);
    }
}
