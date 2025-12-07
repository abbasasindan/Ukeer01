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
}