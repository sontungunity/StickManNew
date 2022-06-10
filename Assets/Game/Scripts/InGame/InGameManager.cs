using Com.LuisPedroFonseca.ProCamera2D;
using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    [SerializeField] private Player player;
    [SerializeField] private AudioClip musicInGame;
    public Player Player => player;
    [Header("Edit")]
    public bool Edit;
    [Header("GetByCode")]
    public LevelMap LevelMap;
    public int EnemyKilled;
    public Vector3 PositionRevive;
    public int CoinInGame = 0;
    public bool KillAllEnemy => EnemyKilled >= LevelMap.NumberEnemy;

    private void Start() {
        SetupNewGame();
        SoundManager.Instance.PlayMusic(musicInGame);
    }

    public void SetupNewGame() {
        PositionRevive = Vector3.zero;
        EnemyKilled = 0;
        CoinInGame = 0;
        player.transform.position = PositionRevive;
        player.SetUpPlayer();
        if(LevelMap != null && !Edit) {
            Destroy(LevelMap.gameObject);
        }
        SetUpMap();
        EventDispatcher.Dispatch<EventKey.EventSetupNewGame>(new EventKey.EventSetupNewGame());
    }

    private void SetUpMap() {
        if(Edit) {
            return;
        }
        var mapPref = DataManager.Instance.GetlevelMapByLevel(GameManager.Instance.CurLevel);
        LevelMap = Instantiate(mapPref, transform);
        LevelMap.transform.localPosition = Vector3.zero;
    }

    public void AddEnemyDie(EnemyBase enemy) {
        EnemyKilled += 1;
        EventDispatcher.Dispatch<EventKey.EnemyDie>(new EventKey.EnemyDie(enemy));
    }

    public void FinishMap() {
        DataManager.Instance.PlayerData.GetLevelPass(LevelMap.Level);
        GameManager.Instance.CurLevel = DataManager.Instance.PlayerData.LevelMap;
        var WinFrame =  FrameManager.Instance.Push<WinFrame>(onCompleted:()=> {
            ItemStack rewardInGame = new ItemStack(ItemID.COIN, InGameManager.Instance.CoinInGame);
            CollectionController.Instance.GetItemStack(rewardInGame, new Vector2(Screen.width/2,Screen.height/2), () => {
                DataManager.Instance.PlayerData.AddItem(rewardInGame);
            });
        });
        
    }

    public void Revived() {
        player.transform.position = PositionRevive;
        player.SetUpPlayer();
    }

    public void SetUpBoss(bool onBossArea = false) {
        var frameGame = FrameManager.Instance.GetFrame<GameFrame>();
        if(onBossArea) {
            frameGame.BossBarHeart.StartActive(true);
            frameGame.RoomSetting.StartActive(false);
            //frameGame.RoomSetting.SetUpOrtho(10f);
        } else {
            frameGame.BossBarHeart.StartActive(false);
            frameGame.RoomSetting.StartActive(true);
        }
    }
}
