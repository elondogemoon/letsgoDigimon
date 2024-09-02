using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnStart();
    void OnExit();
    void OnExecuteUpdate();
}

public class MonsterState : IState
{
    public virtual void OnStart() { }
    public virtual void OnExit() { }
    public virtual void OnExecuteUpdate() { }
}

public class MonsterEnter : MonsterState
{
    private readonly MonsterState _monsterState;
    public MonsterEnter(MonsterState monsterState)
    {
        _monsterState = monsterState;
    }
    public override void OnStart()
    {
        _monsterState.OnStart();
    }
    public override void OnExecuteUpdate()
    {
        //Update���� �ൿ�� �ڵ�
    }
    public override void OnExit()
    {
        //���¸� ������
    }
}