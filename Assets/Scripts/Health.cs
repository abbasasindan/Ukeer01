using UnityEngine;
using UnityEngine.UI; // Required for Slider

public class Health : MonoBehaviour
{
    [Header("Health Stats")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("UI Reference")]
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        
        // Initialize the slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        // Update the UI bar
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        Debug.Log(gameObject.name + " took damage! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Died! Reloading...");
            // Add game over logic here
        }
        else
        {
            Debug.Log("Enemy Defeated!");
            Destroy(gameObject);
        }
    }
}