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
        InitializeTracking();
    }
    
    private void InitializeTracking()
    {
        if (cameraTransform != null)
        {
            lastCameraPosition = cameraTransform.position;
        }
    }

    // This is called by TimeManager.cs the moment the teleport happens
    public void ResetParallaxTracking()
    {
        InitializeTracking();
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;
        
        // Calculate the camera movement since the last frame
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Move the layer based on the camera movement and multiplier
        Vector3 newPosition = transform.position;
        newPosition.x += deltaMovement.x * parallaxMultiplier;
        
        // Keep the Y locked to initial height
        newPosition.y = initialY;

        transform.position = newPosition;
        lastCameraPosition = cameraTransform.position;
    }
}