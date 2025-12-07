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

    void Start()
    {
        // SAFETY CHECK: Ensure the box is hidden when the game loads
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }

    void Update()
    {
        // IF the dialogue is open, listen for a click or key press to close it
        if (dialogueBox.activeInHierarchy)
        {
            // Input.GetMouseButtonDown(0) = Left Click
            // Input.GetKeyDown(KeyCode.Space) = Spacebar
            // Input.GetKeyDown(KeyCode.E) = E key
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                CloseDialogue();
            }
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