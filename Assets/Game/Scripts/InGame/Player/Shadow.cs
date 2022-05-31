using Spine.Unity;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float startTimeSpawn;

    public GameObject shadow;
    private float timeSpawn;
    private SkeletonAnimation _skeletonAnimation;
    private int maxChild = 10;

    [SerializeField] private Transform _player;
    

    private void Start()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    public void Update()
    {
        if (timeSpawn <= 0)
        {
            GameObject instance = Instantiate(shadow, _player.transform.position, Quaternion.identity);
            // _skeletonAnimation.AnimationName = transform.GetChild(0).GetComponent<SkeletonAnimation>().AnimationName;
            instance.GetComponent<SkeletonAnimation>().AnimationName = transform.GetChild(0).GetComponent<SkeletonAnimation>().AnimationName;
            instance.GetComponent<SkeletonAnimation>().timeScale = 0;
            instance.transform.localScale=new Vector3(transform.GetChild(0).rotation.y==0?1:-1,instance.transform.localScale.y,instance.transform.localScale.z);
            instance.GetComponent<SkeletonAnimation>().skeleton.A = Mathf.Lerp(0.3f, 0.7f, 0.2f);
            /*var a = transform.GetChild(0).GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0)
                .TrackTime;*/
            timeSpawn = startTimeSpawn;
            Destroy(instance, 1.5f);
        }
        else
        {
            timeSpawn -= Time.deltaTime;
        }
    }
}
