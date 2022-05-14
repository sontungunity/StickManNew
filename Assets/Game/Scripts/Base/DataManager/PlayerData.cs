using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public int levelPlayer = 0;
    public int life = 5;
    public int levelMap = 0;
    public List<ItemStack> eventory = new List<ItemStack>(){new ItemStack(ItemID.COIN,10)};
    public SpinSave SpinSave = new SpinSave();

    public void AddItem(ItemStack itemStack) {
        ItemStack result = GetItemSaveByItemId(itemStack.ItemID);
        result.Add(itemStack.Amount);
        EventDispatcher.Dispatch<EventKey.IteamChange>(new EventKey.IteamChange(itemStack.ItemID, result.Amount, itemStack.Amount));
    }

    public ItemStack GetItemSaveByItemId(ItemID itemID) {
        ItemStack result = eventory.Find(x => x.ItemID == itemID);
        if(result == null) {
            result = new ItemStack(itemID, 0);
            eventory.Add(result);
        }
        return result;
    }

    public bool GetLevelPass(int level) {
        if(levelMap>= DataManager.Instance.LevelMapMax) {
            levelMap = DataManager.Instance.LevelMapMax;
            return false;
        }

        if(level+1 > levelMap) {
            levelMap = level+1;
            return true;
        }

        return false;
    }
}
