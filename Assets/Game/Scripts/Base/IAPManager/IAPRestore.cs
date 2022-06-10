using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class IAPManager {
    private void RestorePackGame(string productId) {
        if("com.gafu.skins".Equals(productId)) {
            DataManager.Instance.PlayerData.AddItem(new ItemStack(ItemID.UNLOCKSKINS,1));
        }else if("com.gafu.removeads".Equals(productId)) {
            DataManager.Instance.PlayerData.AddItem(new ItemStack(ItemID.REMOVEADS, 1));
        } else if("com.gafu.vip".Equals(productId)) {
            DataManager.Instance.PlayerData.AddItem(new ItemStack(ItemID.UNLOCKSKINS, 1));
            DataManager.Instance.PlayerData.AddItem(new ItemStack(ItemID.REMOVEADS, 1));
        }
    }
}
