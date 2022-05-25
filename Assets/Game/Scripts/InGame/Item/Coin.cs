using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip audioCoin;
    private void OnCollisionEnter2D(Collision2D collision) {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player!= null) {
            if(DataManager.Instance != null) {
                int random = Random.Range(1,5);
                //DataManager.Instance.PlayerData.AddItem(new ItemStack(ItemID.COIN,1));
                InGameManager.Instance.CoinInGame += random;
                SpawnerTextDame.Instance.Spawner(player.transform.position,$"+{random}");
                SoundManager.Instance.PlaySound(audioCoin);
                this.Recycle();
            }
        }
    }
}
