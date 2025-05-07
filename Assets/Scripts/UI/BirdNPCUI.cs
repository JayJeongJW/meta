using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BirdNPCUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI qaText;
    [SerializeField] private Button YesButton;
    [SerializeField] private Button NoButton;


    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        YesButton.onClick.AddListener(OnClickYesButton);
        NoButton.onClick.AddListener(OnClickNoButton);
    }

    public void OnClickYesButton()
    {
        GameManager.Instance.SelectGame(GameManager.MiniGameType.Bird);
        GameManager.Instance.YesGame();

        // ¾À ÀüÈ¯!
        SceneManager.LoadScene("BirdMiniGameScene");
    }


    public void OnClickNoButton()
    {
        GameManager.Instance.NoGame();
    }

    public void UpdateQaText(string text)
    {
        qaText.text = text;
    }


    protected override UIState GetUIState()
    {
        return UIState.BirdNPC;
    }

}
