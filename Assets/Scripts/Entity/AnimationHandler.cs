using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsJump = Animator.StringToHash("IsJump");

    protected Animator animator;

    // �ִϸ����� �ʱ�ȭ
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // �̵� ó�� �޼��� (�̵� �� �ִϸ��̼� Ʈ����)
    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);  // �̵� ���̸� true
    }

    // ���� ó�� �޼��� (���� �� �ִϸ��̼� Ʈ����)
    public void Jump(bool isJump)
    {
        animator.SetBool(IsJump, isJump);  // ���� ���̸� true

    }
}
