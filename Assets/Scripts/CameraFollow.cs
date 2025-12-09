using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target & Speed")]
    // Drag your Player GameObject here in the Inspector
    public Transform target;
    
    // Controls how smoothly the camera tracks the player (e.g., 5f to 10f)
    public float smoothing = 5f; 

    // The desired offset from the player (determines camera height/depth)
    public Vector3 offset = new Vector3(0f, 1f, -10f); 

    void LateUpdate()
    {
        // 1. Check if the target (Player) exists
        if (target == null) return;
        
        // 2. Calculate the desired position
        Vector3 targetCameraPosition = target.position + offset;

        // 3. Smoothly move the camera to the desired position
        // Vector3.Lerp moves the camera gradually toward the target for smooth tracking.
        transform.position = Vector3.Lerp(
            transform.position, 
            targetCameraPosition, 
            smoothing * Time.deltaTime
        );
    }
}