
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

        // ���� �ִϸ��̼��� ���� �ִϸ��̼��̸�
        if (animInfo.IsName("Atk"))
        {
            // �ִϸ��̼��� 90% ������ �� ���� �浹 Ȱ��ȭ
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

        // "Damaged" �ִϸ��̼��� ���� ��
        if (animInfo.IsName("Damaged") && animInfo.normalizedTime >= 1)
        {
            // "Damaged" �ִϸ��̼��� ���� �� "MonsterEnter" ���·� ��ȯ
            _enemy.ChangeState(new MonsterAtk(_enemy));
        }
    }

    public override void ExitState()
    {
        // ���� ���� �� �߰� �۾��� �ʿ��ϸ� ���⿡ �߰�
    }
}


