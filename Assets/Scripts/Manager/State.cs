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
    private float bufferDistance = 1.0f;

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
        if (enemy.target == null)
            return;

        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.target.transform.position);

        if (distanceToTarget <= enemy.AtkRange + bufferDistance)
        {
            enemy.animator.SetBool("IsTrack", false);
            enemy._nav.isStopped = true;
            enemy._nav.velocity = Vector3.zero;
            enemy.ChangeState(new MonsterAtk(enemy));
        }
        else
        {
            enemy._nav.SetDestination(enemy.target.position);
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
        AttackTarget(); // ��� ����
    }

    public override void ExecuteOnUpdate()
    {
        // Ÿ���� ���� ������ ����� �ٽ� ����
        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.target.transform.position);
        if (distanceToTarget > enemy.AtkRange)
        {
            enemy.ChangeState(new MonsterEnter(enemy)); // �ٽ� Ÿ���� �����ϵ��� ���� ��ȯ
        }
        else
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        enemy.animator.SetTrigger("Atk");

        var animInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);

        if (animInfo.IsName("Atk") && animInfo.normalizedTime < 0.90f)
        {
            enemy.collider.enabled = true; // ���� ���� Ȱ��ȭ
        }
        else
        {
            enemy.collider.enabled = false; // ���� ���� ��Ȱ��ȭ
        }
    }

    public override void ExitState()
    {
        // ���� ���� ���� �� �ʿ��� ó��
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

        if (animInfo.IsName("Damaged") && animInfo.normalizedTime >= 1)
        {
            _enemy.ChangeState(new MonsterEnter(_enemy));
        }
    }

    public override void ExitState()
    {
        // ���� ���� �� �߰� �۾��� �ʿ��ϸ� ���⿡ �߰�
    }
}
