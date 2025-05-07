using UnityEngine;
using TMPro;

public class SampleSceneScoreUI : MonoBehaviour
{
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI bestScoreText;

    public float displayDuration = 3f; // 몇 초 동안 보일지 설정
    private float timer;

    void Start()
    {

        int lastScore = PlayerPrefs.GetInt("LastScore", -1);
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);

        if (lastScore >= 0)
        {
            lastScoreText.text = $"Last Score: {lastScore}";
            bestScoreText.text = $"Best Score: {bestScore}";
            timer = displayDuration;
        }
        else
        {
            lastScoreText.gameObject.SetActive(false);
            bestScoreText.gameObject.SetActive(false);
            this.enabled = false;
        }
    }



    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (lastScoreText != null)
                lastScoreText.gameObject.SetActive(false);
            if (bestScoreText != null)
                bestScoreText.gameObject.SetActive(false);

            this.enabled = false; // Update 함수 멈춤
        }
    }
}
