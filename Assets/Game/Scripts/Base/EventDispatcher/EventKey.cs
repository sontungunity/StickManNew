
using STU;

public static class EventKey {
    public struct LoadFinal : IEventArgs {

    }

    public struct SceneChange : IEventArgs {
        public string nameScene;

        public SceneChange(string nameScene) {
            this.nameScene = nameScene;
        }
    }

    public struct IteamChange : IEventArgs {
        public ItemID itemID;
        public int curAmount;
        public int changeAmount;

        public IteamChange(ItemID itemID, int curAmount, int changeAmount) {
            this.itemID = itemID;
            this.curAmount = curAmount;
            this.changeAmount = changeAmount;
        }
    }

    public struct EnemyDie : IEventArgs {

    }

    public struct PlayerChange : IEventArgs {

    }

    public struct EnterBossArea : IEventArgs {

    }

    public struct BossGetDame : IEventArgs {

    }

    public struct SpinChange : IEventArgs {
        public bool GetNew;

        public SpinChange(bool getNew) {
            this.GetNew = getNew;
        }
    }

    public struct EventSkinChange : IEventArgs {
        public ItemID IDBefor;
        public ItemID IDAfter;

        public EventSkinChange(ItemID idBefor, ItemID idAfter) {
            this.IDBefor = idBefor;
            this.IDAfter = idAfter;
        }
    }
}
