using UnityEngine;
using TMPro; // Crucial for TextMeshPro

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;       // The Black Panel
    public TextMeshProUGUI dialogueText; // The White Text inside it

    // Singleton Pattern: Lets other scripts find this easily
    public static DialogueManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowDialogue(string text)
    {
        dialogueBox.SetActive(true); // Open the window
        dialogueText.text = text;    // Update the words
        Time.timeScale = 0f;         // Pause the game
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false); // Close the window
        Time.timeScale = 1f;          // Unpause
    }
}