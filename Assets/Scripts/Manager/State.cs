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
    private readonly MonsterState _monsterState;
    public MonsterEnter(MonsterState monsterState)
    {
        _monsterState = monsterState;
    }
    public override void EnterState()
    {
        _monsterState.EnterState();
    }
    public override void ExecuteOnUpdate()
    {
        //Update���� �ൿ�� �ڵ�
    }
    public override void ExitState()
    {
        //���¸� ������
    }
}