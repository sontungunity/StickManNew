using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorCS : MonoBehaviour
{
    [SerializeField] private Collider2D col2D;
    private bool open;
    private Tween tween;
    private void Start() {
        open = false;
    }

    private void OnEnable() {
        EventDispatcher.AddListener<EventKey.EnemyDie>(HalderEvent);
    }

    private void OnDisable() {
        EventDispatcher.RemoveListener<EventKey.EnemyDie>(HalderEvent);
    }

    private void HalderEvent(EventKey.EnemyDie evt) {
        if(!open) {
            col2D.enabled = false;
            tween = DOVirtual.DelayedCall(0.5f, () => {
                open = InGameManager.Instance.KillAllEnemy;
                col2D.enabled = true;
            });
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(open) {
            Player player = collision.GetComponent<Player>();
            if(player!=null) {
                player.SetPlayerStatusCheckRank(EnumPlayerStatus.WIN,()=> {
                    InGameManager.Instance.FinishMap();
                });
            }
        }
    }
}
