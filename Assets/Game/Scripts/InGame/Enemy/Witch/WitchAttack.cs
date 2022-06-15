using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class WitchAttack : EnemyAttack 
{
    [SerializeField] private BeamRayCast beamFace;
    
    [Header("CustomerFire")]
    [SerializeField] private int timeFire;
    [SerializeField] private float timeDelay;
    [SerializeField] private GameObject fireRay;

    public bool Attacking => attacking;

    private Action callback;
    private bool attacking = false;
    private WaitForSeconds waitTimeDelay;
    private Coroutine coroutine;
    
    protected override void Awake()
    {
        base.Awake();
        waitTimeDelay = new WaitForSeconds(timeDelay);
    }

    private void Update()
    {
        if(!CheckFaceCanMove())
        {
            Vector2 curVelocity = enemyBase.Rg2D.velocity;
            curVelocity.y = 0;
            enemyBase.Rg2D.velocity = curVelocity;
        }
    }

    private bool CheckFaceCanMove()
    {
        Collider2D collider2D = null;
        foreach(var col in beamFace.ArrayCollider2D)
        {
            if(col != null)
            {
                collider2D = col;
                break;
            }
        }
        return collider2D == null;
    }
    
    public override void Attack(Action callback = null)
    {
        if(canAttack)
        {
            this.callback = callback;
            canAttack = false;
            enemyBase.Rg2D.velocity = Vector2.zero;
            enemyAnim.SetAnimAttack();
            attacking = true;
        } 
        else
        {
            callback?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!attacking)
        {
            return;
        }
        coroutine = StartCoroutine(CastFireRay());
    }

    IEnumerator CastFireRay()
    {
        enemyAnim.SetAnim(0, enemyAnim.name = "pre_attack", false, callback);
        fireRay.GetComponent<SpriteRenderer>().enabled = true;
        
        for (int i = 0; i < timeFire; i++)
        {
            // turn on fire effect
            enemyAnim.SetAnim(0, enemyAnim.name = "attack", false, callback);
            FireRayCS fireray = GetComponent<FireRayCS>();
            fireRay.GetComponent<BoxCollider2D>().enabled = true;
            fireray.transform.position = transform.position;
            fireray.Fire(enemyBase.curDame);
            enemyAnim.SetAnim(0, enemyAnim.name = "attack_to_idle", false, callback);
            yield return waitTimeDelay;
        }
        fireRay.GetComponent<SpriteRenderer>().enabled = false;
        fireRay.GetComponent<BoxCollider2D>().enabled = false;
        
        attacking = false;
        callback?.Invoke();
        tween.CheckKillTween();
        tween = DOVirtual.DelayedCall(timeDelayAttack, () =>
        {
            canAttack = true;
        });
    }
    private void OnDisable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
