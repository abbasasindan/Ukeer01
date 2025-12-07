using UnityEngine;

public class Relic : MonoBehaviour
{
    [TextArea]
    public string message = "Time Relic acquired! Press 'F' to shift between timelines.";

    private bool playerInRange = false;

    void Update()
    {
        // Check if Player is close AND presses 'E'
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUpRelic();
        }
    }

    void PickUpRelic()
    {
        // 1. Unlock the Time Travel ability
        if (TimeManager.instance != null)
        {
            TimeManager.instance.UnlockAbility();
        }

        // 2. Show the tutorial text
        if (DialogueManager.instance != null)
        {
            DialogueManager.instance.ShowDialogue(message);
        }

        // 3. Destroy the relic
        Destroy(gameObject);
    }

    // Detect when player walks close
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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