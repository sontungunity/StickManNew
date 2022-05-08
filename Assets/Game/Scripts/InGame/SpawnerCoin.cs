using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCoin : Singleton<SpawnerCoin>
{
    [SerializeField] private Vector2 force;
    [SerializeField] private Coin coinPref;
    public void Spawner(Vector2 postion,int amount) {
        for(int i = 0; i < amount; i++) {
            var coin = coinPref.Spawn(InGameManager.Instance.LevelMap.transform);
            coin.transform.position = postion;
            float x = Random.Range(-force.x,force.x);
            Vector2 newForce = new Vector2(x,force.y);
            coin.transform.GetComponent<Rigidbody2D>().AddForce(newForce,ForceMode2D.Impulse);
        }
    }

    [ContextMenu("TestSpawnCoin")]
    public void TestSpawnCoin() {
        Spawner(InGameManager.Instance.Player.transform.position + Vector3.up*3f,5);
    }
}
