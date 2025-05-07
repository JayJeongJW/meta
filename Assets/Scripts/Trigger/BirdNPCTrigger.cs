using UnityEngine;

public class BirdNPCTrigger : MonoBehaviour
{
    public float closeDistance = 2f;  // 벗어날 거리 기준
    private Transform playerTransform;
    private bool isUIOpen = false;

    private void Update()
    {
        if (isUIOpen && playerTransform != null)
        {
            float dist = Vector2.Distance(transform.position, playerTransform.position);
            if (dist > closeDistance)
            {
              
                UIManager uiManager = FindObjectOfType<UIManager>();
                uiManager.ChangeState(UIState.None);
                isUIOpen = false;
                playerTransform = null;
            }
        }
    }

    public void TryInteract(Transform player)
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.ChangeState(UIState.BirdNPC);

        BirdNPCUI ui = FindObjectOfType<BirdNPCUI>();
        ui.UpdateQaText("Do  you want to play Bird mini game?");

        playerTransform = player;
        isUIOpen = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SetNearbyBirdNPC(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SetNearbyBirdNPC(null);
        }
    }
}
