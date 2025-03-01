using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class PlayerStatetMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }

    private PlayerIdleState _idleState;
    private PlayerMoveState _moveState;
    private PlayerDieState _dieState;

    public PlayerIdleState GetIdleState() => _idleState;
    public PlayerMoveState GetMoveState() => _moveState;
    public PlayerDieState GetDieState() => _dieState;

    private void Awake()
    {
        _idleState = new PlayerIdleState(this);
        _moveState = new PlayerMoveState(this);
        _dieState = new PlayerDieState(this);
    }

    private void Start()
    {
        TransitionToState(_idleState);
    }

    private void Update()
    {
        CurrentState.Update();
    }

    public void TransitionToState(PlayerState newState)
    {
        if (CurrentState == newState)
        {
            return;
        }

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
