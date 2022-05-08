using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    public static float TIME_DELAY_KEY = 0.5f;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private BoxCollider2D col2D;
    public Rigidbody2D Rb2D => rb2D;
    [Header("InfoCollider")]
    [SerializeField] private PlayerTourch playerTourch;
    [SerializeField] private OverlapBoxInfo boxGroundInfo;
    [SerializeField] private OverlapBoxInfo boxWallInfo;
    public PlayerTourch PlayerTourch => playerTourch;
    [Header("PartMove")]
    [SerializeField] private PlayerHorizontal playerHorizontal;
    [SerializeField] private PlayerVertical playerVertical;
    public PlayerHorizontal PlayerHor => playerHorizontal;

    private void Awake() {
        playerHorizontal.Init(this);
        playerVertical.Init(this);
    }


    private void Update() {
        if(Physics2D.OverlapBox(boxGroundInfo.transform.position, boxGroundInfo.size, boxGroundInfo.angle, boxGroundInfo.layerMask)) {
            playerTourch = PlayerTourch.GROUND;
        } else if(Physics2D.OverlapBox(boxWallInfo.transform.position, boxWallInfo.size, boxWallInfo.angle, boxWallInfo.layerMask)) {
            playerTourch = PlayerTourch.WALL;
        } else {
            playerTourch = PlayerTourch.AIR;
        }
    }

    #region Support
    [ContextMenu("SetUp")]
    public void SetUp() {
        rb2D = transform.GetComponent<Rigidbody2D>();
        col2D = transform.GetComponent<BoxCollider2D>();

        GameObject gameobj = new GameObject();
        gameobj.transform.parent = transform;
        gameobj.name = "BoxGroundInfo";
        var overInfo = gameobj.AddComponent<OverlapBoxInfo>();
        gameobj.transform.localPosition = new Vector3(0, -col2D.size.y / 2f, 0f);
        overInfo.size = new Vector2(col2D.size.x - 0.04f, 0.05f);
        overInfo.angle = 0f;
        overInfo.layerMask = LayerMask.GetMask("Ground");
        boxGroundInfo = overInfo;


        GameObject gameobjII = new GameObject();
        gameobjII.transform.parent = transform;
        gameobjII.name = "BoxWallInfo";
        var overInfoII = gameobjII.AddComponent<OverlapBoxInfo>();
        gameobjII.transform.localPosition = new Vector3(col2D.size.x/2,0f, 0f);
        overInfoII.size = new Vector2(0.05f, col2D.size.y - 0.04f);
        overInfoII.angle = 0f;
        overInfoII.layerMask = LayerMask.GetMask("Ground");
        boxWallInfo = overInfoII;

        playerHorizontal = GetComponent<PlayerHorizontal>();
        playerVertical = GetComponent<PlayerVertical>();
    }
    #endregion
}

public enum PlayerTourch {
    AIR = 0,
    WALL = 1,
    GROUND = 2,
}
