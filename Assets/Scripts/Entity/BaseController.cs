using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer characterRenderer;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private bool isJumpPressed = false;  // 점프 키가 눌려 있는 상태를 저장

    protected AnimationHandler animationHandler;  // 애니메이션 핸들러


    protected virtual void Awake()
    {
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }
    // 점프 관련 처리
    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        HandleAction();  // 이동 및 회전 관련 입력을 처리
        Rotate(lookDirection);  // 마우스 방향에 따른 회전 처리
        HandleJump();  // 점프 관련 입력 처리
        DetectMouseInput();  // 마우스 입력 처리
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);  // 물리적 이동 처리
    }

    // 이동 메서드
    protected virtual void HandleAction()
    { 

    }

    public void Movement(Vector2 direction)
    {
        Vector2 move = direction * 5f;  // 이동 속도 설정
        _rigidbody.velocity = new Vector2(move.x, _rigidbody.velocity.y);  // Y는 그대로, X만 이동
        animationHandler.Move(direction);  // 애니메이션 핸들러에 이동 방향 전달
        animationHandler.Jump(isJumpPressed);  // 애니메이션 핸들러에 점프 상태 전달
    }

    // 회전 메서드
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;
        characterRenderer.flipX = isLeft;
    }

    // 점프 기능 처리
    protected void HandleJump()
    {  
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(_rigidbody.velocity.y) < 0.01f)  // Y축 속도가 거의 0일 때만 점프 가능
        {
            isJumpPressed = true;
            _rigidbody.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);  // 점프 힘을 추가
            Debug.Log("점프 키 눌림");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumpPressed = false;
            Debug.Log("점프 키 해제");
        }

        // 애니메이션 상태 업데이트
        animationHandler.Jump(isJumpPressed || _rigidbody.velocity.y > 0);  // 점프 중일 때 애니메이션 트리거
    }

    // 마우스 입력 처리
    private void DetectMouseInput()
    {
        if (Input.GetMouseButtonDown(0))  // 왼쪽 클릭
        {
            Debug.Log("왼쪽 클릭 발생");

            Vector3 screenPos = Input.mousePosition;
            screenPos.z = 10f;  // 카메라와의 거리

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);  // 화면 좌표를 월드 좌표로 변환
            Debug.Log($"화면 좌표: {screenPos}, 월드 좌표: {worldPos}");
        }
    }
}
