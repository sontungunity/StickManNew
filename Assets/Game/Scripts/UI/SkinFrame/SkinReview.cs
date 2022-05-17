using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinReview : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic graphic;
    private SkinItemData skinData;
    public void Show(ItemID itemID) {
        skinData = DataManager.Instance.GetItemDataByID(itemID) as SkinItemData;
        graphic.Skeleton.SetSkin(skinData.NameSpine);
    }
}
