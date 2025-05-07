using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance;

    public PlayerController player { get; private set; }

    public enum MiniGameType { None, Bird, Stack }
    private MiniGameType selectedGame = MiniGameType.None;

    private void Awake()
    {
        Instance = this;

        player = FindObjectOfType<PlayerController>();
        player.Init(this);
    }

    public void SelectGame(MiniGameType type)
    {
        selectedGame = type;
    }

    public void YesGame()
    {
        switch (selectedGame)
        {
            case MiniGameType.Bird:
                SceneManager.LoadScene("BirdGameScene");
                break;
            case MiniGameType.Stack:
                SceneManager.LoadScene("StackGameScene");
                break;
            default:
                Debug.LogWarning("미니게임 종류가 설정되지 않았습니다.");
                break;
        }
    }

    public void NoGame()
    {

        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.ChangeState(UIState.None);  // UI 모두 꺼지게
        }
    }

}
