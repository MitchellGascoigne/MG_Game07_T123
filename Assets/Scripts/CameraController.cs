using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public BoxCollider2D boundaries;

    private float cameraHalfWidth;
    private float cameraHalfHeight;

    private void Start()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Get the target's position and clamp it to the boundaries
        float targetX = Mathf.Clamp(target.position.x,
            boundaries.bounds.min.x + cameraHalfWidth,
            boundaries.bounds.max.x - cameraHalfWidth);

        float targetY = Mathf.Clamp(target.position.y,
            boundaries.bounds.min.y + cameraHalfHeight,
            boundaries.bounds.max.y - cameraHalfHeight);

        // Set the camera's position to the clamped target position
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
