using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : IState
{
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void ExecuteOnUpdate() { }
}

public class PlayerEnter : PlayerState
{
    private readonly Digimon _digimon;
    public PlayerEnter(Digimon playerState)
    {
        Debug.Log("현재상태 enter");
        _digimon = playerState;
    }

    public override void EnterState()
    {
        _digimon.ChangeState(new PlayerAttack(_digimon));
    }

    public override void ExecuteOnUpdate()
    {
        _digimon.ChangeState(new PlayerAttack(_digimon));
    }

    public override void ExitState() { }
}

public class PlayerAttack : PlayerState
{
    private readonly Digimon _digimon;

    public PlayerAttack(Digimon playerState)
    {
        _digimon = playerState;
    }

    public override void EnterState()
    {
        Debug.Log("현재상태 attack");
        _digimon.animator.SetTrigger("Atk");
    }

    public override void ExecuteOnUpdate()
    {
        float currentTime = Time.time;
        if (currentTime - _digimon.LastAttackTime >= _digimon.CoolTime)
        {
            _digimon.LastAttackTime = Time.time + 1000f;
        }
        var animInfo = _digimon.animator.GetCurrentAnimatorStateInfo(0);

        if (animInfo.IsName("Atk"))
        {
            if (animInfo.normalizedTime < 0.32f)
            {

            }
            else if (animInfo.normalizedTime < 0.55f)
            {
                _digimon.atkCollider.enabled = true;
            }
            else if (animInfo.normalizedTime < 0.80f)
            {
                _digimon.atkCollider.enabled = false;
            }
            else
            {
                _digimon.atkCollider.enabled = false;
                _digimon.ChangeState(new PlayerEnter(_digimon));
                return;
            }
        }
    }

    public override void ExitState()
    {

    }
}


