using UnityEngine;

public class SpriteScaleCorrector : MonoBehaviour
{
    // === Public Fields for Inspector Setup ===
    [Tooltip("The factor to multiply the size by (e.g., 1.5, 2.0). Adjust until walking size matches idle size.")]
    public float scaleFactor = 1.0f; 

    // Internal variable to store the consistent base scale set in the Inspector (the idle size)
    private Vector3 originalLocalScale;

    void Start()
    {
        // Store the consistent size set on the Player's Transform (this is the base/idle size)
        originalLocalScale = transform.localScale; 
        
        Debug.Log("Scale Corrector Initialized. Base Scale: " + originalLocalScale);
    }

    // === Methods called by the State Machine Behaviour ===

    // Called by OnStateEnter in ScaleTriggerBehaviour (when walking starts)
    public void ApplyWalkScale()
    {
        // Apply the calculated scale factor to X and Y dimensions relative to the base scale
        transform.localScale = new Vector3(
            originalLocalScale.x * scaleFactor,
            originalLocalScale.y * scaleFactor,
            originalLocalScale.z // Z remains 1
        );
        Debug.Log("Applied Walk Scale: " + transform.localScale);
    }

    // Called by OnStateExit in ScaleTriggerBehaviour (when walking stops)
    public void RevertScale()
    {
        // Returns the player to the base scale (Idle size)
        transform.localScale = originalLocalScale;
        Debug.Log("Reverted Scale to Base: " + transform.localScale);
    }
}