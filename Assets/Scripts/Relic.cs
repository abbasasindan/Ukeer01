using UnityEngine;

public class Relic : MonoBehaviour
{
    [TextArea]
    public string message = "Time Relic acquired! Press 'F' to shift between timelines.";

    private bool playerInRange = false;

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
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered Relic range. Press E to interact.");
            // Optional: Show a small UI prompt here like "Press E"
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