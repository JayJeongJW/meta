using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 따라갈 대상 (플레이어)
    private float offsetX;   // x축 거리 유지용 오프셋

    void Start()
    {
        if (target != null)
        {
            offsetX = transform.position.x - target.position.x;
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        transform.position = pos;
    }
}