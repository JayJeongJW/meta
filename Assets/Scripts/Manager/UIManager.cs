using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    None, //아무것도 아닌 상태
    BirdNPC,  // Bird 미니게임 상태
    StackNPC  // Stack 미니게임 상태
}


public class UIManager : MonoBehaviour
{
    BirdNPCUI birdNPCUI;
    StackNPCUI stackNPCUI;

    private UIState currentState;

    private void Awake()
    {
        birdNPCUI = GetComponentInChildren<BirdNPCUI>();
        stackNPCUI = GetComponentInChildren<StackNPCUI>();

        birdNPCUI.Init(this);
        stackNPCUI.Init(this);

        ChangeState(UIState.None); // 초기엔 아무 UI도 안 보이게
    }


    public void ChangeState(UIState state)
    {
        currentState = state;

        birdNPCUI.SetActive(currentState);
        stackNPCUI.SetActive(currentState);
    }

}

