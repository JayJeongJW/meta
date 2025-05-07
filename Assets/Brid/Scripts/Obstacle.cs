using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Obstacle : MonoBehaviour
    {
        [Header("장애물 위치/간격 설정")]
        public float highPosY = 1f;
        public float lowPosY = -1f;
        public float holeSizemin = 1f;
        public float holeSizemax = 3f;
        public float widthPadding = 3f;

        [Header("장애물 구성 오브젝트")]
        public Transform topObject;
        public Transform bottomObject;

        private BirdGameManager gameManager;

        private void Start()
        {
            gameManager = BirdGameManager.instance;
        }

        // 장애물을 화면에 배치하고 다음 위치 반환
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

        // 플레이어가 장애물을 통과할 때 점수 추가
        private void OnTriggerExit2D(Collider2D collision)
        {
            Player player = collision.GetComponent<Player>();

            // 플레이어가 살아있을 때만 점수 추가
            if (player != null && !player.isDead)
            {
                gameManager.AddScore(1);
            }
        }
    }
}