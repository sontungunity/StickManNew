using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    [SerializeField] private Player player;
    [Header("GetByCode")]
    public bool Edit;
    public LevelMap LevelMap;
    private void Start() {
        SetUpMap();
        player.SetUpPlayer();
    }

    private void SetUpMap() {
        if(Edit) {
            return;
        }
        var mapPref = DataManager.Instance.GetlevelMapByLevel(0);
        LevelMap = Instantiate(mapPref, transform);
        LevelMap.transform.localPosition = Vector3.zero;
    }
}
