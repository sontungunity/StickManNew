using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemStackView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI txt_Amount;
    private ItemStack itemStack;
    public ItemStack Model => itemStack;
    public void Show(ItemStack itemStack) {
        this.itemStack = itemStack;
        icon.sprite = itemStack.Data.Icon;
        txt_Amount.text = itemStack.Amount.ToString();
    }
}
