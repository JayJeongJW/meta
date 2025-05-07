using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Player : MonoBehaviour
    {
        private Animator animator;
        private Rigidbody2D _rigidbody;

        [Header("�̵� �� ���� ����")]
        public float flapForce = 6f;
        public float forwardSpeed = 3f;

        [Header("�÷��̾� ����")]
        public bool isDead = false;
        public bool godMode = false; // �׽�Ʈ�� ���� ����

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
                        // ���� �� ������ �ƹ� ���� ���� (�����ϸ� Retry ��ư�� ������ ����)
                    }
                }
            }
            else
            {
                // ������� �� ���� �Է� �ޱ�
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

            // ȸ�� ���� ���� (���� �� ���� ���, �ϰ� �� �Ʒ��� ����)
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