using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("Environment References")]
    private GameObject Environment_Present; 
    private GameObject Environment_Past; 

    private const string DialoguePanelName = "Dialogue_Panel";
    private GameObject pastWorldTextPanel;

    private bool timeAbilityUnlocked = false; 
    private bool isPastActive = false; 

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); return; }
        
        Environment_Present = GameObject.Find("Environment_Present");
        Environment_Past = GameObject.Find("Environment_Past");
        pastWorldTextPanel = GameObject.Find(DialoguePanelName);

        // Start in the Present
        if (Environment_Present != null) Environment_Present.SetActive(true);
        if (Environment_Past != null) Environment_Past.SetActive(false);  
    }

    void Update()
    {
        // === THE MIRROR LOGIC ===
        // This ensures the Past always mimics the Present's position, 
        // effectively 'following' whatever the Present follows.
        if (Environment_Present != null && Environment_Past != null)
        {
            Environment_Past.transform.position = Environment_Present.transform.position;
        }

        // Input Listener
        if (timeAbilityUnlocked && Input.GetKeyDown(KeyCode.F))
        {
            ToggleRealityShift();
        }
    }

    public void UnlockAbility()
    {
        timeAbilityUnlocked = true;
        Debug.Log("Ability Unlocked: Shift with 'F'");
    }

    public bool IsTimeAbilityUnlocked()
    {
        return timeAbilityUnlocked;
    }

    public void ToggleRealityShift()
    {
        if (timeAbilityUnlocked)
        {
            isPastActive = !isPastActive;

            // Simple visibility swap
            if (Environment_Present != null) Environment_Present.SetActive(!isPastActive);
            if (Environment_Past != null) Environment_Past.SetActive(isPastActive);

            if (DialogueManager.instance != null)
            {
                DialogueManager.instance.SetPastWorldTextVisibility(isPastActive);
            }
        }
    }
}