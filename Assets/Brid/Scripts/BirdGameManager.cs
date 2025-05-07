using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird
{
    public class BirdGameManager : MonoBehaviour
    {
        public static BirdGameManager instance;

        private int currentScore = 0;

        [Header("�ǽð� ���� UI")]
        public TextMeshProUGUI ScoreText;

        private void Awake()
        {
            if (instance == null) instance = this;

            if (BirdGameOverUI.Instance != null)
            {
                BirdGameOverUI.Instance.Init(); // ���� ���� �� UI �ʱ�ȭ
            }
        }

        // ������ ������Ű�� UI ����
        public void AddScore(int score)
        {
            currentScore += score;

            if (ScoreText != null)
                ScoreText.text = $"Score : {currentScore}";
        }

        // ���� ���� ó�� �� ���� ����
        public void GameOver()
        {
            // ���� ������ ScoreManager�� ����
            ScoreManager.Instance.score = currentScore;

            // �ְ� ���� ����
            ScoreManager.Instance.SaveHighScore();

            // ������ ���� ����
            PlayerPrefs.SetInt("LastScore", currentScore);
            PlayerPrefs.Save();

            // ���ӿ��� UI ǥ��
            if (BirdGameOverUI.Instance != null)
            {
                BirdGameOverUI.Instance.Init();
                BirdGameOverUI.Instance.Show(currentScore);
            }
        }


        // �� �����
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}