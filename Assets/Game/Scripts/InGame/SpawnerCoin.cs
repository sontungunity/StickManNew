using STU;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCoin : Singleton<SpawnerCoin> {
    [SerializeField] private Vector2 force;
    [SerializeField] private Coin coinPref;
    [SerializeField] private float timeDelay = 0.2f;
    private WaitForSeconds waitDelay;
    public void Start() {
        waitDelay = new WaitForSeconds(timeDelay);
    }

    public void Spawner(Vector2 postion, int amount) {
        for(int i = 0; i < amount; i++) {
            var coin = coinPref.Spawn(InGameManager.Instance.LevelMap.transform);
            coin.transform.position = postion;
            float x = UnityEngine.Random.Range(-force.x,force.x);
            Vector2 newForce = new Vector2(x,force.y);
            coin.transform.GetComponent<Rigidbody2D>().AddForce(newForce, ForceMode2D.Impulse);
        }
    }

    public void SpawnerII(Vector2 postion, int amount, Action callback = null) {
        StartCoroutine(IESpawnerII(postion,amount,callback));
    }

    IEnumerator IESpawnerII(Vector2 postion, int amount,Action callback = null) {
        for(int i = 0; i < amount; i++) {
            var coin = coinPref.Spawn(InGameManager.Instance.LevelMap.transform);
            coin.transform.position = postion;
            float x = UnityEngine.Random.Range(-force.x,force.x);
            Vector2 newForce = new Vector2(x,force.y);
            coin.transform.GetComponent<Rigidbody2D>().AddForce(newForce, ForceMode2D.Impulse);
            yield return waitDelay;
        }
    }

    [ContextMenu("TestSpawnCoin")]
    public void TestSpawnCoin() {
        SpawnerII(InGameManager.Instance.Player.transform.position + Vector3.up * 3f, 5);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Q)) {
            SpawnerII(InGameManager.Instance.Player.transform.position + Vector3.up * 3f, 5);
        }
    }
}
