using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCS : MonoBehaviour
{
    [SerializeField] private SpineBase spine;
    [SerializeField,SpineAnimation] private string animIdle,animOpen,animOpenIdle;
    [SerializeField] private Transform point;
    public bool open;
    private void Start() {
        spine.SetAnim(0, animIdle, true);
        open = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(open) {
            return;
        }
        Player player = collision.transform.parent.GetComponent<Player>();
        if(player != null) {
            open = true;
            spine.SetAnim(0,animOpen,false,()=> {
                SpawnerCoin.Instance.SpawnerII(point.position,10);
                spine.SetAnim(0, animOpenIdle, true);
            });
        }
    }
}
