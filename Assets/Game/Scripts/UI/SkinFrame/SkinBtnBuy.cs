using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkinBtnBuy : MonoBehaviour {
    [SerializeField] private DisplayObjects disPlayBtn; //1.Use , 2.Price, 3.Lock
    [SerializeField] private Button btnUse,btnBuy;
    [SerializeField] private ItemStackView itemStackView;
    [SerializeField] private TextMeshProUGUI txtDetailLock;
    private SkinItemData dataSkin;
    private PlayerData player => DataManager.Instance.PlayerData;

    private void Start() {
        btnUse.onClick.AddListener(BtnUse);
        btnBuy.onClick.AddListener(BtnBuySkin);
    }

    public void Show(SkinItemData itemData) {
        this.dataSkin = itemData;
        SetUpShow();
    }

    private void SetUpShow() 
    {
        if(player.SkinID == dataSkin.ItemID) 
        {
            disPlayBtn.ActiveAll(false);
        } 
        else if(player.Enought(dataSkin.ItemID)) 
        {
            disPlayBtn.Active(0);
        } 
        else 
        {
            if(dataSkin.WayGetItem.Type == WayGetItem.PriceType.ITEMSTACK) 
            {
                disPlayBtn.Active(1);
                itemStackView.Show(dataSkin.WayGetItem.ItemStackDetail);
            } 
            else 
            {
                SetUpLock();
                if(dataSkin.WayGetItem.Type == WayGetItem.PriceType.LEVEL) 
                {
                    if(player.LevelMap > dataSkin.WayGetItem.LevelDetail) 
                    {
                        player.AddItem(new ItemStack(dataSkin.ItemID, 1));
                        SetUpHave();
                        return;
                    } 
                    else 
                    {
                        txtDetailLock.text = $"UnLock at Level {dataSkin.WayGetItem.LevelDetail}";
                    }
                } 
                else if(dataSkin.WayGetItem.Type == WayGetItem.PriceType.SPECIAL) 
                {
                    txtDetailLock.text = $"Get at Daily, Gifts & Shop";
                } 
                else if(dataSkin.WayGetItem.Type == WayGetItem.PriceType.PRESTIGE) 
                {
                    txtDetailLock.text = $"PRESTIGE";
                } 
                else 
                {
                    txtDetailLock.text = $"LOCK";
                }
            }
        }
    }

    private void SetupUsed() {
        disPlayBtn.ActiveAll(false);
    }

    private void SetUpHave() {
        disPlayBtn.Active(0);
    }

    private void SetUpLockItemStack() {
        disPlayBtn.Active(1);
    }

    private void SetUpLock() {
        disPlayBtn.Active(2);
    }

    private void BtnUse() {
        player.SetSkin(dataSkin.ItemID);
    }

    private void BtnBuySkin() {
        if(player.RemoveItem(dataSkin.WayGetItem.ItemStackDetail)) {
            player.AddItem(new ItemStack(dataSkin.ItemID, 1));
            disPlayBtn.Active(1);
            SetupUsed();
        } else {
            TextNotify.Instance.Show("Not Enough");
        }
    }

}
