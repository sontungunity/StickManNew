using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhastMove : EnemyMove
{
    [SerializeField] private float hight = 5;
    private float hightTarget;

    private void Start() {
        hightTarget = transform.position.y + 5;
    }
    public void StartMove() {

    }

    protected override void FixedUpdate() {
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
        } else if(enemyBase.CurStatus == EnemyStatus.DETECH) {
            rb2D.velocity = new Vector2((int)enemyBase.Display.right.x * speedTarget, rb2D.velocity.y);
        }
    }
}
