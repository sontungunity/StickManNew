using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinItemData", menuName = "Game/SkinItemData")]
public class SkinItemData : ItemData
{
    [SerializeField] private string nameSpine;
    [SerializeField] private WayGetItem wayGetItem;
    public WayGetItem WayGetItem => wayGetItem;
    public string NameSpine => nameSpine;
}
