using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using StackMiniGame;
using UnityEngine.SceneManagement;

namespace StackMiniGame
{


    public class ScoreUI : BaseUI
    {
        TextMeshProUGUI scoreText;
        TextMeshProUGUI comboText;
        TextMeshProUGUI bestScoreText;
        TextMeshProUGUI bestComboText;

        Button startButton;
        Button exitButton;

        protected override UIState GetUIstate()
        {
            return UIState.Score;
        }
        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            scoreText = transform.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
            comboText = transform.Find("ComboTxt").GetComponent<TextMeshProUGUI>();
            bestScoreText = transform.Find("BestScoreTxt").GetComponent<TextMeshProUGUI>();
            bestComboText = transform.Find("BestComboTxt").GetComponent<TextMeshProUGUI>();

            startButton = transform.Find("StartButton").GetComponent<Button>();
            exitButton = transform.Find("ExitButton").GetComponent<Button>();

            startButton.onClick.AddListener(OnClickStartButton);
            exitButton.onClick.AddListener(OnClickExitToLobby);
        }

        public void SetUI(int score, int combo, int bestScore, int bestCombo)
        {
            scoreText.text = score.ToString();
            comboText.text = combo.ToString();
            bestScoreText.text = bestScore.ToString();
            bestComboText.text = bestCombo.ToString();
        }

        void OnClickStartButton()
        {
            uiManager.onClickStart();
        }

        public void OnClickExitToLobby()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}

