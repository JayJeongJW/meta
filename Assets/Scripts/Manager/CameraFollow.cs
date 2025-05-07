using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // ���� ��� (�÷��̾�)
    public float smoothSpeed = 5f;    // �󸶳� �ε巴�� �̵�����
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

        // ��ǥ ��ġ ���
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = transform.position.z;

        // ��ġ ����
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        // �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
}
