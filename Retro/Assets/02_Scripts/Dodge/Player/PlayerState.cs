using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStatetMachine _playerStateMachine;
    protected PlayerInput _playerInput;
    protected PlayerAction _playerAction;

    public PlayerState(PlayerStatetMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _playerInput = playerStateMachine.GetComponent<PlayerInput>();
        _playerAction = playerStateMachine.GetComponent<PlayerAction>();
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStatetMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Update()
    {
        if (_playerInput.move != Vector2.zero)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetMoveState());
        }
    }
}

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerStatetMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Update()
    {
        _playerAction.Move();

        if (_playerInput.move == Vector2.zero)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetIdleState());
        }
    }

}

public class PlayerDieState : PlayerState
{
    public PlayerDieState(PlayerStatetMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        _playerAction.Die();
    }
}
