using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsJump = Animator.StringToHash("IsJump");

    protected Animator animator;

    // 애니메이터 초기화
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // 이동 처리 메서드 (이동 시 애니메이션 트리거)
    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);  // 이동 중이면 true
    }

    // 점프 처리 메서드 (점프 시 애니메이션 트리거)
    public void Jump(bool isJump)
    {
        animator.SetBool(IsJump, isJump);  // 점프 중이면 true

    }
}
