using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace FlappyBird
{
    public class BirdGameOverUI : MonoBehaviour
    {
        // �̱��� �������� ��𼭵� ���� �����ϰ� ��
        public static BirdGameOverUI Instance { get; private set; }

        [Header("���� ���� UI ���")]
        public GameObject gameOverPanel;
        public TextMeshProUGUI finalScoreText;
        public Button retryButton;
        public Button lobbyButton;

        public bool IsVisible => gameOverPanel.activeSelf;

        private void Awake()
        {
            // Instance�� ��� ���� �� �ڽ����� �ʱ�ȭ
            if (Instance == null) Instance = this;
        }

        // UI �ʱ�ȭ �� ��ư �̺�Ʈ ����
        public void Init()
        {
            // RetryButton ����
            var retryObj = gameOverPanel.transform.Find("RetryButton");
            if (retryObj != null)
            {
                retryButton = retryObj.GetComponent<Button>();
                retryButton?.onClick.AddListener(RestartGame);
            }

            // LobbyButton ����
            var lobbyObj = gameOverPanel.transform.Find("LobbyButton");
            if (lobbyObj != null)
            {
                lobbyButton = lobbyObj.GetComponent<Button>();
                lobbyButton?.onClick.AddListener(ReturnToLobby);
            }

            // FinalScoreText ����
            if (finalScoreText == null)
            {
                finalScoreText = gameOverPanel.transform.Find("FinalScoreText")?.GetComponent<TextMeshProUGUI>();
            }

            gameOverPanel.SetActive(false); // ���� �� ��Ȱ��ȭ
        }

        // ������ �Բ� ���� ���� UI ���
        public void Show(int score)
        {
            Init();
            Time.timeScale = 1f;
            gameOverPanel.SetActive(true);
            finalScoreText.text = $"Final Score: {score}";
        }

        // ���� ����� (���� �� �ٽ� �ε�)
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // ���� �κ�� ����
        public void ReturnToLobby()
        {
            SceneManager.LoadScene("SampleScene"); // ���θ� �� �̸�
        }
    }
}