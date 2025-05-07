using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird
{
    public class BirdGameManager : MonoBehaviour
    {
        public static BirdGameManager instance;

        private int currentScore = 0;

        [Header("실시간 점수 UI")]
        public TextMeshProUGUI ScoreText;

        private void Awake()
        {
            if (instance == null) instance = this;

            if (BirdGameOverUI.Instance != null)
            {
                BirdGameOverUI.Instance.Init(); // 게임 시작 시 UI 초기화
            }
        }

        // 점수를 증가시키고 UI 갱신
        public void AddScore(int score)
        {
            currentScore += score;

            if (ScoreText != null)
                ScoreText.text = $"Score : {currentScore}";
        }

        // 게임 오버 처리 및 점수 저장
        public void GameOver()
        {
            // 현재 점수를 ScoreManager에 저장
            ScoreManager.Instance.score = currentScore;

            // 최고 점수 저장
            ScoreManager.Instance.SaveHighScore();

            // 마지막 점수 저장
            PlayerPrefs.SetInt("LastScore", currentScore);
            PlayerPrefs.Save();

            // 게임오버 UI 표시
            if (BirdGameOverUI.Instance != null)
            {
                BirdGameOverUI.Instance.Init();
                BirdGameOverUI.Instance.Show(currentScore);
            }
        }


        // 씬 재시작
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}