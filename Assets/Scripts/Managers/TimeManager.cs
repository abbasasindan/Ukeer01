using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // === 1. SINGLETON INSTANCE DECLARATION ===
    // This static variable allows other scripts to access this instance easily.
    public static TimeManager instance;

    // State variables for the Dual-World mechanic
    private bool timeAbilityUnlocked = false; 
    
    // Tracks the current world state: true = Past (Lush), false = Present (Barren)
    private bool isPastActive = false; 

    // Public reference to the Player's Flashback key (optional, can be done via InputManager)
    // You can set the key in the Inspector if needed, but the Relic script only unlocks the ability.
    // [SerializeField] private KeyCode flashbackKey = KeyCode.F;

    void Awake()
    {
        // === 2. SINGLETON IMPLEMENTATION LOGIC ===
        // Ensures only one instance of TimeManager exists in the scene.
        if (instance == null)
        {
            instance = this;
            // Optionally, prevent the manager from being destroyed when loading new scenes:
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy this duplicate if an instance already exists.
            Destroy(gameObject);
        }
        
        // Ensure the ability starts locked
        timeAbilityUnlocked = false;
        isPastActive = false;
    }

    // === 3. METHOD CALLED BY RELIC.CS ===
    // This is called when the player picks up the relic.
    public void UnlockAbility()
    {
        if (!timeAbilityUnlocked)
        {
            timeAbilityUnlocked = true;
            Debug.Log("Flashback Ability Unlocked! Press F to shift realities.");
            // You would typically call a UI element here to display the 'F' key prompt.
        }
    }

    // === 4. CORE MECHANIC: SHIFTING REALITIES ===
    // This would be called by the PlayerController when the 'F' key is pressed.
    public void ToggleRealityShift()
    {
        if (timeAbilityUnlocked)
        {
            // The Flashback Mechanism Logic Flow (Figure 4.5) is executed here.
            isPastActive = !isPastActive;
            
            if (isPastActive)
            {
                Debug.Log("Entering Lush Past (World B).");
                // Code to disable Desert Tilemap and enable Lush Tilemap goes here.
                // Code to activate the custom Time Dissolve Shader goes here.
            }
            else
            {
                Debug.Log("Returning to Barren Present (World A).");
                // Code to disable Lush Tilemap and enable Desert Tilemap goes here.
            }
        }
        else
        {
            Debug.Log("Flashback Ability is Locked. Find the Time Relic first.");
        }
    }
}