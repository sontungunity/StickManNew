using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using STU;
using UnityEngine.UI;

public class LifeAndHeart : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_NumberLife;
    [SerializeField] private BarpercentUI barpercent;
    [SerializeField] private TextMeshProUGUI txt_EnemyKiller,txt_Damage;
    [SerializeField] private Image icon_Weapon;
    private PlayerData playerdata => DataManager.Instance.PlayerData;
    private Player player => InGameManager.Instance.Player;
    private LevelMap levelMap => InGameManager.Instance.LevelMap;

    private void Start() {
        txt_NumberLife.text = playerdata.Life.ToString();
        txt_EnemyKiller.text = InGameManager.Instance.EnemyKilled + " /" + levelMap.NumberEnemy;
    }

    private void OnEnable() {
        EventDispatcher.AddListener<EventKey.PlayerChange>(HalderPlayerChange);
        EventDispatcher.AddListener<EventKey.EnemyDie>(HalderEnemyDie);
    }

    private void OnDisable() {
        EventDispatcher.RemoveListener<EventKey.PlayerChange>(HalderPlayerChange);
        EventDispatcher.RemoveListener<EventKey.EnemyDie>(HalderEnemyDie);
    }

    private void HalderPlayerChange(EventKey.PlayerChange evt) {
        barpercent.Show(player.curHeart /(float) player.OriginHeart);
        txt_Damage.text = player.Dame.ToString();
        icon_Weapon.overrideSprite = player.Weapon != null ? player.Weapon.Icon : null;
    }

    private void HalderEnemyDie(EventKey.EnemyDie evt) {
        txt_EnemyKiller.text = $"{InGameManager.Instance.EnemyKilled}/{levelMap.NumberEnemy}";
    }
}
