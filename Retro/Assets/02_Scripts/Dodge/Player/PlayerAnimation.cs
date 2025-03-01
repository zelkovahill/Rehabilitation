using UnityEditorInternal;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerStateMachine _playerStateMachine;
    private PlayerState _currentState => _playerStateMachine.CurrentState;

    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Die = Animator.StringToHash("Die");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Update()
    {

        switch (_currentState)
        {
            case PlayerIdleState idleState:
                _animator.SetBool(Move, false);
                break;
            case PlayerMoveState moveState:
                _animator.SetBool(Move, true);
                break;
            case PlayerDieState dieState:
                _animator.SetTrigger(Die);
                break;
        }
    }

}
