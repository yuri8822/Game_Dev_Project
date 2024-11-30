using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
        // Calculate screen bounds based on camera
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Store bounds
        screenBounds = new Vector2(
            Mathf.Abs(bottomLeft.x - topRight.x) / 2,
            Mathf.Abs(bottomLeft.y - topRight.y) / 2
        );
    }

    void LateUpdate()
    {
        // Restrict player position
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -screenBounds.x, screenBounds.x);
        position.y = Mathf.Clamp(position.y, -screenBounds.y, screenBounds.y);
        transform.position = position;
    }
}
