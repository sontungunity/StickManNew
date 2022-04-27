using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyBase;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float speed;
    [SerializeField] private BeamRayCast beamFace;
    private void Update() {
        if(enemyBase.CurStatus == EnemyStatus.MOVE) {
            checkFace();
        } else {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
    }

    private void FixedUpdate() {
        if(enemyBase.CurStatus == EnemyStatus.MOVE) {
            rb2D.velocity = new Vector2((int)enemyBase.Display.right.x * speed, rb2D.velocity.y);
        }else if(enemyBase.CurStatus == EnemyStatus.DETECH) {
            rb2D.velocity = new Vector2((int)enemyBase.Display.right.x * speed * 4, rb2D.velocity.y);
        }
        
    }

    private void checkFace() {
        Collider2D collider2D = null;
        foreach(var col in beamFace.ArrayCollider2D) {
            if(col!=null) {
                collider2D = col;
            }
        }
        if(collider2D!=null) {
            enemyBase.Flip();
        }
    }
}
