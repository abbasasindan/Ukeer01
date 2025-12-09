using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Header("Tracking Settings")]
    public Transform cameraTransform; 
    public float parallaxMultiplier = 1.0f; 

    private Vector3 lastCameraPosition;
    // Store the initial Y position relative to the root Environment object
    private float initialY; 

    void Start()
    {
        // Store the starting Y position
        initialY = transform.position.y;
        InitializeTracking();
    }
    
    private void InitializeTracking()
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

        // Apply parallax only to the X-axis movement
        Vector3 newPosition = transform.position;
        newPosition.x += deltaMovement.x * parallaxMultiplier;
        
        // CRITICAL FIX: Lock the Y position to the initial starting Y value 
        // to prevent any vertical drift or unwanted movement.
        newPosition.y = initialY;

        transform.position = newPosition;
        lastCameraPosition = cameraTransform.position;
    }

    // Public method called by TimeManager.cs
    public void ResetParallaxTracking()
    {
        InitializeTracking();
    }
}