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
    private void FixedUpdate() {
        if(enemyBase.CurStatus != EnemyStatus.MOVE && enemyBase.CurStatus != EnemyStatus.DETECH) {
            return;
        }

        if(!CheckDownWardCanMove() || !CheckFaceCanMove()) {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
            if(enemyBase.CurStatus == EnemyStatus.MOVE) {
                enemyBase.Flip();
            }
            return;
        }

        if(enemyBase.CurStatus == EnemyStatus.MOVE) {
            rb2D.velocity = new Vector2((int)enemyBase.Display.right.x * speed, rb2D.velocity.y);
        }else if(enemyBase.CurStatus == EnemyStatus.DETECH) {
            rb2D.velocity = new Vector2((int)enemyBase.Display.right.x * speedTarget, rb2D.velocity.y);
        }
    }

    private bool CheckDownWardCanMove() {
        Collider2D collider2D = null;
        foreach(var col in beamDownward.ArrayCollider2D) {
            if(col != null) {
                collider2D = col;
                break;
            }
        }
        return collider2D != null;
    }

    private bool CheckFaceCanMove() {
        Collider2D collider2D = null;
        foreach(var col in beamFace.ArrayCollider2D) {
            if(col != null) {
                collider2D = col;
                break;
            }
        }

        return collider2D == null;
    }
}
