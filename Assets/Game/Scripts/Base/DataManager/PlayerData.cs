using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public int LevelPlayer = 0;
    public int Life = 5;
    public int LevelMap = 0;
    public List<ItemStack> eventory = new List<ItemStack>(){new ItemStack(ItemID.COIN,10)};
    public SpinSave SpinSave = new SpinSave();
    public DailySave DailySave = new DailySave();
    public ItemID SkinID = ItemID.SKIN_00;

    public void AddItem(ItemStack itemStack) {
        ItemStack result = GetItemSaveByItemId(itemStack.ItemID);
        result.Add(itemStack.Amount);
        EventDispatcher.Dispatch<EventKey.IteamChange>(new EventKey.IteamChange(itemStack.ItemID, result.Amount, itemStack.Amount));
    }

    public bool RemoveItem(ItemStack itemStack) {
        ItemStack result = GetItemSaveByItemId(itemStack.ItemID);
        if(result.Amount < itemStack.Amount) {
            return false;
        }
        result.Add(-itemStack.Amount);
        EventDispatcher.Dispatch<EventKey.IteamChange>(new EventKey.IteamChange(itemStack.ItemID, result.Amount, itemStack.Amount));
        return true;
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
        if(LevelMap>= DataManager.Instance.LevelMapMax) {
            LevelMap = DataManager.Instance.LevelMapMax;
            return false;
        }

        if(level+1 > LevelMap) {
            LevelMap = level+1;
            return true;
        }

        return false;
    }

    public bool Enought(ItemID itemID,int amout = 1) {
        ItemStack item = eventory.Find(x=>x.ItemID == itemID);
        if(item == null) {
            return false;
        }
        return item.Amount >= amout;
    }

    public void Update() {
        DailySave.GetUpDate();
    }
}
