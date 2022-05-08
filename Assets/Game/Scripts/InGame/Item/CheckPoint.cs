using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject fire;

    private void Start() {
        fire.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!fire.activeInHierarchy) {
            Player player = collision.GetComponent<Player>();
            if(player!=null) {
                fire.SetActive(true);
                InGameManager.Instance.PositionRevive = transform.position;
            }
        }
    }
}
