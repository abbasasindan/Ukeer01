using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // === 1. SINGLETON INSTANCE ===
    public static TimeManager instance;

    [Header("Worlds")]
    // We need these references to actually turn the objects on/off
    public GameObject presentWorld;
    public GameObject pastWorld;

    [Header("Settings")]
    public KeyCode switchKey = KeyCode.F;

    // State variables (Using your teammate's naming convention)
    private bool timeAbilityUnlocked = false;
    private bool isPastActive = false;

    void Awake()
    {
        // Singleton Implementation
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Ensure the ability starts locked
        timeAbilityUnlocked = false;
        isPastActive = false;
    }

    void Start()
    {
        // Set the initial visual state
        UpdateWorldVisuals();
    }

    // === THIS WAS MISSING ===
    void Update()
    {
        // Check for input every frame
        if (timeAbilityUnlocked && Input.GetKeyDown(switchKey))
        {
            ToggleRealityShift();
        }
    }

    // === CALLED BY RELIC.CS ===
    public void UnlockAbility()
    {
        if (!timeAbilityUnlocked)
        {
            timeAbilityUnlocked = true;
            Debug.Log("Flashback Ability Unlocked!");
        }
    }

    // === CORE MECHANIC ===
    public void ToggleRealityShift()
    {
        // Toggle the boolean state
        isPastActive = !isPastActive;

        // Update the actual game objects
        UpdateWorldVisuals();
    }

    // Helper function to handle the SetActive logic
    void UpdateWorldVisuals()
    {
        if (isPastActive)
        {
            // Enter Past
            pastWorld.SetActive(true);
            presentWorld.SetActive(false);
        }
        else
        {
            // Return to Present
            pastWorld.SetActive(false);
            presentWorld.SetActive(true);
        }
    }
}