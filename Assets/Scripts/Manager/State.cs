using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("현재 상태 : enter");
        if (enemy.target != null)
        {
            enemy._nav.SetDestination(enemy.target.position);
            enemy.animator.SetBool("IsTrack", true);
        }
    }
    public override void ExecuteOnUpdate()
    {
        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.target.transform.position);

        if (distanceToTarget < enemy.AtkRange)
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
        
    }

    public override void ExecuteOnUpdate()
    {
        enemy.animator.SetTrigger("Atk");
        var animInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log(animInfo.normalizedTime);
        if (animInfo.IsName("Atk"))
        {
            if (animInfo.normalizedTime < 0.32f)
            {

            }
            else if (animInfo.normalizedTime < 0.55f)
            {
                enemy.collider.enabled = true;
            }
            else if (animInfo.normalizedTime < 0.80f)
            {
                enemy.collider.enabled = false;
            }
            else
            {
                enemy.collider.enabled = false;
                enemy.ChangeState(new MonsterEnter(enemy)); 
                return;
            }
        }

        if (Time.time - enemy.LastAttackTime >= enemy.CoolTime)
        {
            enemy.ChangeState(new MonsterAtk(enemy));
        }
    }


public override void ExitState()
    {
        
    }
}
