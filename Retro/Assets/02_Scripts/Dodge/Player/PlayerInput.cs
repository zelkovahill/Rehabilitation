using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("[키보드 키 설정]")]
    [SerializeField, Tooltip("플레이어의 달리기 키를 설정합니다.")]
    private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("[키보드 입력]")]
    [Tooltip("플레이어의 입력 값을 받습니다.")]
    public Vector2 move;

    [Header("[입력 상태]")]
    public bool Sprint;

    private void Update()
    {
        HandleInput();
        HandleSprint();
    }

    private void HandleInput()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
    }

    private void HandleSprint()
    {
        Sprint = Input.GetKey(sprintKey);
    }
}
