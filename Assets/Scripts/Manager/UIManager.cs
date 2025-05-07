using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    None, //�ƹ��͵� �ƴ� ����
    BirdNPC,  // Bird �̴ϰ��� ����
    StackNPC  // Stack �̴ϰ��� ����
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

        ChangeState(UIState.None); // �ʱ⿣ �ƹ� UI�� �� ���̰�
    }


    public void ChangeState(UIState state)
    {
        currentState = state;

        birdNPCUI.SetActive(currentState);
        stackNPCUI.SetActive(currentState);
    }

}

