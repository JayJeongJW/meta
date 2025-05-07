using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StackNPCUI : BaseUI
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
        SceneManager.LoadScene("StackMiniGame");
    }




    public void OnClickNoButton()
    {
        GameManager.Instance.NoGame();
    }
    public void UpdateQaText(string text)
    {
        qaText.text = text;
    }

    // StackNPCUI.cs
    protected override UIState GetUIState()
    {
        return UIState.StackNPC;
    }

}