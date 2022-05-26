using STU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TriggerBoss : MonoBehaviour {
    [SerializeField] private GameObject displayBoss;
    [SerializeField] private GameObject displayBossDie;
    [SerializeField] private Collider2D col2D;
    private Tween tween;
    private void Start() {
        col2D.enabled = true;
        displayBoss.SetActive(false);
        displayBossDie.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision) {
        Player player = collision.transform.parent.GetComponent<Player>();
        if(player != null ) {
            if(player.transform.position.x > transform.position.x) {
                InGameManager.Instance.SetUpBoss(true);
                enabled = false;
                displayBoss.SetActive(true);
            }
        }
    }

    private void OnDisable() {
        tween.CheckKillTween();
    }
}
