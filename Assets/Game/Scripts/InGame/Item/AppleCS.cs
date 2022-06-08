using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCS : MonoBehaviour
{
    [SerializeField] private float percent = 0.25f;
    [SerializeField] private GameObject itemBoost;
    [SerializeField] private ParticleSystem par;
    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.transform.parent.GetComponent<Player>();
        if(player!=null) {
            player.Healing(percent);

            var play = par.Spawn();
            play.transform.position = this.transform.position;
            play.Play();
            
            itemBoost.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
