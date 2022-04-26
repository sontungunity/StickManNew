using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int amountEnemy;
    public int Level => level;
    public int AmountEnemy => amountEnemy;
}
