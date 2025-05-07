using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // 따라갈 대상 (플레이어)
    public float smoothSpeed = 5f;    // 얼마나 부드럽게 이동할지
    public Vector2 minBounds = new Vector2(-8f, -4.5f);
    public Vector2 maxBounds = new Vector2(8f, 4.5f);



    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 목표 위치 계산
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = transform.position.z;

        // 위치 제한
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        // 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
}
