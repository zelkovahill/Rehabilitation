using TMPro;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("총알 프리펩을 설정합니다.")]
    private GameObject bulletPrefab;

    [SerializeField, Tooltip("최소 생성 주기를 설정합니다.")]
    private float minSpawnDelay = 0.5f;

    [SerializeField, Tooltip("최대 생성 주기를 설정합니다.")]
    private float maxSpawnDelay = 3f;

    // 내부 변수
    private Transform _target;
    private float _spwnRate;
    private float _timeAfterSpawn;

    private void Start()
    {
        _timeAfterSpawn = 0f;

        _spwnRate = Random.Range(minSpawnDelay, maxSpawnDelay);

        _target = FindFirstObjectByType<PlayerAction>().transform;

        // target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        _timeAfterSpawn += Time.deltaTime;

        if (_timeAfterSpawn >= _spwnRate)
        {
            _timeAfterSpawn = 0f;

            // GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            GameObject bullet = PoolManager.Instance.SpawnFromPool("Bullet", transform.position, transform.rotation);

            bullet.transform.LookAt(_target);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetVelocity();

            _spwnRate = Random.Range(minSpawnDelay, maxSpawnDelay);
        }
    }
}
