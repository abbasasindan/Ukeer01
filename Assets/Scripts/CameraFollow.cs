using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [Header("World A Layers (Present)")]
    // Drag ALL present-world background layers here
    public Transform[] presentBackgroundLayers; 
    
    [Header("World B Layers (Past)")]
    // Drag ALL past-world background layers here
    public Transform[] pastBackgroundLayers; 

    [Header("Parallax Settings")]
    // This array holds the speed for each layer, matching the order in the arrays above
    public float[] parallaxSpeeds; 
    
    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = transform.position;
        // Basic check for array length consistency
        if (presentBackgroundLayers.Length != pastBackgroundLayers.Length || 
            presentBackgroundLayers.Length != parallaxSpeeds.Length)
        {
            Debug.LogError("Parallax setup error: Mismatch in the number of layers or speeds between worlds.");
        }
    }

    void LateUpdate()
    {
        // Calculate how much the camera has moved since the last frame
        Vector3 deltaMovement = transform.position - lastCameraPosition;

        // Loop through the speeds array (since both layer arrays have the same length)
        for (int i = 0; i < parallaxSpeeds.Length; i++)
        {
            float parallaxX = deltaMovement.x * parallaxSpeeds[i];
            
            // 1. Move the Present (World A) Layer
            Vector3 presentTargetPos = presentBackgroundLayers[i].position;
            presentTargetPos.x += parallaxX;
            presentBackgroundLayers[i].position = presentTargetPos;

            // 2. Move the Past (World B) Layer
            Vector3 pastTargetPos = pastBackgroundLayers[i].position;
            pastTargetPos.x += parallaxX;
            pastBackgroundLayers[i].position = pastTargetPos;
        }

        // Update the camera's position for the next frame
        lastCameraPosition = transform.position;
    }
}