using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public static DialogueManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
    }

    void Start()
    {
        if (instance == this && dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (instance != this) return;

        // IF dialogue is open, listen for close input
        if (dialogueBox != null && dialogueBox.activeInHierarchy)
        {
            // ADDED: KeyCode.E so controls are consistent with Interactable.cs
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)
                || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            {
                CloseDialogue();
            }
        }
    }

    public void ShowDialogue(string text)
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(true);
            if (dialogueText != null)
            {
                dialogueText.gameObject.SetActive(true);
                dialogueText.text = text;
            }
            Time.timeScale = 0f;
        }
    }

    public void CloseDialogue()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    // === NEW: METHOD FOR PAST WORLD TEXT VISIBILITY ===
    // This method is called by the TimeManager to toggle the persistent past world text.
    public void SetPastWorldTextVisibility(bool isVisible)
    {
        // The DialoguePanel variable should be assigned in the Inspector
        if (dialogueBox != null)
        {
            // Toggle the visibility of the dialogue box/panel
            dialogueBox.SetActive(isVisible);
            
            // Set the static text content if the panel is being made visible
            if (dialogueText != null && isVisible) 
            {
                // Ensure the text component itself is active (optional, but safe)
                dialogueText.gameObject.SetActive(true); 
                
                // Set the specific past-world text
                dialogueText.text = "The past whispers..."; 
            }
        }
        // NOTE: We do NOT set Time.timeScale = 0f here, as this is background text.
    }
}