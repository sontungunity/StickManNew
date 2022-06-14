using STU;
using UnityEngine;

public class WitchCS : BossBase
{
    private WitchAttack witchATK => enemyAttack as WitchAttack;
    [SerializeField] private GameObject land;
    
    protected override void Update()
    {
        if(curStatus == EnemyStatus.IDLE || curStatus == EnemyStatus.NONE || curStatus == EnemyStatus.MOVE || curStatus == EnemyStatus.DETECH || curStatus == EnemyStatus.GET_DAME) 
        {
            afterUpdateStatus = EnemyStatus.MOVE;
            SetupStatus();
            SetStatus(afterUpdateStatus);
        }
    }

    public override void GetDame(int dame, GameObject objMakeDame = null)
    {
        if(curStatus == EnemyStatus.DIE || curHeart <= 0) 
        {
            return;
        }
        curHeart -= dame;
        EventDispatcher.Dispatch<EventKey.BossGetDame>(new EventKey.BossGetDame());
        if(curHeart <= 0) 
        {
            land.SetActive(true);
            Die(objMakeDame);
        }
        else
        {
            if(!witchATK.Attacking)
            {
                rg2D.velocity = Vector2.zero;
                enemyAnim.SetAnimGetDame(() =>
                {
                    enemyAnim.AnimIdle();
                });
            }
            particleBlood?.Play();
        }
    }

    protected override void SetupStatus()
    {
        foreach(var col in distan_attack.ArrayCollider2D)
        {
            if(col != null && col.transform.parent.GetComponent<Player>() != null)
            {
                afterUpdateStatus = EnemyStatus.ATTACK;
                if(!enemyAttack.CanAttack)
                {
                    afterUpdateStatus = EnemyStatus.IDLE;
                }
                return;
            }
        }

        foreach(var col in eye_Befor.ArrayCollider2D)
        {
            if(col != null && col.transform.parent.GetComponent<Player>() != null)
            {
                afterUpdateStatus = EnemyStatus.DETECH;
                break;
            }
        }
        
        if(afterUpdateStatus != EnemyStatus.DETECH)
        {
            foreach(var col in eye_After.ArrayCollider2D)
            {
                if(col != null && col.transform.parent.GetComponent<Player>() != null)
                {
                    afterUpdateStatus = EnemyStatus.DETECH;
                    Flip();
                    break;
                }
            }
        }
    }

    public void Blink()
    {
        
    }
}