using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Header("Tracking Settings")]
    public Transform cameraTransform; 
    public float parallaxMultiplier = 1.0f; 

    private Vector3 lastCameraPosition;
    private float initialY; 

    void Start()
    {
        initialY = transform.position.y;
        if (cameraTransform == null && Camera.main != null) 
            cameraTransform = Camera.main.transform;
            
        ResetParallaxTracking();
    }

    // This is called by TimeManager during the teleport jump
    public void ResetParallaxTracking()
    {
        if (cameraTransform != null)
        {
            lastCameraPosition = cameraTransform.position;
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;
        
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Apply movement
        Vector3 newPos = transform.position;
        newPos.x += deltaMovement.x * parallaxMultiplier;
        newPos.y = initialY; // Lock vertical position

        transform.position = newPos;
        lastCameraPosition = cameraTransform.position;
    }
}