using System.Collections;
using UnityEngine;

public class MOB : MonoBehaviour
{
    public Transform castleTransform;   // Target for the mob
    public int maxHealth;               // Maximum health of the mob
    public int damage;                  // Damage dealt to the castle
    public float moveSpeed;             // Speed of the mob's movement

    private int currentHealth;          // Current health of the mob
    private Coroutine attackCoroutine;  // Coroutine for attack logic

    protected virtual void Start()
    {
        currentHealth = maxHealth;      // Initialize mob's health

        // Automatically find the CastleHealth object in the scene
        CastleHealth castleHealth = FindObjectOfType<CastleHealth>();
        if (castleHealth != null)
        {
            castleTransform = castleHealth.transform;
        }
        else
        {
            Debug.LogError("CastleHealth object not found!");
        }
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;        // Reduce health by the damage amount
        StartCoroutine(BlinkRed());    // Provide visual feedback for taking damage

        if (currentHealth <= 0)        // Destroy mob if health reaches zero
            Destroy(gameObject);
    }

    private IEnumerator BlinkRed()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.color = Color.red;      // Change sprite color to red
            yield return new WaitForSeconds(0.2f); // Wait briefly
            spriteRenderer.color = Color.white;    // Revert to original color
        }
    }

    private void Update()
    {
        if (castleTransform != null && attackCoroutine == null)
        {
            // If close to the castle, start attacking
            if (Vector2.Distance(transform.position, castleTransform.position) < 0.5f)
            {
                attackCoroutine = StartCoroutine(Attack());
            }
            else
            {
                MoveTowardsCastle(); // Otherwise, keep moving toward the castle
            }
        }
    }

    private void MoveTowardsCastle()
    {
        // Move toward the castle without rotating the sprite
        Vector2 direction = (castleTransform.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private IEnumerator Attack()
    {
        CastleHealth castleHealth = castleTransform.GetComponent<CastleHealth>();
        if (castleHealth != null)
        {
            castleHealth.TakeDamage(damage);  // Damage the castle
            Debug.Log($"Castle under attack! Damage dealt: {damage}");
        }

        yield return new WaitForSeconds(0.01f); // Wait briefly before destroying the mob
        Destroy(gameObject);                  // Destroy the mob after attacking
    }
}
