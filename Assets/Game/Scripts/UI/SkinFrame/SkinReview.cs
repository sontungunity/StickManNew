using System;
using Spine.Unity;
using UnityEngine;

public class SkinReview : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic graphic;
    [SerializeField] private GetItemBtn priceShow;
    [SerializeField] private NarbarManager _narbarManager;
    
    private SkinItemData skinData;

   

    public void Show(ItemID itemID) {
        skinData = DataManager.Instance.GetItemDataByID(itemID) as SkinItemData;
        graphic.Skeleton.SetSkin(skinData.NameSpine);

        switch (_narbarManager.CurIndex)
        {
            case 0:
                priceShow.initprice(skinData);
                break;
            case 1:
                priceShow.InitAds(skinData);
                break;
            case 2:
                priceShow.InitWayGetSkin();
                break;
            case 3:
                priceShow.initprice(skinData);
                break;
        }
    }
}
