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
        enemy._nav.SetDestination(enemy.target.position);
    }
    public override void ExecuteOnUpdate()
    {
        if(Vector3.Distance(enemy.transform.position, enemy.target.transform.position) > 3)
        {
            enemy._nav.isStopped= true;
            enemy._nav.velocity= Vector3.zero;
        }
    }
    public override void ExitState()
    {
        
    }
}