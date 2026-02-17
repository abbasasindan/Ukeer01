using UnityEngine;

public class BanditHealth : MonoBehaviour
{
    // Ensure the method is public so PlayerController can see it
    public void Vanish()
    {
        Debug.Log($"[Logic Check] Vanish called on: {gameObject.name}");

        // S#1: Disable the entire GameObject (The 'Uncheck' in Hierarchy)
        this.gameObject.SetActive(false);

        // S#2: Fallback - If it's still visible, manually kill the renderer/collider
        if (TryGetComponent<SpriteRenderer>(out var renderer)) renderer.enabled = false;
        if (TryGetComponent<Collider2D>(out var col)) col.enabled = false;

        // S#3: Verify if it actually happened
        if (!this.gameObject.activeInHierarchy)
        {
            Debug.Log("Bandit state: Successfully deactivated.");
        }
        else
        {
            Debug.LogError("Bandit state: FAILED to deactivate. Is something else enabling this?");
        }
    }
}