using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("Teleport Settings")]
    public float worldOffset = 1000f; // Distance between worlds
    public bool timeAbilityUnlocked = false; // Set to True in Inspector to test
    private bool isPastActive = false; 

    private GameObject player;

    void Awake()
    {
        // Singleton pattern
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); return; }
    }

    void Start()
    {
        // Finding player by tag is the most reliable way
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // TimeManager handles the F key so PlayerController doesn't have to
        if (timeAbilityUnlocked && Input.GetKeyDown(KeyCode.F))
        {
            ToggleRealityShift();
        }
    }

    // This method is required by your PlayerController script
    public bool IsTimeAbilityUnlocked()
    {
        return timeAbilityUnlocked;
    }

    public void UnlockAbility()
    {
        timeAbilityUnlocked = true;
    }

    public void ToggleRealityShift()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        // Flip the state (toggle)
        isPastActive = !isPastActive;
        
        // Calculate move amount: +1000 if going to Past, -1000 if coming back
        float moveAmount = isPastActive ? worldOffset : -worldOffset;

        // 1. Teleport the Player
        player.transform.position += new Vector3(moveAmount, 0, 0);

        // 2. Teleport the Camera immediately to prevent background sliding
        if (Camera.main != null)
        {
            Camera.main.transform.position += new Vector3(moveAmount, 0, 0);
        }

        // 3. Reset Parallax Tracking instantly
        // This stops the backgrounds from "flying" when the camera jumps 1000 units
        ParallaxLayer[] layers = Object.FindObjectsByType<ParallaxLayer>(FindObjectsSortMode.None);
        foreach (ParallaxLayer layer in layers)
        {
            layer.ResetParallaxTracking();
        }

        Debug.Log("Teleport Successful! Current World: " + (isPastActive ? "PAST" : "PRESENT"));
    }
}