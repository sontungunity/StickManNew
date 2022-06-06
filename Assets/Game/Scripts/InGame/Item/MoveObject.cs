using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MoveObject : MonoBehaviour
{
    [SerializeField] private SimplePlatform obj;
    [SerializeField] private Transform pointStart,pointEnd;
    [SerializeField] private bool showEnd;
    [SerializeField] private float speed;
    private Sequence sequence;

    private void OnValidate() {
        if(showEnd) {
            obj.transform.position = pointEnd.position;
        } else {
            obj.transform.position = pointStart.position;
        }
    }

    private void Start() {
        obj.transform.position = pointStart.position;
        float distance = Vector2.Distance(pointStart.position,pointEnd.position);
        float timeMove = distance / speed;
        sequence = DOTween.Sequence();
        sequence.Append(obj.transform.DOMove(pointEnd.position, timeMove).SetEase(Ease.Linear));
        sequence.Append(obj.transform.DOMove(pointStart.position,timeMove).SetEase(Ease.Linear));
        sequence.SetLoops(-1);
    }


    private void OnDisable() {
        sequence.Kill();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pointStart.position, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointEnd.position, 0.5f);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(pointStart.position, pointEnd.position);
    }
}
