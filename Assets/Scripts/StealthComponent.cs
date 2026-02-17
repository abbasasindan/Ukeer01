using UnityEngine;

public class StealthComponent : MonoBehaviour
{
    [Header("Detection Settings")]
    public LayerMask hidingSpotLayer; // Set this to your 'HidingSpot' layer
    public float checkRadius = 0.6f;
    
    [Header("Visual Feedback")]
    public float hiddenAlpha = 0.5f; // Transparency when hidden

    private bool isHidden = false;
    private SpriteRenderer sprite;
    private int originalSortingOrder;
    private Rigidbody2D rb;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        originalSortingOrder = sprite.sortingOrder;
    }

    void Update()
    {
        // UC-2: Check if player is overlapping a 'HidingSpot' collider
        bool canHide = Physics2D.OverlapCircle(transform.position, checkRadius, hidingSpotLayer);

        if (canHide && Input.GetKeyDown(KeyCode.S))
        {
            ToggleStealth();
        }
        
        // If we move away from the rock, force un-hide
        if (!canHide && isHidden)
        {
            SetStealth(false);
        }
    }

    void ToggleStealth()
    {
        SetStealth(!isHidden);
    }

    public void SetStealth(bool state)
    {
        isHidden = state;
        
        if (isHidden)
        {
            // Visual: Fade and move behind the rock
            sprite.color = new Color(1, 1, 1, hiddenAlpha);
            sprite.sortingOrder = -1; 
            Debug.Log("Stealth: Hidden from Bandits.");
        }
        else
        {
            // Visual: Restore original state
            sprite.color = Color.white;
            sprite.sortingOrder = originalSortingOrder;
            Debug.Log("Stealth: Exposed.");
        }
    }

    // Public getter so Bandit AI can check if player is hidden
    public bool IsPlayerHidden() => isHidden;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}