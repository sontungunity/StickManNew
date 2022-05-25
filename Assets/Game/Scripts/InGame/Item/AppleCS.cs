using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCS : MonoBehaviour
{
    [SerializeField] private float percent = 0.25f;
    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.transform.parent.GetComponent<Player>();
        if(player!=null) {
            player.Healing(percent);
            this.gameObject.SetActive(false);
        }
    }
}
