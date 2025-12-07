using UnityEngine;

public class Relic : MonoBehaviour
{
    [TextArea]
    public string message = "Time Relic acquired! Press 'F' to shift between timelines.";

    private bool playerInRange = false;

    // --- NEW DEBUG SECTION ---
    void Start()
    {
        // If this doesn't print, the script is disabled or not attached!
        Debug.Log("RELIC SCRIPT IS ALIVE on object: " + gameObject.name);
    }
    // -------------------------

    void Update()
    {
        // Now checks if Player is close AND presses 'E'
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed while in range. Picking up relic.");
            PickUpRelic();
        }
    }

    void PickUpRelic()
    {
        Debug.Log("PickUpRelic called.");

        // 1. Unlock the Time Travel ability
        if (TimeManager.instance != null)
        {
            TimeManager.instance.UnlockAbility();
        }
        else
        {
            Debug.LogError("TimeManager instance not found!");
        }

        // 2. Show the tutorial text
        if (DialogueManager.instance != null)
        {
            DialogueManager.instance.ShowDialogue(message);
        }
        else
        {
            Debug.LogError("DialogueManager instance not found!");
        }

        // 3. Destroy the relic (so you can't pick it up twice)
        Destroy(gameObject);
    }

    // Detect when player walks close
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // --- NEW DEBUG ---
        // This prints the name of ANYTHING that touches the relic
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        // -----------------

        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered Relic range. Press E to interact.");
        }
    }

    // Detect when player walks away
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left Relic range.");
        }
    }
}