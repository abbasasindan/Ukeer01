using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("Teleport Settings")]
    public float worldOffset = 1000f; 
    private bool timeAbilityUnlocked = false; 
    private bool isPastActive = false; 

    private GameObject player;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); return; }
        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (timeAbilityUnlocked && Input.GetKeyDown(KeyCode.F))
        {
            ToggleRealityShift();
        }
    }

    public void ToggleRealityShift()
    {
        if (player == null) return;

        isPastActive = !isPastActive;
        
        // Calculate the jump distance
        float moveAmount = isPastActive ? worldOffset : -worldOffset;

        // 1. Teleport the Player
        player.transform.position += new Vector3(moveAmount, 0, 0);

        // 2. Teleport the Camera so the player stays in view
        if (Camera.main != null)
        {
            Camera.main.transform.position += new Vector3(moveAmount, 0, 0);
        }

        // 3. FIX THE DRIFT: Find all parallax layers and reset them
        // Note: Using FindObjectsByType to avoid the obsolete warning in your screenshot
        ParallaxLayer[] allLayers = Object.FindObjectsByType<ParallaxLayer>(FindObjectsSortMode.None);
        foreach (ParallaxLayer layer in allLayers)
        {
            layer.ResetParallaxTracking();
        }
    }

    public void UnlockAbility() { timeAbilityUnlocked = true; }
}