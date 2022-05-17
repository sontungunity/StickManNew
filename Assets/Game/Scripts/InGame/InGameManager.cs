using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    [SerializeField] private Camera camera;
    [SerializeField] private Player player;
    public Camera Camera => camera;
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
    }

    private void SetUpMap() {
        if(Edit) {
            return;
        }
        var mapPref = DataManager.Instance.GetlevelMapByLevel(DataManager.Instance.PlayerData.LevelMap);
        LevelMap = Instantiate(mapPref, transform);
        LevelMap.transform.localPosition = Vector3.zero;
        EventDispatcher.Dispatch<EventKey.EnemyDie>(new EventKey.EnemyDie());
    }

    public void AddEnemyDie(int amount = 1) {
        EnemyKilled += amount;
        EventDispatcher.Dispatch<EventKey.EnemyDie>(new EventKey.EnemyDie());
    }

    public void FinishMap() {
        DataManager.Instance.PlayerData.GetLevelPass(LevelMap.Level);
        SceneManager.Instance.LoadSceneAsyn(SceneManager.SCENE_HOME);
    }

    public void Revived() {
        player.transform.position = PositionRevive;
        player.SetUpPlayer();
    }
}
