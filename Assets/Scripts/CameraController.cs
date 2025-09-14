using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public float followSpeed = 5f;
    public Vector3 offset = new Vector3(0, 0, -10);

    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player != null)
        {
            // Set initial position
            transform.position = player.transform.position + offset;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Smoothly follow player
            Vector3 targetPosition = player.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
