using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyBase;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float speedTarget = 8f;
    [SerializeField] private BeamRayCast beamFace;
    [SerializeField] private BeamRayCast beamDownward;
    private void Update() {
        if(enemyBase.CurStatus == EnemyStatus.MOVE) {
            CheckFace();
            CheckDownward();
        }
    }

    private void FixedUpdate() {
        if(enemyBase.CurStatus == EnemyStatus.MOVE) {
            rb2D.velocity = new Vector2((int)enemyBase.Display.right.x * speed, rb2D.velocity.y);
        }else if(enemyBase.CurStatus == EnemyStatus.DETECH) {
            rb2D.velocity = new Vector2((int)enemyBase.Display.right.x * speedTarget, rb2D.velocity.y);
        }
        
    }

    private void CheckFace() {
        Collider2D collider2D = null;
        foreach(var col in beamFace.ArrayCollider2D) {
            if(col!=null) {
                collider2D = col;
                break;
            }
        }

        if(collider2D!=null) {
            enemyBase.Flip();
            return;
        }
    }

    private void CheckDownward() {
        Collider2D collider2D = null;
        foreach(var col in beamDownward.ArrayCollider2D) {
            if(col != null) {
                collider2D = col;
                break;
            }
        }

        if(collider2D == null) {
            enemyBase.Flip();
            return;
        }
    }
}
