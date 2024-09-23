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
        _digimon.animator.SetTrigger("Atk");
    }

    public override void ExecuteOnUpdate()
    {
        if (_digimon.isEvolutioning)
        {
            _digimon.ChangeState(new PlayerEvolution(_digimon));
            return;
        }

        if (_digimon.CurrentHp <= 0)
        {
            _digimon.ChangeState(new PlayerStun(_digimon));
            return;
        }

        RotateTowardsClick();  // 마우스 클릭한 방향으로 회전

        var animInfo = _digimon.animator.GetCurrentAnimatorStateInfo(0);
        if (animInfo.IsName("Atk"))
        {
            if (animInfo.normalizedTime >= 0.55f)
            {
                _digimon.atkCollider.enabled = true;
            }
            else
            {
                _digimon.atkCollider.enabled = false;
                _digimon.ChangeState(new PlayerEnter(_digimon));
            }
        }
    }

    public override void ExitState()
    {

    }

    private void RotateTowardsClick()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Vector3 targetPosition = new Vector3(hitInfo.point.x, _digimon.transform.position.y, hitInfo.point.z);
                _digimon.transform.LookAt(targetPosition);
            }
        }
    }
}


public class PlayerStun : PlayerState
{
    private readonly Digimon _digimon;
    public PlayerStun(Digimon playerState)
    {
        _digimon = playerState;
    }

    public override void EnterState()
    {
        _digimon.animator.SetTrigger("Down");
        _digimon.CurrentHp = 100;
    }

    public override void ExecuteOnUpdate()
    {
        _digimon.ChangeState(new PlayerAttack(_digimon));
    }

    public override void ExitState() { }
}

public class PlayerEvolution : PlayerState
{
    private readonly Digimon _digimon;

    public PlayerEvolution(Digimon digimon)
    {
        _digimon = digimon;
    }

    public override void EnterState()
    {

    }

    public override void ExecuteOnUpdate()
    {
        _digimon.atkCollider.enabled = false;
        if (_digimon.isEvolutioning == false)
        {
            _digimon.ChangeState(new PlayerEnter(_digimon));
        }
    }
    public override void ExitState()
    {
        _digimon.animator.enabled= true;
    }
}

public class PlayerSkill : PlayerState
{
    private readonly Digimon _digimon;

    public PlayerSkill(Digimon digimon)
    {
        _digimon = digimon;
    }

    public override void EnterState()
    {
        _digimon.animator.SetTrigger("Skill");
        _digimon.CurrentMP = 0;

    }

    public override void ExecuteOnUpdate()
    {
        if (_digimon.isEvolutioning == true)
        {
            _digimon.ChangeState(new PlayerEvolution(_digimon));
        }

        var animInfo = _digimon.animator.GetCurrentAnimatorStateInfo(0);

        if (animInfo.IsName("Skill"))
        {
            if (animInfo.normalizedTime < 0.32f)
            {

            }
            
            else if (animInfo.normalizedTime < 0.55f)
            {
                _digimon._currentSkill.Execute(_digimon.transform.position);
                _digimon.atkCollider.enabled = true;
            }
            else if (animInfo.normalizedTime < 0.7f)
            {
                _digimon.atkCollider.enabled = false;
            }
            else if (animInfo.normalizedTime < 0.80f)
            {
                _digimon.atkCollider.enabled = true;
            }
            else
            {
                _digimon._currentSkill.OffSkillEffect();
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


