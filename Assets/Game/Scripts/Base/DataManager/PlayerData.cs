using STU;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData {
    public int LevelPlayer = 0;
    public int LevelMap = 0;
    public List<ItemStack> eventory = new List<ItemStack>(){new ItemStack(ItemID.COIN,300),new ItemStack(ItemID.LUCKYWEEL,1),new ItemStack(ItemID.ITEMBOW,1),new ItemStack(ItemID.ITEMSWORD,1), new ItemStack(ItemID.LIFE,5)};
    public SpinSave SpinSave = new SpinSave();
    public DailySave DailySave = new DailySave();
    public GiftsSave GiftsSave = new GiftsSave();
    public ItemID SkinID = ItemID.SKIN_00;

    public void AddItem(List<ItemStack> lstItemStack) {
        foreach(var itemStack in lstItemStack) {
            AddItem(itemStack);
        }
    }

    public void AddItem(IEnumerable<ItemStack> itemStacks) {
        foreach(var itemStack in itemStacks) {
            AddItem(itemStack);
        }
    }

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
        if(LevelMap >= DataManager.Instance.LevelMapMax) {
            LevelMap = DataManager.Instance.LevelMapMax;
            return false;
        }

        if(level + 1 > LevelMap) {
            LevelMap = level + 1;
            return true;
        }

        return false;
    }

    public bool Enought(ItemID itemID, int amout = 1) {
        ItemStack item = eventory.Find(x=>x.ItemID == itemID);
        if(item == null) {
            return false;
        }
        return item.Amount >= amout;
    }

    public void Update() {
        DailySave.GetUpDate();
    }

    public void SetSkin(ItemID itemID) {
        var itemData = itemID.GetDataByID();
        if(itemData as SkinItemData) {
            SkinID = itemID;
            EventDispatcher.Dispatch<EventKey.EventSkinChange>(new EventKey.EventSkinChange());
        }
    }
}
