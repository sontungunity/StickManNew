using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMove : MonoBehaviour
{
    [SerializeField] private Transform leftTop, leftBot, mid, rightTop, rightBot;

    public void Blink(Transform pos)
    {
        var b = transform.position == pos.transform.position;
    }
}
