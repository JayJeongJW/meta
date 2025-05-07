using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;

    public virtual void Init(UIManager uiManager)
    {
        this.uiManager = uiManager;

    }

    // �߻� �޼���: ���� Ŭ�������� �ݵ�� �����ؾ� ��
    protected abstract UIState GetUIState();

    // UI ���¿� �°� Ȱ��ȭ ���θ� ����
    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}

