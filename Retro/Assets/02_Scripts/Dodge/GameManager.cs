using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("[UI 설정]")]
    [SerializeField, Tooltip("게임 오버시 활성화 할 텍스트 게임 오브젝트를 설정합니다.")]
    private GameObject gameOverPanel;

    [SerializeField, Tooltip("생존 시간을 표시할 텍스트 컴포넌트를 설정합니다.")]
    private TextMeshProUGUI timeText;

    [SerializeField, Tooltip("최고 기록을 표시할 텍스트 컴포넌트를 설정합니다.")]
    private TextMeshProUGUI recordText;

    private PlayerAction _playerAction;

    // 내부 변수
    private float _surviveTime; // 생존 시간
    private bool _isGameOver; // 게임 오버 상태

    private void Awake()
    {
        _playerAction = FindFirstObjectByType<PlayerAction>();
        _playerAction.OnDie += EndGame;
    }

    private void Disable()
    {
        _playerAction.OnDie -= EndGame;
    }

    private void Start()
    {
        _surviveTime = 0f;
        _isGameOver = false;
    }

    private void Update()
    {
        if (!_isGameOver)
        {
            _surviveTime += Time.deltaTime;
            timeText.text = $"Time: {(int)_surviveTime}";
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Dodge");
            }
        }

    }

    /// <summary>
    /// 현재 게임을 게임 오버 상태로 변경하는 메서드
    /// </summary>
    public void EndGame()
    {
        _isGameOver = true; // 현재 상태를 게임 오버 상태로 전환
        gameOverPanel.SetActive(true);   // 게임 오버 텍스트 게임 오브젝트를 활성화

        float bestTime = PlayerPrefs.GetFloat("BestTime"); // BestTime 키로 저장된, 이전까지의 최고 기록 가져오기

        // 이전까지의 최고 기록보다 현재 생존 시간이 더 크다면
        if (_surviveTime > bestTime)
        {
            bestTime = _surviveTime;    // 최고 기록의 값을 현재 생존 시간의 값으로 변경
            PlayerPrefs.SetFloat("BestTime", bestTime); // 변경된 최고 기록을 BestTime 키로 저장
        }

        // 최고 기록을 recordText 텍스트 컴포넌트를 통해 표시
        recordText.text = $"Best Time: {(int)bestTime}";
    }

}
