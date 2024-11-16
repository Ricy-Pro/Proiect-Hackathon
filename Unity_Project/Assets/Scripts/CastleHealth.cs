using UnityEngine;
using UnityEngine.UI;  // Needed to work with UI components like Slider and Image

public class CastleHealth : MonoBehaviour
{
    // Define the maximum health of the castle
    public int maxHealth = 100;
    private int currentHealth;

    // Reference to the Health Bar (Slider)
    public Slider healthBar;

    // Reference to the Fill Area Image component (for color changes)
    public Image healthBarFillImage; 

    void Start()
    {
        // Initialize the current health to max at the start of the game
        currentHealth = maxHealth;

        // Ensure health bar is full at the start
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // If you want to change the color at the start, you can set it here too
        if (healthBarFillImage != null)
        {
            healthBarFillImage.color = Color.green;  // Initial color (green when full health)
        }
    }

    // Function to handle taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Castle Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Object.Destroy(gameObject);
        }
    }

    // Function to change the color of the health bar
    void UpdateHealthBarColor()
    {
        if (healthBarFillImage != null)
        {
            if (currentHealth > maxHealth * 0.5f)
                healthBarFillImage.color = Color.green;   // Green for more than 50% health
            else if (currentHealth > maxHealth * 0.2f)
                healthBarFillImage.color = Color.yellow;  // Yellow for 20% to 50% health
            else
                healthBarFillImage.color = Color.red;     // Red for less than 20% health
        }
    }

    // Handle destruction of the castle
    void DestroyCastle()
    {
        // For now, just destroy the castle GameObject
        Destroy(gameObject);
    }
}
