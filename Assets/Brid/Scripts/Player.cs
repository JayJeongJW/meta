using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Player : MonoBehaviour
    {
        private Animator animator;
        private Rigidbody2D _rigidbody;

        [Header("이동 및 점프 설정")]
        public float flapForce = 6f;
        public float forwardSpeed = 3f;

        [Header("플레이어 상태")]
        public bool isDead = false;
        public bool godMode = false; // 테스트용 무적 상태

        private float deathCooldown = 0f;
        private bool isFlap = false;

        private BirdGameManager gameManager;

        void Start()
        {
            gameManager = BirdGameManager.instance;
            animator = GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (isDead)
            {
                deathCooldown -= Time.deltaTime;

                if (deathCooldown <= 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        // 죽은 후 눌러도 아무 동작 없음 (가능하면 Retry 버튼을 누르게 유도)
                    }
                }
            }
            else
            {
                // 살아있을 때 점프 입력 받기
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    isFlap = true;
                }
            }
        }

        void FixedUpdate()
        {
            if (isDead) return;

            Vector3 velocity = _rigidbody.velocity;
            velocity.x = forwardSpeed;

            if (isFlap)
            {
                velocity.y += flapForce;
                isFlap = false;
            }

            _rigidbody.velocity = velocity;

            // 회전 각도 조정 (점프 시 위로 들고, 하강 시 아래로 향함)
            float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (godMode || isDead) return;

            isDead = true;
            deathCooldown = 1f;

            animator.SetTrigger("IsDie");
            gameManager.GameOver();
        }
    }
}