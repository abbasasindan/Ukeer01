using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // === 1. SINGLETON INSTANCE DECLARATION ===
    public static TimeManager instance;

    // === 2. ENVIRONMENT REFERENCES ===
    [Header("Environment References")]
    // These are found via code to be robust against team changes
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

    // === CORE MECHANIC: SHIFTING REALITIES (SYNCHRONIZATION ADDED) ===
    public void ToggleRealityShift()
    {
        if (timeAbilityUnlocked)
        {
            // 1. Flip the state
            isPastActive = !isPastActive;
            
            // 2. CRITICAL SYNCHRONIZATION STEP: Match the position of the inactive world to the active one.
            if (Environment_Present != null && Environment_Past != null)
            {
                if (isPastActive)
                {
                    // Moving to PAST: Set PAST position equal to PRESENT position.
                    Environment_Past.transform.position = Environment_Present.transform.position;
                }
                else
                {
                    // Moving to PRESENT: Set PRESENT position equal to PAST position.
                    Environment_Present.transform.position = Environment_Past.transform.position;
                }
            }
            
            // 3. Toggle the visibility of the two main environment containers
            if (Environment_Present != null)
                Environment_Present.SetActive(!isPastActive);
            
            if (Environment_Past != null)
                Environment_Past.SetActive(isPastActive);

            // 4. Delegate UI visibility control to the DialogueManager
            if (DialogueManager.instance != null)
            {
                DialogueManager.instance.SetPastWorldTextVisibility(isPastActive);
            }

            Debug.Log(isPastActive ? "Entering Environment_Past." : "Returning to Environment_Present.");
        }
    }
}