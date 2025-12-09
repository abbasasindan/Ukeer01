using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Header("Tracking Settings")]
    // Drag the Main Camera's Transform here in the Inspector
    public Transform cameraTransform; 
    
    // Controls how much slower this layer moves (0.1 for slow, 0.9 for fast)
    public float parallaxMultiplier = 0.5f; 

    private Vector3 lastCameraPosition;

    void Start()
    {
        // Get the initial position of the camera when the scene starts
        if (cameraTransform != null)
        {
            lastCameraPosition = cameraTransform.position;
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;
        
        // 1. Calculate how far the camera has moved since the last frame
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // 2. Apply the parallax effect: move the layer a fraction of the camera's movement
        Vector3 newPosition = transform.position + deltaMovement * parallaxMultiplier;

        // 3. Update the layer's position
        transform.position = newPosition;

        // 4. Update the stored camera position for the next frame
        lastCameraPosition = cameraTransform.position;
    }
}