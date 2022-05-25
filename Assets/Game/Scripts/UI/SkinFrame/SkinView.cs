using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinView : MonoBehaviour {
    [Header("Data")]
    [SerializeField] private SkinItemData model;
    [Space]
    [SerializeField] private Button btn_Select;
    [Header("Displaye")]
    [SerializeField] private GameObject main;
    [SerializeField] private Image icon;
    [SerializeField] private DisplayObjects bgDisplay; // 0.Using, 1.Lock, 2.Unlock
    [SerializeField] private Image iconLock;

    public SkinItemData Model => model;
    private Action<SkinView> _actionSelect;
    
    private void Awake() 
    {
        btn_Select.onClick.AddListener(OnSelect);
    }

    public void Show(SkinItemData skinData) 
    {
        this.model = skinData;
        if(skinData == null) {
            main.SetActive(false);
            return;
        }
        main.SetActive(true);
        icon.sprite = model.Icon;
        if(model.ItemID == DataManager.Instance.PlayerData.SkinID) {
            SetUpUsing();
        }else if(DataManager.Instance.PlayerData.Enought(model.ItemID)) {
            SetUpUnLock();
        } else {
            SetUpLock();
        }
    }

    private void SetUpUsing() {
        bgDisplay.Active(0);
        iconLock.gameObject.SetActive(false);
    }

    private void SetUpUnLock() {
        bgDisplay.Active(2);
        iconLock.gameObject.SetActive(false);
    }

    private void SetUpLock() {
        bgDisplay.Active(1);
        iconLock.gameObject.SetActive(true);
    }
    
    public void SetOnSelect(Action<SkinView> onSelect) {
        this._actionSelect = onSelect;
    }

    private void OnSelect() {
        _actionSelect?.Invoke(this);
    }
}
