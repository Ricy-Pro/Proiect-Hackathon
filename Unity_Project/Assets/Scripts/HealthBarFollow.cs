using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthSlider;  // Reference to the health slider
    public float currentHealth = 100f;
    public float maxHealth = 100f;

    void Start()
    {
        // Initialize health slider value
        healthSlider.value = currentHealth / maxHealth;
    }

    void Update()
    {
        // Update health slider based on current health
        healthSlider.value = currentHealth / maxHealth;
    }

    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
    }
}
