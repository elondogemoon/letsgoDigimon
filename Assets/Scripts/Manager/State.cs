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
        if (enemy.target != null)
        {
            enemy._nav.SetDestination(enemy.target.position);
            enemy.animator.SetBool("IsTrack", true);
            enemy._nav.isStopped = false;
        }
    }

    public override void ExecuteOnUpdate()
    {
        if (enemy.target == null)
            return;
        //enemy._nav.SetDestination(enemy.target.position);

        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.target.transform.position);

        if (distanceToTarget <= enemy.AtkRange)
        {
            enemy.animator.SetBool("IsTrack", false);
            enemy._nav.isStopped = true;
            enemy._nav.velocity = Vector3.zero;
            enemy.ChangeState(new MonsterAtk(enemy));
        }

        else
        {
            return;
        }
    }

    public override void ExitState()
    {
        enemy.animator.SetBool("IsTrack", false);
        enemy._nav.isStopped = true;
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
        enemy._nav.isStopped = true;
        AttackTarget();
        enemy.animator.SetTrigger("Atk");
    }

    public override void ExecuteOnUpdate()
    {
        AttackTarget();
    }

    private void AttackTarget()
    {
        enemy.animator.SetTrigger("Atk");

        var animInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);

        if (animInfo.IsName("Atk") && animInfo.normalizedTime < 0.90f)
        {
            enemy.atkcollider.enabled = true; 
        }
        else
        {
            enemy.atkcollider.enabled = false;
        }
    }

    public override void ExitState()
    {
        
    }
}

public class MonsterDamaged : MonsterState
{
    private readonly Enemy _enemy;

    public MonsterDamaged(Enemy enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.animator.SetTrigger("Damaged");
        SpawnManager.Instance.HitEffectOn(_enemy.transform);
    }

    public override void ExecuteOnUpdate()
    {
        var animInfo = _enemy.animator.GetCurrentAnimatorStateInfo(0);

        if (animInfo.IsName("Damaged") && animInfo.normalizedTime <= 0.9f)
        {
            _enemy.ChangeState(new MonsterEnter(_enemy));
        }
    }

    public override void ExitState()
    {

    }
}
