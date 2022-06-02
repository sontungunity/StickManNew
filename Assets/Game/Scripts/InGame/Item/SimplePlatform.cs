using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SimplePlatform : MonoBehaviour {
    [SerializeField] private Rigidbody2D rg2D;
    [SerializeField]private Player player;

    private void OnCollisionEnter2D(Collision2D collision) {
        var player = collision.transform.GetComponent<Player>();
        if(player != null) {
            player.transform.parent = transform;
            this.player = player;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(player != null) {
            var player = collision.transform.GetComponent<Player>();
            if(player != null) {
                player.transform.parent = InGameManager.Instance.LevelMap.transform;
            }
        }
    }
}
