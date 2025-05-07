using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using StackMiniGame;

namespace StackMiniGame
{


    public enum UIState
    {
        Home,
        Game,
        Score,

    }

    public class UIManager : MonoBehaviour
    {
        static UIManager instance;

        public static UIManager Instance
        {
            get { return instance; }
        }

        UIState currentState = UIState.Home;
        HomeUI homeUI = null;
        GameUI gameUI = null;
        ScoreUI scoreUI = null;

        TheStack theStack = null;

        private void Awake()
        {
            instance = this;

            theStack = FindObjectOfType<TheStack>();

            homeUI = GetComponentInChildren<HomeUI>(true);
            homeUI?.Init(this);

            gameUI = GetComponentInChildren<GameUI>(true);
            gameUI?.Init(this);

            scoreUI = GetComponentInChildren<ScoreUI>(true);
            scoreUI?.Init(this);

            ChangeState(UIState.Home);
        }

        public void ChangeState(UIState state)
        {
            currentState = state;
            homeUI?.SetActive(currentState);
            gameUI?.SetActive(currentState);
            scoreUI?.SetActive(currentState);
        }

        public void onClickStart()
        {
            theStack.Restart();
            ChangeState(UIState.Game);
        }

        public void onClickExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void UpdateScore()
        {
            gameUI.SetUI(theStack.Score, theStack.ComboCount, theStack.MaxCombo);
        }

        public void SetScoreUI()
        {
            scoreUI.SetUI(theStack.Score, theStack.MaxCombo, theStack.BestScore, theStack.BestCombo);
            ChangeState(UIState.Score);
        }
    }
}