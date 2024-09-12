
using UnityEngine;

public interface IState
{
    void EnterState();
    void ExitState();
    void ExecuteOnUpdate();
}

public class MonsterState : IState
{
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void ExecuteOnUpdate() { }
}

public class MonsterEnter : MonsterState
{
    private readonly Enemy enemy;
    public MonsterEnter(Enemy enemyState)
    {
        enemy = enemyState;
    }
    public override void EnterState()
    {
        if (enemy.target != null)
        {
            enemy._nav.SetDestination(enemy.target.position);
            enemy.animator.SetBool("IsTrack", true);
        }
    }
    public override void ExecuteOnUpdate()
    {
        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.target.transform.position);

        if (distanceToTarget <= enemy.AtkRange)
        {
            enemy.animator.SetBool("IsTrack", false);
            enemy._nav.isStopped = true;
            enemy._nav.velocity = Vector3.zero;

            enemy.ChangeState(new MonsterAtk(enemy));
        }
    }

    public override void ExitState()
    {
        
    }
}

public class MonsterAtk : MonsterState
{
    private readonly Enemy enemy;

    public MonsterAtk(Enemy enemyState)
    {
       enemy = enemyState;
    }
    public override void EnterState()
    {
        enemy.animator.SetTrigger("Atk");
    }

    public override void ExecuteOnUpdate()
    {
        float currentTime = Time.time;
        if (currentTime - enemy.LastAttackTime >= enemy.CoolTime)
        {
            enemy.LastAttackTime = Time.time + 1000f;
        }
        var animInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);

        if (animInfo.IsName("Atk"))
        {
            
            if (animInfo.normalizedTime < 0.90f)
            {
                enemy.collider.enabled = true;
            }
            else
            {
                enemy.collider.enabled = false;
                enemy.ChangeState(new MonsterEnter(enemy)); 
                return;
            }
        }
        enemy.ChangeState(new MonsterEnter(enemy));

    }


    public override void ExitState()
    {
        
    }
}
