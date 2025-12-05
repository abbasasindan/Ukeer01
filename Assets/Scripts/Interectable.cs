using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Settings")]
    [TextArea(3, 10)] // Creates a large text box in the Inspector for easy typing
    public string loreText = "The desert remembers what we forgot...";

    private bool playerInRange;

    void Update()
    {
        // Check if player is nearby AND presses the 'E' key
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // If the dialogue box is already open, close it
            if (DialogueManager.instance.dialogueBox.activeInHierarchy)
            {
                DialogueManager.instance.CloseDialogue();
            }
            // If the box is closed, open it with THIS object's specific text
            else
            {
                DialogueManager.instance.ShowDialogue(loreText);
            }
        }
    }

    // Triggered when the Player enters the collider zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered interaction range.");
        }
    }

    // Triggered when the Player leaves the collider zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            // Automatically close the dialogue if the player runs away
            DialogueManager.instance.CloseDialogue();
        }
    }
}