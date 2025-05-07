using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;

    public virtual void Init(UIManager uiManager)
    {
        this.uiManager = uiManager;

    }

    // 추상 메서드: 하위 클래스에서 반드시 구현해야 함
    protected abstract UIState GetUIState();

    // UI 상태에 맞게 활성화 여부를 결정
    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}

