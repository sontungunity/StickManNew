using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeAndHeart : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_NumberLife;
    [SerializeField] private Barpercent barpercent;
    private PlayerData playerdata => DataManager.Instance.PlayerData;
    public void Init() {
        txt_NumberLife.text = playerdata.life.ToString();
        barpercent.Init();
    }
}
