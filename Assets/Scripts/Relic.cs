using UnityEngine;

public class Relic : MonoBehaviour
{
    [TextArea]
    public string message = "Time Relic acquired! Press 'F' to shift between timelines.";

    private bool playerInRange = false;
    private bool isPickedUp = false; 

    void Update()
    {
        // Check if Player is close AND presses 'E', AND the relic hasn't been picked up yet
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isPickedUp)
        {
            PickUpRelic();
        }
    }

    void PickUpRelic()
    {
        // Set flag immediately to prevent re-entry
        isPickedUp = true; 

        // 1. Unlock the Time Travel ability
        if (TimeManager.instance != null)
        {
            TimeManager.instance.UnlockAbility();
        }

        // 2. OPTIONAL: If you still need a notification, show a simple Debug log or use a quick fading UI element.
        // NOTE: The main past-world text is now controlled by TimeManager.ToggleRealityShift()
        // If you need the DialogueManager for the initial notification, uncomment the following block:
        /*
        if (DialogueManager.instance != null)
        {
            DialogueManager.instance.ShowDialogue(message);
        }
        */

        // 3. Destroy the relic
        Destroy(gameObject);
    }

    // Detect when player walks close
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPickedUp)
        {
            playerInRange = true;
        }
    }

    // Detect when player walks away
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}