using UnityEngine;

public class TestDamage : MonoBehaviour
{
    // Reference to the CastleHealth script (attach the Castle GameObject here)
    public CastleHealth castleHealth;

    // Damage amount to apply
    public int damageAmount = 10;

    // This function is called when the user clicks the object (if it has a collider)
    private void OnMouseDown()
    {
        // Apply damage to the castle when the square is clicked
        if (castleHealth != null)
        {
            castleHealth.TakeDamage(damageAmount);
        }
        else
        {
            Debug.LogError("CastleHealth script not assigned to the Castle GameObject!");
        }
    }
}
