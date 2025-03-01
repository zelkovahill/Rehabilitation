using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    private PlayerStateMachine _playerStateMachine;
    private PlayerState _currentState => _playerStateMachine.CurrentState;
    private PlayerAction _playerAction;

    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Sprint = Animator.StringToHash("Sprint");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        _playerAction = GetComponent<PlayerAction>();
    }

    private void Update()
    {
        ResetAnimation();

        switch (_currentState)
        {
            case PlayerIdleState idleState:
                _animator.SetBool(Move, false);
                break;

            case PlayerMoveState moveState:
                if (_playerInput.Sprint)
                {
                    _animator.SetBool(Sprint, true);
                }
                _animator.SetBool(Move, true);

                break;


                // case PlayerDieState dieState:
                //     _animator.SetTrigger(Die);
                //     break;
        }
    }

    private void ResetAnimation()
    {
        _animator.SetBool(Move, false);
        _animator.SetBool(Sprint, false);
    }

}
