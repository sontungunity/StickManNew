using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int numberEnemy;
    public int Level => level;
    public int NumberEnemy => numberEnemy;

    [ContextMenu("SetUpInfoMap")]
    public void SetUpInFoMap() {
        numberEnemy = transform.GetComponentsInChildren<EnemyBase>().Length;
    }
}
