using Spine.Unity;
using UnityEngine;

public class PackFrames : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic skeGraphic1;
    [SerializeField] private SkeletonGraphic skeGraphic2;
    [SerializeField] private PriceBtn priceBtn;

    private SkinItemData skinItemData;

    public void ShowPack0()
    {
        var skinData1 = DataManager.Instance.GetItemDataByID(ItemID.SKIN_33) as SkinItemData;
        var skinData2 = DataManager.Instance.GetItemDataByID(ItemID.SKIN_32) as SkinItemData;
        skeGraphic1.Skeleton.SetSkin(skinData1.NameSpine);
        skeGraphic2.Skeleton.SetSkin(skinData2.NameSpine);
        priceBtn.PriceText();
    }

    public void ShowPack1()
    {
        var skinData1 = DataManager.Instance.GetItemDataByID(ItemID.SKIN_13) as SkinItemData;
        skeGraphic1.Skeleton.SetSkin(skinData1.NameSpine);
        priceBtn.PriceText();
    }
    public void ShowPack2()
    {
        priceBtn.PriceText();
    }
    public void ShowPack3()
    {
        var skinData1 = DataManager.Instance.GetItemDataByID(ItemID.SKIN_28) as SkinItemData;
        skeGraphic1.Skeleton.SetSkin(skinData1.NameSpine);
        priceBtn.PriceText();
    }
}
