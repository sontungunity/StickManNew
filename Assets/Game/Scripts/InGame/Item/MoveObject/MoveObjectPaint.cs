using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectPaint : MonoBehaviour
{
    [SerializeField] private int index;
    private void OnDrawGizmosSelected() {
        Gizmos.color = index.GetColorByInt();
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
