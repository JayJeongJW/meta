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

    private bool isJumpPressed = false;  // ���� Ű�� ���� �ִ� ���¸� ����

    protected AnimationHandler animationHandler;  // �ִϸ��̼� �ڵ鷯


    protected virtual void Awake()
    {
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }
    // ���� ���� ó��
    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        HandleAction();  // �̵� �� ȸ�� ���� �Է��� ó��
        Rotate(lookDirection);  // ���콺 ���⿡ ���� ȸ�� ó��
        HandleJump();  // ���� ���� �Է� ó��
        DetectMouseInput();  // ���콺 �Է� ó��
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);  // ������ �̵� ó��
    }

    // �̵� �޼���
    protected virtual void HandleAction()
    { 

    }

    public void Movement(Vector2 direction)
    {
        Vector2 move = direction * 5f;  // �̵� �ӵ� ����
        _rigidbody.velocity = new Vector2(move.x, _rigidbody.velocity.y);  // Y�� �״��, X�� �̵�
        animationHandler.Move(direction);  // �ִϸ��̼� �ڵ鷯�� �̵� ���� ����
        animationHandler.Jump(isJumpPressed);  // �ִϸ��̼� �ڵ鷯�� ���� ���� ����
    }

    // ȸ�� �޼���
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;
        characterRenderer.flipX = isLeft;
    }

    // ���� ��� ó��
    protected void HandleJump()
    {  
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(_rigidbody.velocity.y) < 0.01f)  // Y�� �ӵ��� ���� 0�� ���� ���� ����
        {
            isJumpPressed = true;
            _rigidbody.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);  // ���� ���� �߰�
            Debug.Log("���� Ű ����");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumpPressed = false;
            Debug.Log("���� Ű ����");
        }

        // �ִϸ��̼� ���� ������Ʈ
        animationHandler.Jump(isJumpPressed || _rigidbody.velocity.y > 0);  // ���� ���� �� �ִϸ��̼� Ʈ����
    }

    // ���콺 �Է� ó��
    private void DetectMouseInput()
    {
        if (Input.GetMouseButtonDown(0))  // ���� Ŭ��
        {
            Debug.Log("���� Ŭ�� �߻�");

            Vector3 screenPos = Input.mousePosition;
            screenPos.z = 10f;  // ī�޶���� �Ÿ�

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);  // ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            Debug.Log($"ȭ�� ��ǥ: {screenPos}, ���� ��ǥ: {worldPos}");
        }
    }
}
