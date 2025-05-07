using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBird;

public class BgLooperM : MonoBehaviour
{
    [Header("배경 및 장애물 관리")]
    public int numBgCount = 5;
    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        // 씬 내의 모든 Obstacle을 찾아 초기화
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        if (obstacles.Length == 0) return;

        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 배경 반복 처리
        if (collision.CompareTag("BackGround"))
        {
            BoxCollider2D bgCollider = collision.GetComponent<BoxCollider2D>();
            if (bgCollider != null)
            {
                float width = bgCollider.size.x;
                Vector3 pos = collision.transform.position;
                pos.x += width * numBgCount;
                collision.transform.position = pos;
            }
            return;
        }

        // 장애물 재배치 처리
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}