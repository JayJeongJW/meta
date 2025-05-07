using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int score = 0;

    private void Awake()
    {
        // 싱글턴 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 이동 시 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 점수 추가 메서드
    public void AddScore(int value)
    {
        score += value;
    }

    // 최고 점수 저장
    public void SaveHighScore()
    {
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    // 점수 초기화
    public void ResetScore()
    {
        score = 0;
    }
}
