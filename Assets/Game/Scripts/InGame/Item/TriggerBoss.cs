using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TriggerBoss : MonoBehaviour {
    [SerializeField] private GameObject displayBoss;
    [SerializeField] private GameObject disabe;
    [SerializeField] private BoxCollider2D col2D;
    [SerializeField] private Rect rect;
    private void Start() {
        col2D.enabled = true;
        if(displayBoss != null) {
            displayBoss.SetActive(false);
        }
        if(disabe != null) {
            disabe?.SetActive(true);
        }
    }

    private void OnEnable() {
        EventDispatcher.AddListener<EventKey.EnemyDie>(HalderEnemyDie);
    }

    private void OnDisable() {
        EventDispatcher.AddListener<EventKey.EnemyDie>(HalderEnemyDie);
    }

    private void HalderEnemyDie(EventKey.EnemyDie evt) {
        if(evt.enemyDie as BossBase) {
            if(disabe!=null) {
                disabe?.SetActive(false);
            }
            InGameManager.Instance.SetUpBoss(false);
            ProcameraController.Instance.SetUpDefault();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        Player player = collision.transform.parent.GetComponent<Player>();
        if(player != null) {
            if(player.transform.position.x > transform.position.x) {
                col2D.enabled = false;
                if(displayBoss!=null) {
                    displayBoss?.SetActive(true);
                }
                InGameManager.Instance.SetUpBoss(true);
                ProcameraController.Instance.AddRoom(rect);
                ProcameraController.Instance.SetOrthographic(10f);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawCube(transform.position, col2D.size);
    }

    [ContextMenu("SetUpSupportCollider")]
    public void SetUpSupportCollider() {
        if(displayBoss != null) {
            SetUpGameObject(displayBoss);
        }
        if(disabe != null) {
            SetUpGameObject(disabe);
        }
    }

    private void SetUpGameObject(GameObject obj) {
        var img = obj.GetComponent<SpriteRenderer>();
        var col = obj.GetComponent<BoxCollider2D>();
        col.size = img.size;
        obj.SetActive(false);
    }
}
