using UnityEngine;
using UnityEngine.UI;

public class GetItemBtn : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Text text;
    [SerializeField] private NarbarManager _narbarManager;
    [SerializeField] private SkinView _skinView;
    [SerializeField] private SkinItemData _skinItemData;

    private void Start()
    {
        btn.onClick.AddListener(() => ClickBtn(_skinView));
    }

    public void initprice(SkinItemData itemData)                  // Normal Skin
    {
        text.text = itemData.WayGetItem.ItemStackDetail.Amount.ToString();
        _skinItemData = itemData;
    }        

    public void InitAds(SkinItemData itemData)                    // Free Skin
    {
        _skinItemData = itemData;
        if (DataManager.Instance.PlayerData.LevelPlayer >= itemData.WayGetItem.LevelDetail)
        {
            text.text = "WATCH ADS TO UNLOCK";
            text.fontSize = 35;
        }
        else
        {
            text.text = "UNLOCK AT LEVEL " + itemData.WayGetItem.LevelDetail;
            text.fontSize = 40;
        }
    }

    public void InitWayGetSkin()                                 // Special Skin
    {
        text.text = "UNLOCK IN MYSTERY CARD";
        text.fontSize = 35;
    }

    public void InitUseSkinBtn()
    {
        text.text = "USE";
    }

    public void ClickBtn(SkinView skinview)
    {
        _skinView = skinview;
        // trigger when user click buy.
        switch (_narbarManager.CurIndex)
        {
            case 0:
                _skinItemData.WayGetItem.Type = WayGetItem.PriceType.ITEMSTACK;
                int curCoin = DataManager.Instance.PlayerData.GetItemSaveByItemId(ItemID.COIN).Amount;
                int skinPrice = _skinItemData.WayGetItem.ItemStackDetail.Amount;
                if (curCoin >= skinPrice)
                {
                    _skinView = skinview;
                    _skinView.SetUpUnLock();
                    Debug.Log("purchase!");
                    // set skin usable
                }
                else
                {
                    Debug.Log("Not enough coin");
                    // go to GoldPack shop
                }
                break;
            
            case 1:
                _skinItemData.WayGetItem.Type = WayGetItem.PriceType.LEVEL;
                int curLevel = DataManager.Instance.PlayerData.LevelPlayer;
                int levelDemand = _skinItemData.WayGetItem.LevelDetail;
                if (curLevel >= levelDemand)
                {
                    _skinView = skinview;
                    _skinView.SetUpUnLock();
                    Debug.Log("UNLOCK SKIN");
                }
                else
                {
                    Debug.Log("NOT ENOUGH LEVEL");
                }
                break;
            
            case 2:
                _skinItemData.WayGetItem.Type = WayGetItem.PriceType.SPECIAL;
                // click will select use this Skin.
                break;
            
            case 3:
                _skinItemData.WayGetItem.Type = WayGetItem.PriceType.SPIN;
                // click will select use this Skin.
                break;
        }
    }
    
    //tung
}
