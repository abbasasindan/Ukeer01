using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // === 1. SINGLETON INSTANCE DECLARATION ===
    public static TimeManager instance;

    // === 2. ENVIRONMENT REFERENCES ===
    [Header("Environment References")]
    private GameObject Environment_Present; 
    private GameObject Environment_Past; 

    // === 3. UI REFERENCE ===
    private const string DialoguePanelName = "Dialogue_Panel";
    private GameObject pastWorldTextPanel;

    // State variables
    private bool timeAbilityUnlocked = false; 
    private bool isPastActive = false; 

    void Awake()
    {
        // === SINGLETON IMPLEMENTATION LOGIC ===
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // --- INITIALIZE REFERENCES IN CODE ---
        Environment_Present = GameObject.Find("Environment_Present");
        Environment_Past = GameObject.Find("Environment_Past");
        pastWorldTextPanel = GameObject.Find(DialoguePanelName);

        timeAbilityUnlocked = false;
        isPastActive = false;
        
        // Set initial state
        if (Environment_Present != null)
            Environment_Present.SetActive(true);
        if (Environment_Past != null)
            Environment_Past.SetActive(false);  
            
        // Set the text panel to OFF initially
        if (pastWorldTextPanel != null)
            pastWorldTextPanel.SetActive(false); 
    }
    
    public void UnlockAbility()
    {
        if (!timeAbilityUnlocked)
        {
            timeAbilityUnlocked = true;
            Debug.Log("Flashback Ability Unlocked! Press F to shift realities.");
        }
    }
    
    public bool IsTimeAbilityUnlocked()
    {
        return timeAbilityUnlocked; 
    }

    // === CORE MECHANIC: SHIFTING REALITIES (SYNCHRONIZATION & PARALLAX RESET) ===
    public void ToggleRealityShift()
    {
        if (timeAbilityUnlocked)
        {
            // 1. Flip the state and determine worlds
            isPastActive = !isPastActive;
            
            GameObject worldToActivate; 
            GameObject worldToDeactivate; 

            if (isPastActive)
            {
                worldToActivate = Environment_Past;
                worldToDeactivate = Environment_Present;
            }
            else
            {
                worldToActivate = Environment_Present;
                worldToDeactivate = Environment_Past;
            }

            // 2. CRITICAL SYNCHRONIZATION: Move the world to be activated to the exact X-position
            if (worldToActivate != null && worldToDeactivate != null)
            {
                Vector3 targetPosition = worldToDeactivate.transform.position;
                
                // Only copy the X position (where the player is looking), preserving original Y/Z vertical alignment.
                worldToActivate.transform.position = new Vector3(
                    targetPosition.x,
                    worldToActivate.transform.position.y,
                    worldToActivate.transform.position.z
                );
            }
            
            // 3. Toggle visibility (This triggers OnEnable() on the activated Parallax layers)
            if (Environment_Present != null)
                Environment_Present.SetActive(!isPastActive);
            
            if (Environment_Past != null)
                Environment_Past.SetActive(isPastActive);

            // 4. Delegate UI visibility control
            if (DialogueManager.instance != null)
            {
                DialogueManager.instance.SetPastWorldTextVisibility(isPastActive);
            }

            Debug.Log(isPastActive ? "Entering Environment_Past." : "Returning to Environment_Present.");
        }
    }
}