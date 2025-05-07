using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int score = 0;

    private void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� �̵� �� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���� �߰� �޼���
    public void AddScore(int value)
    {
        score += value;
    }

    // �ְ� ���� ����
    public void SaveHighScore()
    {
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    // ���� �ʱ�ȭ
    public void ResetScore()
    {
        score = 0;
    }
}
