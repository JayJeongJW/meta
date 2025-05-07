using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Obstacle : MonoBehaviour
    {
        [Header("��ֹ� ��ġ/���� ����")]
        public float highPosY = 1f;
        public float lowPosY = -1f;
        public float holeSizemin = 1f;
        public float holeSizemax = 3f;
        public float widthPadding = 3f;

        [Header("��ֹ� ���� ������Ʈ")]
        public Transform topObject;
        public Transform bottomObject;

        private BirdGameManager gameManager;

        private void Start()
        {
            gameManager = BirdGameManager.instance;
        }

        // ��ֹ��� ȭ�鿡 ��ġ�ϰ� ���� ��ġ ��ȯ
        public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
        {
            float holeSize = Random.Range(holeSizemin, holeSizemax);
            float halfHoleSize = holeSize / 2f;

            topObject.localPosition = new Vector3(0, halfHoleSize);
            bottomObject.localPosition = new Vector3(0, -halfHoleSize);

            Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
            placePosition.y = Random.Range(lowPosY, highPosY);

            transform.position = placePosition;
            return placePosition;
        }

        // �÷��̾ ��ֹ��� ����� �� ���� �߰�
        private void OnTriggerExit2D(Collider2D collision)
        {
            Player player = collision.GetComponent<Player>();

            // �÷��̾ ������� ���� ���� �߰�
            if (player != null && !player.isDead)
            {
                gameManager.AddScore(1);
            }
        }
    }
}