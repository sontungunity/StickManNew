using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player!= null) {
            if(DataManager.Instance != null) 
            {
                SoundManager.Instance.PlaySoundGoldCollect();
                int random = Random.Range(1,5);
                //DataManager.Instance.PlayerData.AddItem(new ItemStack(ItemID.COIN,1));
                InGameManager.Instance.CoinInGame += random;
                SpawnerTextDame.Instance.Spawner(player.transform.position,$"+{random}");
                this.Recycle();
            }
        }
    }
}
