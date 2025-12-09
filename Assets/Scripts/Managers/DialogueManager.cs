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

    // NOTE: Start() is removed to let the TimeManager set the initial visibility state.
    // NOTE: The Update() method is removed to let the TimeManager set the visibility state.
    
    // Original methods kept for external calls (like the Relic interaction)
    public void ShowDialogue(string text)
    {
        if (dialogueBox != null)
        {
            // IMPORTANT: We only set visibility to TRUE here. TimeManager handles the OFF state.
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
    
    // This method is now only for setting the static text during world swap
    public void SetPastWorldTextVisibility(bool isVisible)
    {
        if (dialogueBox != null)
        {
            // Toggle the visibility of the dialogue box/panel
            dialogueBox.SetActive(isVisible);
            
            // Set the static text content if the panel is being made visible
            if (dialogueText != null && isVisible) 
            {
                dialogueText.gameObject.SetActive(true); 
                dialogueText.text = "The past whispers..."; 
            }
        }
    }
}