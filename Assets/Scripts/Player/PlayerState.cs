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
    private readonly Digimon player;
    public PlayerEnter(Digimon playerState)
    {
        player = playerState;
    }

    public override void EnterState()
    {
       
    }

    public override void ExecuteOnUpdate()
    {
        
    }

    public override void ExitState() { }
}

public class PlayerAttack : PlayerState
{
    private readonly PlayerState _playerState;

    public PlayerAttack(PlayerState playerState)
    {
        _playerState = playerState;
    }

    public override void EnterState()
    {

    }
    public override void ExecuteOnUpdate()
    {
        
    }

    public override void ExitState()
    {

    }
}
