using UnityEngine;

public class SwordCS : MonoBehaviour
{
    [SerializeField] private WeaponID weaponID;
    [SerializeField] private SpriteRenderer img;
    [SerializeField] private GameObject iconAds;
    [Header("Customer")] 
    [SerializeField] private bool noAds;

    private void Start()
    {
        WeaponData data = weaponID.GetDataWeaponByID();
        img.sprite = data.Icon;
        iconAds.SetActive(!noAds);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.transform.parent.GetComponent<Player>();
        if(player != null) 
        {
            if(noAds) {
                player.SetWeapon(weaponID.GetDataWeaponByID());
                gameObject.SetActive(false);
                return;
            }
            AdsManager.Instance.ShowRewarded((value)=> {
                if(value) {
                    player.SetWeapon(weaponID.GetDataWeaponByID());
                    gameObject.SetActive(false);
                }
            });
        }
    }
}
