using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    [SerializeField] private Collider2D col2D;
    [SerializeField] private int layer;
    private void Awake() {
        col2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == layer) {
            col2D.isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player!= null) {
            if(DataManager.Instance != null) 
            {
                SoundManager.Instance.PlaySoundGoldCollect();
                int random = Random.Range(2,10);
                InGameManager.Instance.CoinInGame += random;
                SpawnerTextDame.Instance.Spawner(player.transform.position,$"+{random}");
                col2D.isTrigger = true;
                this.Recycle();
            }
            return;
        }

        LavaCS lava = collision.gameObject.GetComponent<LavaCS>();
        if(lava != null) {
            col2D.isTrigger = true;
            this.Recycle();
        }
    }
}
