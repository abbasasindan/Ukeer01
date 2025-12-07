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
            // If a duplicate exists, destroy this object AND return immediately
            // to prevent the rest of the code from running on a dead object.
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Safety Check: If this instance was destroyed in Awake, stop here.
        if (instance != this) return;

        // Ensure the box is hidden when the game loads
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }

    void Update()
    {
        // Safety Check: If this instance was destroyed, stop here.
        if (instance != this) return;

        // IF the dialogue is open, listen for a click or key press to close it
        if (dialogueBox != null && dialogueBox.activeInHierarchy)
        {
            // Input.GetMouseButtonDown(0) = Left Click
            // Input.GetKeyDown(KeyCode.Space) = Spacebar
            // Input.GetKeyDown(KeyCode.Return) = Enter
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                CloseDialogue();
            }
        }
    }

    public void ShowDialogue(string text)
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(true);             // 1. Open the Panel
            if (dialogueText != null)
            {
                dialogueText.gameObject.SetActive(true); // 2. FORCE the text object to turn on
                dialogueText.text = text;                // 3. Update the words
            }
            Time.timeScale = 0f;                     // 4. Pause the game
        }
    }

    public void CloseDialogue()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false); // Close the window
        }
        Time.timeScale = 1f;          // Unpause
    }
}