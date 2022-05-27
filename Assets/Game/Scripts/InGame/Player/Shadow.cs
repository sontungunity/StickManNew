using System;
using Spine.Unity;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private float timeSpawn;
    public float startTimeSpawn;

    public GameObject shadow;
    [SerializeField] private Transform _player;
    // private PlayerHorizontal _playerHorizontal;

    // private void Start()
    // {
    //     _playerHorizontal = GetComponent<PlayerHorizontal>();
    // }

    public void Update()
    {
        // if (_playerHorizontal._isMoving)
        // {
            if (timeSpawn <= 0)
            {
                // var transformLocalScale = shadow.transform.localScale;
                // transformLocalScale.x = dir;
                GameObject instance = Instantiate(shadow, _player.transform.position, Quaternion.identity);
                
                instance.GetComponent<SkeletonAnimation>().AnimationName =
                    transform.GetChild(0).GetComponent<SkeletonAnimation>().AnimationName;
                instance.GetComponent<SkeletonAnimation>().timeScale = 0;
                instance.transform.localScale=new Vector3(transform.GetChild(0).rotation.y==0?1:-1,instance.transform.localScale.y,instance.transform.localScale.z);
                instance.GetComponent<SkeletonAnimation>().skeleton.A = Mathf.Lerp(0.3f, 0.7f, 0.2f);
                timeSpawn = startTimeSpawn;
                Destroy(instance, 1.5f);
            }
            else
            {
                timeSpawn -= Time.deltaTime;
            }
        // }
    }
}
