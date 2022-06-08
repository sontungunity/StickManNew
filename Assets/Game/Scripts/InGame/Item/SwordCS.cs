using UnityEngine;

public class SwordCS : MonoBehaviour
{
    [SerializeField] private WeaponID weaponID;
    [SerializeField] private SpriteRenderer img;
    [SerializeField] private ParticleSystem par;

    private void Start()
    {
        WeaponData data = weaponID.GetDataWeaponByID();
        img.sprite = data.Icon;
        this.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        // transform.rotation = new Quaternion(0, 0, -40, 0);
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
                    gameObject.SetActive(false);
                }
            });
        }
    }
}
