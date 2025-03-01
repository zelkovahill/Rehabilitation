using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField, Tooltip("회전 속도를 설정합니다.")]
    private float rotationSpeed = 60f;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
