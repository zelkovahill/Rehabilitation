using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField, Tooltip("총알의 이동 속도를 설정합니다.")]
    private float speed = 8f;

    // 내부 변수
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void SetVelocity()
    {
        _rigidbody.linearVelocity = transform.forward * speed;

        CancelInvoke(nameof(ReturnToPool));
        Invoke(nameof(ReturnToPool), 2f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAction playerAction = other.GetComponent<PlayerAction>();

            playerAction?.Die();
        }
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

}
