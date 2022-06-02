using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriceBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void PriceText()
    {
        text.text = "Price";
    }
}
