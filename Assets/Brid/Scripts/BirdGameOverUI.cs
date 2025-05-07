using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace FlappyBird
{
    public class BirdGameOverUI : MonoBehaviour
    {
        // 싱글턴 패턴으로 어디서든 접근 가능하게 함
        public static BirdGameOverUI Instance { get; private set; }

        [Header("게임 오버 UI 요소")]
        public GameObject gameOverPanel;
        public TextMeshProUGUI finalScoreText;
        public Button retryButton;
        public Button lobbyButton;

        public bool IsVisible => gameOverPanel.activeSelf;

        private void Awake()
        {
            // Instance가 비어 있을 때 자신으로 초기화
            if (Instance == null) Instance = this;
        }

        // UI 초기화 및 버튼 이벤트 연결
        public void Init()
        {
            // RetryButton 연결
            var retryObj = gameOverPanel.transform.Find("RetryButton");
            if (retryObj != null)
            {
                retryButton = retryObj.GetComponent<Button>();
                retryButton?.onClick.AddListener(RestartGame);
            }

            // LobbyButton 연결
            var lobbyObj = gameOverPanel.transform.Find("LobbyButton");
            if (lobbyObj != null)
            {
                lobbyButton = lobbyObj.GetComponent<Button>();
                lobbyButton?.onClick.AddListener(ReturnToLobby);
            }

            // FinalScoreText 연결
            if (finalScoreText == null)
            {
                finalScoreText = gameOverPanel.transform.Find("FinalScoreText")?.GetComponent<TextMeshProUGUI>();
            }

            gameOverPanel.SetActive(false); // 시작 시 비활성화
        }

        // 점수와 함께 게임 오버 UI 출력
        public void Show(int score)
        {
            Init();
            Time.timeScale = 1f;
            gameOverPanel.SetActive(true);
            finalScoreText.text = $"Final Score: {score}";
        }

        // 게임 재시작 (현재 씬 다시 로드)
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // 메인 로비로 복귀
        public void ReturnToLobby()
        {
            SceneManager.LoadScene("SampleScene"); // 메인맵 씬 이름
        }
    }
}