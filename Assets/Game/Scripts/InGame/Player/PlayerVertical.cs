using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerVertical : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private Vector2 jumpForceGround = new Vector2(0,9f);
    [SerializeField] private Vector2 jumpForceWall = new Vector2(9f,9f);
    [SerializeField] private float timeForOneJump = 0.2f;
    [Header("Fall")]
    //[SerializeField] private float gravityFall = 9f;
    //[SerializeField] private float gravityNormal = 18f;
    [SerializeField] private float fallForAir = 18f;
    [SerializeField] private float fallForceWall =3f;
    [SerializeField] private PlayerHorizontal playerHorizontal;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Player player;
    [Header("ListAnim")]
    [SerializeField] private List<EnumPlayerStatus> lstStatusIdle;
    private TurnJump turnJump;
    public TurnJump JumpInfo => turnJump;
    private bool doJump;

    private PlayerMovement playerMovement;
    private Rigidbody2D rb2D => playerMovement.Rb2D;
    
    private void Awake() {
        turnJump = new TurnJump();
    }
    public void Init(PlayerMovement playerMovement) {
        this.playerMovement = playerMovement;
    }

    private void Update() {
        Inputs();
    }

    private void Inputs() {
        if(Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Jump")) {
            doJump = true;
        }
    }
    private void FixedUpdate() {
        //Hanlder Input
        if(doJump) {
            if(playerMovement.PlayerTourch == PlayerTourch.GROUND) {
                turnJump.Set(EnumJumpType.JUMP_I,jumpForceGround, timeForOneJump);
            } else if(playerMovement.PlayerTourch == PlayerTourch.WALL) { 
                Vector2 bounce = jumpForceWall;
                if(playerMovement.PlayerFace == DirHorizontal.RIGHT) {
                    bounce.x = -bounce.x;
                }
                turnJump.Set(EnumJumpType.JUMP_II, bounce, timeForOneJump);
            } else if(playerMovement.PlayerTourch == PlayerTourch.AIR) {
                if(turnJump.TypeJump == EnumJumpType.JUMP_I) {
                    turnJump.Set(EnumJumpType.JUMP_II,jumpForceGround, timeForOneJump);
                }
            }
            doJump = false;
        } 

        //Hanlder movement
        if(turnJump.TimeJump > 0) {
            if(turnJump.Force.x * (int)playerMovement.PlayerFace > 0 && playerMovement.PlayerTourch == PlayerTourch.WALL) {
                turnJump.TimeJump = 0;
            } else {
                rb2D.velocity = new Vector2(rb2D.velocity.x + turnJump.Force.x, turnJump.Force.y);
            }
        } else {
            if(playerMovement.PlayerTourch == PlayerTourch.AIR) {
                rb2D.velocity = new Vector2(rb2D.velocity.x, -fallForAir);
            } else if(playerMovement.PlayerTourch == PlayerTourch.WALL) {
                if(playerHorizontal.XDirectionalInput ==0  ) {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, -fallForceWall);
                } else {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                }
            } else if(playerMovement.PlayerTourch == PlayerTourch.GROUND) {
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
            }
        }

        //Hanlder Anim
        if(turnJump.TimeJump > 0) {
            player.SetPlayerStatusCheckRank(EnumPlayerStatus.JUMP);
        } else {
            if(playerMovement.PlayerTourch == PlayerTourch.AIR) {
                player.SetPlayerStatusCheckRank(EnumPlayerStatus.JUMPFALL);
            } else if(playerMovement.PlayerTourch == PlayerTourch.WALL) {
                player.SetPlayerStatusCheckRank(EnumPlayerStatus.CLIMB);
            } else if(playerMovement.PlayerTourch == PlayerTourch.GROUND) {
                player.SetIdleCheckStatus(lstStatusIdle);
            }
        }

        //hanlder Time
        if(turnJump.TimeJump > 0) {  
            turnJump.TimeJump -= Time.fixedDeltaTime;
        } 
    }

    public void SetUpNoVertical() {
        turnJump.Defaul();
        player.SetIdleCheckStatus(lstStatusIdle);
    }

    public class TurnJump {
        public EnumJumpType TypeJump;
        public Vector2 Force;
        public float TimeJump;

        public TurnJump() {
            this.TypeJump = EnumJumpType.NONE;
            this.Force = Vector2.zero;
            this.TimeJump = 0f;
        }

        public TurnJump(EnumJumpType type, Vector2 force,float timeJump) {
            this.TypeJump = type;
            this.Force = force;
            this.TimeJump = timeJump;
        }

        public void Set(EnumJumpType type, Vector2 force, float timeJump) {
            this.TypeJump = type;
            this.Force = force;
            this.TimeJump = timeJump;
        }

        public void Defaul() {
            this.TypeJump = EnumJumpType.NONE;
            this.Force = Vector2.zero;
            this.TimeJump = 0f;
        }
    }

    #region Setup
    [ContextMenu("Setup")]
    public void Setup() {
        playerHorizontal = GetComponent<PlayerHorizontal>();
    }
    #endregion
}

public enum EnumJumpType {
    NONE,
    JUMP_I,
    JUMP_II
}




