using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhastBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rg2D;
    private Vector2 direction;
    private float speed;
    private int dame;
    public void Fire(Vector2 direction, float speed, int dame) {
        this.direction = direction;
        this.speed = speed;
        this.dame = dame;
        transform.right = direction;
    }

    private void FixedUpdate() {
        rg2D.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.parent.GetComponent<BossBase>()) {
            return;
        }
        Player player = collision.transform.parent.GetComponent<Player>();
        if(player!=null) {
            player.GetDame(dame);
        }
        this.Recycle();
    }
}
