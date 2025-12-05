using UnityEngine;

public class Relic : MonoBehaviour
{
    [TextArea]
    public string message = "Time Relic acquired! Press 'F' to shift between timelines.";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object colliding is the Player
        if (collision.CompareTag("Player"))
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

            // 3. Remove the relic from the scene (it's been picked up)
            Destroy(gameObject);
        }
    }
}