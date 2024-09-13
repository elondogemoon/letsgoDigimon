
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
        
    }

    public override void ExecuteOnUpdate()
    {
        enemy.animator.SetTrigger("Atk");

        var animInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);

        // 현재 애니메이션이 공격 애니메이션이면
        if (animInfo.IsName("Atk"))
        {
            // 애니메이션이 90% 이하일 때 공격 충돌 활성화
            if (animInfo.normalizedTime < 0.90f)
            {
                enemy.collider.enabled = true;
            }
            else
            {
                enemy.collider.enabled = false;

                enemy.ChangeState(new MonsterAtk(enemy));
            }
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
    }

    public override void ExecuteOnUpdate()
    {
        var animInfo = _enemy.animator.GetCurrentAnimatorStateInfo(0);

        // "Damaged" 애니메이션이 끝난 후
        if (animInfo.IsName("Damaged") && animInfo.normalizedTime >= 1)
        {
            // "Damaged" 애니메이션이 끝난 후 "MonsterEnter" 상태로 전환
            _enemy.ChangeState(new MonsterAtk(_enemy));
        }
    }

    public override void ExitState()
    {
        // 상태 종료 시 추가 작업이 필요하면 여기에 추가
    }
}


