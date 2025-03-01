using System;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("[플레이어 설정]")]
    [SerializeField, Tooltip("플레이어의 속도를 설정합니다.")]
    private float speed = 5f;

    [SerializeField, Tooltip("플레이어의 달리기 속도를 설정합니다.")]
    private float sprintSpeed = 10f;

    [SerializeField, Tooltip("회전 속도를 설정합니다.")]
    private float rotationSpeed = 10f;

    public Action OnDie;

    // 내부 변수
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private float _currentSpeed;
    public bool IsDie { get; private set; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
    }

    public void Move()
    {
        _currentSpeed = _playerInput.Sprint ? sprintSpeed : speed;
        _moveDirection = new Vector3(_playerInput.move.x, 0, _playerInput.move.y).normalized;

        if (_moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        _characterController.Move(_moveDirection * _currentSpeed * Time.deltaTime);
    }

    public void Die()
    {
        OnDie?.Invoke();
        IsDie = true;
        // this.gameObject.SetActive(false);

        // GameManager gameManager = FindFirstObjectByType<GameManager>();
        // gameManager.EndGame();
    }


}
