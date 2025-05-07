using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StackMiniGame;
using UnityEngine.SceneManagement;

namespace StackMiniGame
{

    public class HomeUI : BaseUI
    {
        Button startButton;
        Button exitButton;


        protected override UIState GetUIstate()
        {
            return UIState.Home;
        }

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            startButton = transform.Find("StartButton").GetComponent<Button>();
            exitButton = transform.Find("ExitButton").GetComponent<Button>();

            startButton.onClick.AddListener(OnClickStartButton);
            exitButton.onClick.AddListener(OnClickExitToLobby);
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
