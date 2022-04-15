using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public int level = 0;
    public List<ItemStack> eventory = new List<ItemStack>(){new ItemStack(ItemID.COIN,10)};
}
