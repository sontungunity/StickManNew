using UnityEngine;

public class SwordCS : MonoBehaviour
{
    [SerializeField] private WeaponID weaponID;
    // [SerializeField] private SpriteRenderer img;
    [SerializeField] private ParticleSystem par;
    [SerializeField] private GameObject _itemBoost;

    private void Start()
    {
        WeaponData data = weaponID.GetDataWeaponByID();
        // img.sprite = data.Icon;
        // transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.transform.parent.GetComponent<Player>();
        if(player != null) 
        {
            var spawnPar = par.Spawn();
            spawnPar.transform.position = this.transform.position;
            spawnPar.Play();

            AdsManager.Instance.ShowRewarded((value)=> {
                if(value) {
                    player.SetWeapon(weaponID.GetDataWeaponByID());
                    //_itemBoost.SetActive(false);
                    gameObject.SetActive(false);
                }
            });
        }
    }
}
