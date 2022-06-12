using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigCoinCS : MonoBehaviour
{
    [SerializeField] private int minCoin = 300,maxCoin = 451;

    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.transform.parent.GetComponent<Player>();
        if(player != null) {
            if(DataManager.Instance != null) {
                int random = Random.Range(minCoin,maxCoin);
                DataManager.Instance.PlayerData.AddItem(new ItemStack(ItemID.COIN, random));
                SpawnerTextDame.Instance.Spawner(player.transform.position, $"+{random}");
                gameObject.SetActive(false);
            }
        }
    }
}
