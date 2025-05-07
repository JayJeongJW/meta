using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Camera camera;
    private GameManager gameManager;
    private Rigidbody2D _rigidbody;
    private Animator animator;
    private AnimationHandler animationHandler;
    private BirdNPCTrigger nearbyBirdNPC;
    private StackNPCTrigger nearbyStackNPC;



    private bool isJumpPressed = false;

    public Vector2 movementDirection = Vector2.zero;
    private Vector2 lookDirection = Vector2.zero;

    
    public float minX = -8f, maxX = 8f;
    public float minY = -4.5f, maxY = 4.5f;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        camera = Camera.main;
    }




    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        animationHandler = GetComponentInChildren<AnimationHandler>();


        if (animator == null)
        {

        }
    }

    void Update()
    {
        // WASD 입력 처리
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(moveX, moveY).normalized;


        if (movementDirection.magnitude > 0.1f && !isJumpPressed)
        {
            animator.SetBool("IsMove", true);  // 이동 중이면 IsMove를 true
        }
        else
        {
            animator.SetBool("IsMove", false); // 멈추면 IsMove를 false
        }


        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPosition - (Vector2)transform.position);


        _rigidbody.velocity = movementDirection * speed;


        RestrictMovement();


        Rotate(lookDirection);


        HandleJump();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearbyBirdNPC != null)
            {
                nearbyBirdNPC.TryInteract(this.transform);
            }

            else if (nearbyStackNPC != null)
            {
                nearbyStackNPC.TryInteract(this.transform);
            }
        }


    }

    // 점프 처리 (애니메이션만)
    protected void HandleJump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumpPressed = false;
        }


        if (isJumpPressed)
        {
            animator.SetBool("IsJump", true);
        }
        else
        {
            animator.SetBool("IsJump", false);
        }
    }

    // 맵 경계를 넘어가지 않게 제한
    private void RestrictMovement()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }

    // 회전 처리
    private void Rotate(Vector2 direction)
    {
        if (direction.magnitude != 0)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bool isLeft = Mathf.Abs(rotZ) > 90f;
            transform.rotation = Quaternion.Euler(0f, isLeft ? 180f : 0f, 0f);
        }
    }
    public void SetNearbyBirdNPC(BirdNPCTrigger npc)
    {
        nearbyBirdNPC = npc;
    }

    public void SetNearbyStackNPC(StackNPCTrigger npc)
    {
        nearbyStackNPC = npc;
    }
}
