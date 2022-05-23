using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpikeCS : MonoBehaviour {
    //[SerializeField] private Transform display;
    [SerializeField] private BoxCollider2D  boxCollider;
    [SerializeField] private float timeUp = 0.5f,timeDown = 1f,timeDelay = 2f;
    private Sequence mySequence;
    private void Start() {
        float yStart = -boxCollider.size.y;
        transform.localPosition = new Vector3(0, yStart, 0);
        mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOLocalMoveY(0, timeUp).SetEase(Ease.OutExpo));
        mySequence.Append(transform.DOLocalMoveY(yStart, timeDown).SetEase(Ease.Linear));
        mySequence.AppendInterval(timeDelay);
        mySequence.SetLoops(-1);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.transform.parent.GetComponent<Player>();
        if(player != null) {
            player.GetDame(player.curHeart);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Player player = collision.transform.parent.GetComponent<Player>();
        if(player != null) {
            player.GetDame(player.curHeart);
        }
    }

    private void OnDisable() {
        mySequence.Kill();
    }
}
