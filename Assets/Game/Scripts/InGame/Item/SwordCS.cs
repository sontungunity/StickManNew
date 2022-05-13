using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCS : MonoBehaviour
{
    [SerializeField] private WeaponID weaponID;
    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.GetComponent<Player>();
        if(player != null) {
            AdsManager.Instance.ShowRewarded((value)=> {
                if(value) {
                    player.SetWeapon(weaponID);
                    gameObject.SetActive(false);
                }
            });
        }
    }
}
